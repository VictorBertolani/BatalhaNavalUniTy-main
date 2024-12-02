using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

public class ButtonSovietico : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image buttonImage;
    private Color originalColor;
    private bool isButtonVisible = false; // Controla a visibilidade do botão após o clique

    // Referência aos outros botões
    public GameObject[] outrosBotoes;

    void Start()
    {
        // Encontra o GameObject com o nome "RussoBotao" e pega o componente Image
        GameObject russoBotao = GameObject.Find("RussoBotao");

        if (russoBotao == null)
        {
            Debug.LogError("Botão com o nome 'RussoBotao' não foi encontrado!");
            return;
        }

        buttonImage = russoBotao.GetComponent<Image>();

        if (buttonImage == null)
        {
            Debug.LogError("O botão 'RussoBotao' não possui o componente Image!");
            return;
        }

        originalColor = buttonImage.color;
        SetButtonVisibility(false);

        // Inicializa a lista de outros botões
        outrosBotoes = GameObject.FindGameObjectsWithTag("Botao");
    }

    private void SetButtonVisibility(bool isVisible)
    {
        if (buttonImage != null)
        {
            Color color = originalColor;
            color.a = isVisible ? 1f : 0f;  // 1f = totalmente visível, 0f = invisível
            buttonImage.color = color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isButtonVisible)
        {
            SetButtonVisibility(true);  // Mostra o botão
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isButtonVisible)
        {
            SetButtonVisibility(false);  // Esconde o botão
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isButtonVisible = !isButtonVisible;
        SetButtonVisibility(isButtonVisible);
        ToggleOtherButtonsVisibility();

        PlayerPrefs.SetInt("IndexPersonagem", 3);
        PlayerPrefs.Save();  // Salva o valor para que possa ser usado em outra cena
        Debug.Log("IndexPersonagem alterado para: " + PlayerPrefs.GetInt("IndexPersonagem"));
        PhotonNetwork.LoadLevel("Game");
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
                    color.a = 0f;  // Torna o botão invisível
                    buttonImage.color = color;
                }
            }
        }
    }
}
