using UnityEngine;
using UnityEngine.UI; // Para manipular a interface do usu�rio
using UnityEngine.EventSystems; // Para lidar com os eventos de clique

public class AparecerBotaoEscolha : MonoBehaviour, IPointerClickHandler
{
    // Refer�ncia para o bot�o SelecaoBoneco
    public GameObject selecaoBoneco;

    // Fun��o chamada quando o bot�o for clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        // Verifica se a refer�ncia para SelecaoBoneco est� atribu�da
        if (selecaoBoneco != null)
        {
            // Torna o bot�o SelecaoBoneco vis�vel
            selecaoBoneco.SetActive(true);
        }
    }
}
