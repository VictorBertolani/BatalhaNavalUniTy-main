using UnityEngine;
using UnityEngine.SceneManagement;  // Necessário para carregar cenas
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EscolhaDeBarcos : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Quando o botão for clicado, troca para a cena 'EscolhaBarcos'
        SceneManager.LoadScene("EscolhaBarcos");
    }
}
