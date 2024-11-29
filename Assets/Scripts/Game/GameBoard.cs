using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    public Button cellPrefab; // Prefab da célula (botão)
    public Button[,] cells; // Matriz de células
    public int gridSize = 10; // Tamanho do tabuleiro
    public Transform panel; // Painel onde as células serão adicionadas

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
                cell.transform.localPosition = new Vector3(x * 30, y * 30, 0); // Ajustar a posição
                int currentX = x; // Capturar a variável
                int currentY = y; // Capturar a variável
                cell.onClick.AddListener(() => OnCellClicked(currentX, currentY));
                cells[x, y] = cell; // Armazenar na matriz
            }
        }
    }

    void OnCellClicked(int x, int y)
    {
        Debug.Log($"Célula clicada: ({x}, {y})");
        // Aqui você pode colocar a lógica de posicionamento de barcos
    }
}