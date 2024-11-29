using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoardCell : MonoBehaviour, IPointerClickHandler
{
    public Button button;
    private int x, y; // Coordenadas da célula no tabuleiro
    public BoardManager boardManager; // Referência ao BoardManager

    public void Initialize(int x, int y, BoardManager boardManager)
    {
        this.x = x;
        this.y = y;
        this.boardManager = boardManager;
        button.onClick.AddListener(OnCellClicked); // Adiciona o evento de clique
    }

    public void OnCellClicked()
    {
        // Verifica se o jogador pode posicionar navios ou atacar
        if (boardManager.CanPlaceShip)
        {
            boardManager.PlaceShip(x, y);
        }
        else if (boardManager.CanAttack)
        {
            boardManager.Attack(x, y);
        }
    }

    public void Hit()
    {
        button.GetComponent<Image>().color = Color.red; // Muda a cor para vermelho se atingido
    }

    public void Miss()
    {
        button.GetComponent<Image>().color = Color.blue; // Muda a cor para azul se não atingido
    }

    // Habilita ou desabilita o botão
    public void SetInteractable(bool isInteractable)
    {
        button.interactable = isInteractable;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCellClicked();
    }
}