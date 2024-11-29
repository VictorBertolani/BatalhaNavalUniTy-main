using UnityEngine;
using UnityEngine.UI; // Para manipular a interface do usuário
using UnityEngine.EventSystems; // Para lidar com os eventos de clique

public class AparecerBotaoEscolha : MonoBehaviour, IPointerClickHandler
{
    // Referência para o botão SelecaoBoneco
    public GameObject selecaoBoneco;

    // Função chamada quando o botão for clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        // Verifica se a referência para SelecaoBoneco está atribuída
        if (selecaoBoneco != null)
        {
            // Torna o botão SelecaoBoneco visível
            selecaoBoneco.SetActive(true);
        }
    }
}
