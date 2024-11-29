using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonPirata : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image buttonImage;
    private Color originalColor;
    private bool isButtonVisible = false; // Controla a visibilidade do botão após o clique

    // Referência aos outros botões
    public GameObject[] outrosBotoes;

    void Start()
    {
        // Encontra o GameObject com o nome "PirataBotao" e pega o componente Image
        GameObject pirataBotao = GameObject.Find("PirataBotao");

        // Se o botão "PirataBotao" não for encontrado, mostramos um erro
        if (pirataBotao == null)
        {
            Debug.LogError("Botão com o nome 'PirataBotao' não foi encontrado!");
            return;
        }

        // Pega o componente Image do botão
        buttonImage = pirataBotao.GetComponent<Image>();

        // Se o botão não tiver o componente Image, mostramos um erro
        if (buttonImage == null)
        {
            Debug.LogError("O botão 'PirataBotao' não possui o componente Image!");
            return;
        }

        // Guarda a cor original da imagem
        originalColor = buttonImage.color;

        // Inicialmente, o botão estará invisível (alpha = 0)
        SetButtonVisibility(false);

        // Inicializa a lista de outros botões
        outrosBotoes = GameObject.FindGameObjectsWithTag("Botao"); // Aqui você deve garantir que todos os botões tenham o mesmo Tag ("Botao")
    }

    // Função para mostrar ou esconder o botão
    private void SetButtonVisibility(bool isVisible)
    {
        if (buttonImage != null)
        {
            Color color = originalColor;
            color.a = isVisible ? 1f : 0f;  // 1f = totalmente visível, 0f = invisível
            buttonImage.color = color;
        }
    }

    // Quando o mouse entra na área do botão
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isButtonVisible) // Só exibe quando não está visível após o clique
        {
            SetButtonVisibility(true);  // Mostra o botão
        }
    }

    // Quando o mouse sai da área do botão
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isButtonVisible) // Só esconde quando não estiver visível após o clique
        {
            SetButtonVisibility(false);  // Esconde o botão
        }
    }

    // Quando o botão é clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        // Alterna a visibilidade do botão
        isButtonVisible = !isButtonVisible;

        // Define a visibilidade do botão de acordo com o estado
        SetButtonVisibility(isButtonVisible);

        // Chama a função para esconder os outros botões
        ToggleOtherButtonsVisibility();

        PlayerPrefs.SetInt("IndexPersonagem", 1);
        PlayerPrefs.Save();  // Salva o valor para que possa ser usado em outra cena
        Debug.Log("IndexPersonagem alterado para: " + PlayerPrefs.GetInt("IndexPersonagem"));
    }

    // Função para esconder todos os outros botões
    private void ToggleOtherButtonsVisibility()
    {
        foreach (var botao in outrosBotoes)
        {
            if (botao.name != gameObject.name) // Se o botão não for o atual
            {
                Image buttonImage = botao.GetComponent<Image>();
                if (buttonImage != null)
                {
                    Color color = buttonImage.color;
                    color.a = 0f;  // Torna o botão invisível
                    buttonImage.color = color;
                }
            }
        }
    }
}
