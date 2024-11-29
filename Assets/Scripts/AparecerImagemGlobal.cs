using UnityEngine;
using UnityEngine.EventSystems; // Para detectar eventos de clique no bot�o

public class AparecerImagemGlobal : MonoBehaviour, IPointerClickHandler
{
    // Refer�ncia para o GameObject do bot�o
    public GameObject botaoToToggle;

    private bool isBotaoVisible = false;  // Controla a visibilidade do bot�o

    // Fun��o chamada quando o bot�o � clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        // Alterna o estado de visibilidade do bot�o
        isBotaoVisible = !isBotaoVisible;

        // Define a visibilidade do bot�o com base na vari�vel `isBotaoVisible`
        botaoToToggle.SetActive(isBotaoVisible);
    }
}
