using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BoardManager : MonoBehaviourPunCallbacks
{
    public GameObject cellPrefab;      // Prefab para a célula do tabuleiro
    public Transform boardPanel;        // Painel onde o tabuleiro será gerado
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
        GenerateBoard();
        currentState = GameState.PlacingShips; // Começa na fase de posicionamento
        UpdateStatus();
        SetCellInteractable(true); // Habilita as células do jogador
    }

    private void GenerateBoard()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject cell = Instantiate(cellPrefab, boardPanel);
                cell.GetComponent<BoardCell>().Initialize(x, y, this); // Inicializa a célula
            }
        }
    }

    public void PlaceShip(int x, int y)
    {
        if (currentState == GameState.PlacingShips && playerShips[x, y] == null && placedShips < shipsToPlace)
        {
            playerShips[x, y] = Instantiate(shipPrefab, new Vector3(x, 0, y), Quaternion.identity);
            playerShips[x, y].transform.SetParent(boardPanel); // Para que o navio fique na mesma hierarquia do painel
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
            StartBattle();
        }
    }

    [PunRPC]
    private void NotifyAllShipsPlaced()
    {
        currentState = GameState.Attacking; // Muda o estado após todos os navios serem posicionados
        UpdateStatus();
        SetCellInteractable(false); // Desabilita as células do jogador após confirmar
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
            // Lógica de ataque a ser implementada
        }
    }

    // Método para habilitar ou desabilitar a interação das células
    private void SetCellInteractable(bool isInteractable)
    {
        foreach (Transform child in boardPanel)
        {
            child.GetComponent<BoardCell>().SetInteractable(isInteractable);
        }
    }

    // Método para confirmar o posicionamento
    public void ConfirmPlacement()
    {
        // Aqui você pode verificar se ambos os jogadores posicionaram todos os navios
        CheckIfAllShipsPlaced();
    }
}