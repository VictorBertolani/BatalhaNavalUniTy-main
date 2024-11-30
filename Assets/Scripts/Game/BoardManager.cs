using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BoardManager : MonoBehaviourPunCallbacks
{
    public GameObject cellPrefab;       // Prefab para a célula do tabuleiro
    public Transform playerBoardPanel1; // Painel onde o tabuleiro do jogador 1 será gerado
    public Transform playerBoardPanel2; // Painel onde o tabuleiro do jogador 2 será gerado
    public Transform opponentBoardPanel1; // Painel do oponente para o jogador 1
    public Transform opponentBoardPanel2; // Painel do oponente para o jogador 2
    public Text statusText;             // Texto para mostrar o status do jogo
    public GameObject shipPrefab;       // Prefab do navio

    private const int gridSize = 6;     // Tamanho do tabuleiro (6x6)
    private GameObject[,] playerShips = new GameObject[gridSize, gridSize]; // Navios do jogador
    private int shipsToPlace = 6;       // Número máximo de navios que cada jogador deve posicionar
    private int placedShips = 0;        // Contador de navios posicionados

    private enum GameState { PlacingShips, Attacking }
    private GameState currentState;

    private void Start()
    {
        // Verifica se o jogador é o Host ou Client
        if (PhotonNetwork.IsMasterClient)
        {
            // O Host usa o PlayerBoard1 e o oposto para o jogador 2
            GenerateBoard(true, playerBoardPanel1, opponentBoardPanel2);
        }
        else
        {
            // O Client usa o PlayerBoard2 e o oposto para o jogador 1
            GenerateBoard(false, playerBoardPanel2, opponentBoardPanel1);
        }

        currentState = GameState.PlacingShips; // Começa na fase de posicionamento
        UpdateStatus();
        SetCellInteractable(true); // Habilita as células do jogador
        SetOpponentBoardVisible(false); // Torna o tabuleiro do oponente invisível durante a colocação dos barcos
    }

    private void GenerateBoard(bool isPlayerBoard, Transform playerBoard, Transform opponentBoard)
    {
        // Gerar o tabuleiro do jogador
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject cell = Instantiate(cellPrefab, playerBoard);
                cell.GetComponent<BoardCell>().Initialize(x, y, this, isPlayerBoard, statusText);
            }
        }

        // Gerar o tabuleiro do oponente (este será invisível durante a fase de posicionamento)
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject cell = Instantiate(cellPrefab, opponentBoard);
                cell.GetComponent<BoardCell>().Initialize(x, y, this, false, statusText);
                cell.SetActive(false); // Torna o tabuleiro do oponente invisível durante o posicionamento
            }
        }
    }

    public void PlaceShip(int x, int y)
    {
        if (currentState == GameState.PlacingShips && playerShips[x, y] == null && placedShips < shipsToPlace)
        {
            playerShips[x, y] = Instantiate(shipPrefab, new Vector3(x, 0, y), Quaternion.identity);
            playerShips[x, y].transform.SetParent(playerBoardPanel1); // Coloca o navio no painel do jogador
            placedShips++; // Incrementa o contador de navios posicionados
            UpdateStatus(); // Atualiza o status para mostrar quantos barcos faltam
        }
        else if (placedShips >= shipsToPlace)
        {
            Debug.Log("Você já posicionou todos os seus barcos!"); // Mensagem de log se o limite for atingido
        }
    }

    private void CheckIfAllShipsPlaced()
    {
        // Se o jogador posicionou todos os navios, notifique o outro jogador
        if (placedShips >= shipsToPlace)
        {
            photonView.RPC("NotifyAllShipsPlaced", RpcTarget.Others);
            statusText.text = "Começando Batalha";
            StartBattle();
        }
    }

    [PunRPC]
    private void NotifyAllShipsPlaced()
    {
        currentState = GameState.Attacking; // Muda o estado após todos os navios serem posicionados
        UpdateStatus();
        SetCellInteractable(false); // Desabilita as células do jogador após confirmar
        SetOpponentBoardVisible(true); // Torna o tabuleiro do oponente visível quando a batalha começar
    }

    private void StartBattle()
    {
        currentState = GameState.Attacking; // Muda para a fase de ataque
        UpdateStatus();
    }
    private void UpdateStatus()
    {
        switch (currentState)
        {
            case GameState.PlacingShips:
                statusText.text = $"Posicione seus navios! ({placedShips}/{shipsToPlace} posicionados)";
                break;
            case GameState.Attacking:
                statusText.text = "Hora de atacar!";
                break;
        }
    }

    // Propriedades para verificar o estado atual do jogo
    public bool CanPlaceShip => currentState == GameState.PlacingShips; // Propriedade para verificar se pode posicionar navios
    public bool CanAttack => currentState == GameState.Attacking; // Propriedade para verificar se pode atacar

    public void Attack(int x, int y)
    {
        if (currentState == GameState.Attacking)
        {
            // Lógica de ataque a ser implementada (deve-se definir o que acontece no ataque)
            Debug.Log($"Ataque na posição ({x}, {y})");

            // Aqui pode ser inserida a lógica para verificar se o tiro acertou ou errou no oponente.
            // O método `Hit` e `Miss` já estão definidos para atualizar a célula quando um ataque for feito.

            // Para fins de exemplo, vamos simular um "acerto" e "erro":
            bool isHit = Random.Range(0, 2) == 0; // Simula um ataque com 50% de chance de acerto
            if (isHit)
            {
                // Acertou o oponente
                photonView.RPC("HitOpponentCell", RpcTarget.Others, x, y);
            }
            else
            {
                // Errou
                photonView.RPC("MissOpponentCell", RpcTarget.Others, x, y);
            }
        }
    }

    // RPC para atualizar a célula do oponente quando um acerto ocorrer
    [PunRPC]
    private void HitOpponentCell(int x, int y)
    {
        Transform opponentCell = GetOpponentCell(x, y);
        if (opponentCell != null)
        {
            opponentCell.GetComponent<BoardCell>().Hit();
        }
    }

    // RPC para atualizar a célula do oponente quando um erro ocorrer
    [PunRPC]
    private void MissOpponentCell(int x, int y)
    {
        Transform opponentCell = GetOpponentCell(x, y);
        if (opponentCell != null)
        {
            opponentCell.GetComponent<BoardCell>().Miss();
        }
    }

    // Método para obter a célula correspondente no tabuleiro do oponente
    private Transform GetOpponentCell(int x, int y)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return opponentBoardPanel2.GetChild(x * gridSize + y);
        }
        else
        {
            return opponentBoardPanel1.GetChild(x * gridSize + y);
        }
    }

    // Método para habilitar ou desabilitar a interação das células
    private void SetCellInteractable(bool isInteractable)
    {
        foreach (Transform child in playerBoardPanel1)
        {
            child.GetComponent<BoardCell>().SetInteractable(isInteractable);
        }
        foreach (Transform child in playerBoardPanel2)
        {
            child.GetComponent<BoardCell>().SetInteractable(isInteractable);
        }
    }

    // Método para tornar o tabuleiro do oponente visível ou invisível
    private void SetOpponentBoardVisible(bool isVisible)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // O Host verá o tabuleiro do Player 2
            foreach (Transform child in opponentBoardPanel2)
            {
                child.gameObject.SetActive(isVisible);
            }
        }
        else
        {
            // O Client verá o tabuleiro do Player 1
            foreach (Transform child in opponentBoardPanel1)
            {
                child.gameObject.SetActive(isVisible);
            }
        }
    }

    // Método para confirmar o posicionamento de todos os navios
    public void ConfirmPlacement()
    {
        // Aqui você pode verificar se ambos os jogadores posicionaram todos os navios
        CheckIfAllShipsPlaced();
    }
}