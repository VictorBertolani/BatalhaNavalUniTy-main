using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEgito : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image buttonImage;
    private Color originalColor;
    private bool isButtonVisible = false; // Controla a visibilidade do bot�o ap�s o clique

    // Refer�ncia aos outros bot�es
    public GameObject[] outrosBotoes;

    void Start()
    {
        // Encontra o GameObject com o nome "EgitoBotao" e pega o componente Image
        GameObject egitoBotao = GameObject.Find("EgitoBotao");

        if (egitoBotao == null)
        {
            Debug.LogError("Bot�o com o nome 'EgitoBotao' n�o foi encontrado!");
            return;
        }

        buttonImage = egitoBotao.GetComponent<Image>();

        if (buttonImage == null)
        {
            Debug.LogError("O bot�o 'EgitoBotao' n�o possui o componente Image!");
            return;
        }

        originalColor = buttonImage.color;
        SetButtonVisibility(false);

        // Inicializa a lista de outros bot�es
        outrosBotoes = GameObject.FindGameObjectsWithTag("Botao");
    }

    private void SetButtonVisibility(bool isVisible)
    {
        if (buttonImage != null)
        {
            Color color = originalColor;
            color.a = isVisible ? 1f : 0f;  // 1f = totalmente vis�vel, 0f = invis�vel
            buttonImage.color = color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isButtonVisible)
        {
            SetButtonVisibility(true);  // Mostra o bot�o
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isButtonVisible)
        {
            SetButtonVisibility(false);  // Esconde o bot�o
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isButtonVisible = !isButtonVisible;
        SetButtonVisibility(isButtonVisible);
        ToggleOtherButtonsVisibility();

        PlayerPrefs.SetInt("IndexPersonagem", 2);
        PlayerPrefs.Save();  // Salva o valor para que possa ser usado em outra cena
        Debug.Log("IndexPersonagem alterado para: " + PlayerPrefs.GetInt("IndexPersonagem"));
    }

    private void ToggleOtherButtonsVisibility()
    {
        foreach (var botao in outrosBotoes)
        {
            if (botao.name != gameObject.name)
            {
                Image buttonImage = botao.GetComponent<Image>();
                if (buttonImage != null)
                {
                    Color color = buttonImage.color;
                    color.a = 0f;  // Torna o bot�o invis�vel
                    buttonImage.color = color;
                }
            }
        }
    }
}
