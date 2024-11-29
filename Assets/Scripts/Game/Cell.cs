using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Cell : MonoBehaviour
{
    private int x, y;
    private bool hasShip = false;
    private Button button;
    private GameBoard board; // Referência ao tabuleiro

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCellClicked);
    }

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetShip(bool hasShip)
    {
        this.hasShip = hasShip;
    }

    public bool HasShip()
    {
        return hasShip;
    }

    // Método para setar a referência ao tabuleiro
    public void SetBoard(GameBoard board)
    {
        this.board = board;
    }

    void OnCellClicked()
    {
        // Chame o método de ataque do GameManager
        GameManager.Instance.AttackCell(x, y);
    }

    public void HandleHit()
    {
        // Lógica para lidar com acerto
        GetComponent<Image>().color = Color.red; // Mudar a cor para vermelho
    }

    public void HandleMiss()
    {
        // Lógica para lidar com erro
        GetComponent<Image>().color = Color.blue; // Mudar a cor para azul
    }
}