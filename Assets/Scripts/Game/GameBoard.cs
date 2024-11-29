using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    public Button cellPrefab; // Prefab da c�lula (bot�o)
    public Button[,] cells; // Matriz de c�lulas
    public int gridSize = 10; // Tamanho do tabuleiro
    public Transform panel; // Painel onde as c�lulas ser�o adicionadas

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        cells = new Button[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Button cell = Instantiate(cellPrefab, panel);
                cell.transform.localPosition = new Vector3(x * 30, y * 30, 0); // Ajustar a posi��o
                int currentX = x; // Capturar a vari�vel
                int currentY = y; // Capturar a vari�vel
                cell.onClick.AddListener(() => OnCellClicked(currentX, currentY));
                cells[x, y] = cell; // Armazenar na matriz
            }
        }
    }

    void OnCellClicked(int x, int y)
    {
        Debug.Log($"C�lula clicada: ({x}, {y})");
        // Aqui voc� pode colocar a l�gica de posicionamento de barcos
    }
}