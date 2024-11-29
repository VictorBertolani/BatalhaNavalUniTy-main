using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Cell : MonoBehaviour
{
    private int x, y;
    private bool hasShip = false;
    private Button button;
    private GameBoard board; // Refer�ncia ao tabuleiro

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

    // M�todo para setar a refer�ncia ao tabuleiro
    public void SetBoard(GameBoard board)
    {
        this.board = board;
    }

    void OnCellClicked()
    {
        // Chame o m�todo de ataque do GameManager
        GameManager.Instance.AttackCell(x, y);
    }

    public void HandleHit()
    {
        // L�gica para lidar com acerto
        GetComponent<Image>().color = Color.red; // Mudar a cor para vermelho
    }

    public void HandleMiss()
    {
        // L�gica para lidar com erro
        GetComponent<Image>().color = Color.blue; // Mudar a cor para azul
    }
}