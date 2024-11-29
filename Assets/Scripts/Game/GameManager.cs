using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public GameBoard player1Board; // Tabuleiro do Jogador 1
    public GameBoard player2Board; // Tabuleiro do Jogador 2

    private List<PlayerShip> player1Ships = new List<PlayerShip>();
    private List<PlayerShip> player2Ships = new List<PlayerShip>();
    private int currentPlayerTurn = 0; // 0 para Jogador 1, 1 para Jogador 2

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Posicionar navios do Jogador 1
            PositionShipsForPlayer1();
        }
        else
        {
            // Posicionar navios do Jogador 2
            PositionShipsForPlayer2();
        }
    }

    public void PositionShipsForPlayer1()
    {
        photonView.RPC("RPC_PositionShipsForPlayer1", RpcTarget.All);
    }

    public void PositionShipsForPlayer2()
    {
        photonView.RPC("RPC_PositionShipsForPlayer2", RpcTarget.All);
    }

    [PunRPC]
    void RPC_PositionShipsForPlayer1()
    {
        // Lógica para o jogador 1 posicionar navios
    }

    [PunRPC]
    void RPC_PositionShipsForPlayer2()
    {
        // Lógica para o jogador 2 posicionar navios
    }

    public void AttackCell(int x, int y)
    {
        // Lógica de ataque aqui...
    }

    public void CheckForVictory()
    {
        // Lógica de verificação de vitória aqui...
    }
}