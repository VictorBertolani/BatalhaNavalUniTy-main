using UnityEngine;
using UnityEngine.EventSystems; // Para detectar eventos de clique no botão

public class AparecerImagemGlobal : MonoBehaviour, IPointerClickHandler
{
    // Referência para o GameObject do botão
    public GameObject botaoToToggle;

    private bool isBotaoVisible = false;  // Controla a visibilidade do botão

    // Função chamada quando o botão é clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        // Alterna o estado de visibilidade do botão
        isBotaoVisible = !isBotaoVisible;

        // Define a visibilidade do botão com base na variável `isBotaoVisible`
        botaoToToggle.SetActive(isBotaoVisible);
    }
}
