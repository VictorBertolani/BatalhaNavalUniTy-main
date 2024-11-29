using UnityEngine;

public class GridDrawer : MonoBehaviour
{
    public GameObject cellPrefab;   // Prefab de célula que você vai instanciar
    public int largura = 10;        // Número de colunas
    public int altura = 10;         // Número de linhas
    public float tamanhoCelula = 1f; // Tamanho de cada célula

    void Start()
    {
        CriarGrade();
    }

    void CriarGrade()
    {
        // Percorre todas as posições na grade e instancia o prefab de célula
        for (int x = 0; x < largura; x++)
        {
            for (int y = 0; y < altura; y++)
            {
                // Calcula a posição para a célula
                Vector3 position = new Vector3(x * tamanhoCelula, y * tamanhoCelula, 0);

                // Instancia o prefab da célula
                Instantiate(cellPrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
