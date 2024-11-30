using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoardCell : MonoBehaviour, IPointerClickHandler
{
    public Button button;
    private int x, y; // Coordenadas da c�lula no tabuleiro
    public BoardManager boardManager; // Refer�ncia ao BoardManager
    public bool isPlayerBoard; // Flag para indicar se � o tabuleiro do jogador
    public Text messageText; // Refer�ncia para exibir a mensagem

    public void Initialize(int x, int y, BoardManager boardManager, bool isPlayerBoard, Text messageText)
    {
        this.x = x;
        this.y = y;
        this.boardManager = boardManager;
        this.isPlayerBoard = isPlayerBoard;
        this.messageText = messageText;
        button.onClick.AddListener(OnCellClicked); // Adiciona o evento de clique
    }

    public void OnCellClicked()
    {
        // Verifica se o jogador est� tentando posicionar ou atacar
        if (isPlayerBoard) // Se for o tabuleiro do jogador
        {
            if (boardManager.CanPlaceShip)
            {
                // Muda a cor da c�lula para amarelo quando est� no modo de posicionamento
                button.GetComponent<Image>().color = Color.yellow;
                boardManager.PlaceShip(x, y);
            }
            else if (boardManager.CanAttack)
            {
                boardManager.Attack(x, y);
            }
        }
        else // Se for o tabuleiro do oponente
        {
            if (boardManager.CanAttack)
            {
                // Exibe mensagem de erro caso o jogador tente interagir com o pr�prio tabuleiro no ataque
                messageText.text = "Voc� n�o pode selecionar os barcos de seu oponente!";
                // Certifique-se de que o bot�o permane�a branco
                button.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void Hit()
    {
        button.GetComponent<Image>().color = Color.red; // Muda a cor para vermelho se atingido
    }

    public void Miss()
    {
        button.GetComponent<Image>().color = Color.blue; // Muda a cor para azul se n�o atingido
    }

    // Habilita ou desabilita o bot�o
    public void SetInteractable(bool isInteractable)
    {
        button.interactable = isInteractable;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCellClicked();
    }
}
