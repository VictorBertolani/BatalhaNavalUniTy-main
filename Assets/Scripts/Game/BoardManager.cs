using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BoardManager : MonoBehaviourPunCallbacks
{
    public GameObject cellPrefab;       // Prefab para a c�lula do tabuleiro
    public Transform playerBoardPanel1; // Painel onde o tabuleiro do jogador 1 ser� gerado
    public Transform playerBoardPanel2; // Painel onde o tabuleiro do jogador 2 ser� gerado
    public Transform opponentBoardPanel1; // Painel do oponente para o jogador 1
    public Transform opponentBoardPanel2; // Painel do oponente para o jogador 2
    public Text statusText;             // Texto para mostrar o status do jogo
    public GameObject shipPrefab;       // Prefab do navio

    private const int gridSize = 6;     // Tamanho do tabuleiro (6x6)
    private GameObject[,] playerShips = new GameObject[gridSize, gridSize]; // Navios do jogador
    private int shipsToPlace = 6;       // N�mero m�ximo de navios que cada jogador deve posicionar
    private int placedShips = 0;        // Contador de navios posicionados

    private enum GameState { PlacingShips, Attacking }
    private GameState currentState;

    private void Start()
    {
        // Verifica se o jogador � o Host ou Client
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

        currentState = GameState.PlacingShips; // Come�a na fase de posicionamento
        UpdateStatus();
        SetCellInteractable(true); // Habilita as c�lulas do jogador
        SetOpponentBoardVisible(false); // Torna o tabuleiro do oponente invis�vel durante a coloca��o dos barcos
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

        // Gerar o tabuleiro do oponente (este ser� invis�vel durante a fase de posicionamento)
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject cell = Instantiate(cellPrefab, opponentBoard);
                cell.GetComponent<BoardCell>().Initialize(x, y, this, false, statusText);
                cell.SetActive(false); // Torna o tabuleiro do oponente invis�vel durante o posicionamento
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
            Debug.Log("Voc� j� posicionou todos os seus barcos!"); // Mensagem de log se o limite for atingido
        }
    }

    private void CheckIfAllShipsPlaced()
    {
        // Se o jogador posicionou todos os navios, notifique o outro jogador
        if (placedShips >= shipsToPlace)
        {
            photonView.RPC("NotifyAllShipsPlaced", RpcTarget.Others);
            statusText.text = "Come�ando Batalha";
            StartBattle();
        }
    }

    [PunRPC]
    private void NotifyAllShipsPlaced()
    {
        currentState = GameState.Attacking; // Muda o estado ap�s todos os navios serem posicionados
        UpdateStatus();
        SetCellInteractable(false); // Desabilita as c�lulas do jogador ap�s confirmar
        SetOpponentBoardVisible(true); // Torna o tabuleiro do oponente vis�vel quando a batalha come�ar
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
            // L�gica de ataque a ser implementada (deve-se definir o que acontece no ataque)
            Debug.Log($"Ataque na posi��o ({x}, {y})");

            // Aqui pode ser inserida a l�gica para verificar se o tiro acertou ou errou no oponente.
            // O m�todo `Hit` e `Miss` j� est�o definidos para atualizar a c�lula quando um ataque for feito.

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

    // RPC para atualizar a c�lula do oponente quando um acerto ocorrer
    [PunRPC]
    private void HitOpponentCell(int x, int y)
    {
        Transform opponentCell = GetOpponentCell(x, y);
        if (opponentCell != null)
        {
            opponentCell.GetComponent<BoardCell>().Hit();
        }
    }

    // RPC para atualizar a c�lula do oponente quando um erro ocorrer
    [PunRPC]
    private void MissOpponentCell(int x, int y)
    {
        Transform opponentCell = GetOpponentCell(x, y);
        if (opponentCell != null)
        {
            opponentCell.GetComponent<BoardCell>().Miss();
        }
    }

    // M�todo para obter a c�lula correspondente no tabuleiro do oponente
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

    // M�todo para habilitar ou desabilitar a intera��o das c�lulas
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

    // M�todo para tornar o tabuleiro do oponente vis�vel ou invis�vel
    private void SetOpponentBoardVisible(bool isVisible)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // O Host ver� o tabuleiro do Player 2
            foreach (Transform child in opponentBoardPanel2)
            {
                child.gameObject.SetActive(isVisible);
            }
        }
        else
        {
            // O Client ver� o tabuleiro do Player 1
            foreach (Transform child in opponentBoardPanel1)
            {
                child.gameObject.SetActive(isVisible);
            }
        }
    }

    // M�todo para confirmar o posicionamento de todos os navios
    public void ConfirmPlacement()
    {
        // Aqui voc� pode verificar se ambos os jogadores posicionaram todos os navios
        CheckIfAllShipsPlaced();
    }
}