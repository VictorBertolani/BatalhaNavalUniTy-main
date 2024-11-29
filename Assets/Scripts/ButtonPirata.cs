using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonPirata : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image buttonImage;
    private Color originalColor;
    private bool isButtonVisible = false; // Controla a visibilidade do bot�o ap�s o clique

    // Refer�ncia aos outros bot�es
    public GameObject[] outrosBotoes;

    void Start()
    {
        // Encontra o GameObject com o nome "PirataBotao" e pega o componente Image
        GameObject pirataBotao = GameObject.Find("PirataBotao");

        // Se o bot�o "PirataBotao" n�o for encontrado, mostramos um erro
        if (pirataBotao == null)
        {
            Debug.LogError("Bot�o com o nome 'PirataBotao' n�o foi encontrado!");
            return;
        }

        // Pega o componente Image do bot�o
        buttonImage = pirataBotao.GetComponent<Image>();

        // Se o bot�o n�o tiver o componente Image, mostramos um erro
        if (buttonImage == null)
        {
            Debug.LogError("O bot�o 'PirataBotao' n�o possui o componente Image!");
            return;
        }

        // Guarda a cor original da imagem
        originalColor = buttonImage.color;

        // Inicialmente, o bot�o estar� invis�vel (alpha = 0)
        SetButtonVisibility(false);

        // Inicializa a lista de outros bot�es
        outrosBotoes = GameObject.FindGameObjectsWithTag("Botao"); // Aqui voc� deve garantir que todos os bot�es tenham o mesmo Tag ("Botao")
    }

    // Fun��o para mostrar ou esconder o bot�o
    private void SetButtonVisibility(bool isVisible)
    {
        if (buttonImage != null)
        {
            Color color = originalColor;
            color.a = isVisible ? 1f : 0f;  // 1f = totalmente vis�vel, 0f = invis�vel
            buttonImage.color = color;
        }
    }

    // Quando o mouse entra na �rea do bot�o
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isButtonVisible) // S� exibe quando n�o est� vis�vel ap�s o clique
        {
            SetButtonVisibility(true);  // Mostra o bot�o
        }
    }

    // Quando o mouse sai da �rea do bot�o
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isButtonVisible) // S� esconde quando n�o estiver vis�vel ap�s o clique
        {
            SetButtonVisibility(false);  // Esconde o bot�o
        }
    }

    // Quando o bot�o � clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        // Alterna a visibilidade do bot�o
        isButtonVisible = !isButtonVisible;

        // Define a visibilidade do bot�o de acordo com o estado
        SetButtonVisibility(isButtonVisible);

        // Chama a fun��o para esconder os outros bot�es
        ToggleOtherButtonsVisibility();

        PlayerPrefs.SetInt("IndexPersonagem", 1);
        PlayerPrefs.Save();  // Salva o valor para que possa ser usado em outra cena
        Debug.Log("IndexPersonagem alterado para: " + PlayerPrefs.GetInt("IndexPersonagem"));
    }

    // Fun��o para esconder todos os outros bot�es
    private void ToggleOtherButtonsVisibility()
    {
        foreach (var botao in outrosBotoes)
        {
            if (botao.name != gameObject.name) // Se o bot�o n�o for o atual
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
