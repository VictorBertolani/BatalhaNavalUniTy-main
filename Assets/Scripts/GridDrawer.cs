using UnityEngine;

public class GridDrawer : MonoBehaviour
{
    public GameObject cellPrefab;   // Prefab de c�lula que voc� vai instanciar
    public int largura = 10;        // N�mero de colunas
    public int altura = 10;         // N�mero de linhas
    public float tamanhoCelula = 1f; // Tamanho de cada c�lula

    void Start()
    {
        CriarGrade();
    }

    void CriarGrade()
    {
        // Percorre todas as posi��es na grade e instancia o prefab de c�lula
        for (int x = 0; x < largura; x++)
        {
            for (int y = 0; y < altura; y++)
            {
                // Calcula a posi��o para a c�lula
                Vector3 position = new Vector3(x * tamanhoCelula, y * tamanhoCelula, 0);

                // Instancia o prefab da c�lula
                Instantiate(cellPrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
