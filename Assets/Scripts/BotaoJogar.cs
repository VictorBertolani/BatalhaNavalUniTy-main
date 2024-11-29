using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BotaoJogar : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Quando o bot�o for clicado, troca para a cena 'EscolhaBarcos'
        SceneManager.LoadScene("SampleScene");
    }
}
