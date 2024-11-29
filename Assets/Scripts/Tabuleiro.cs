using UnityEngine;

public class Tabuleiro : MonoBehaviour
{
    public int largura = 10;  // número de colunas
    public int altura = 10;   // número de linhas
    public GameObject barcoPrefab;  // Prefab do barco para visualização no tabuleiro

    private bool[,] grade; // Matriz para armazenar o status de ocupação de cada célula

    void Start()
    {
        grade = new bool[largura, altura];
    }

    public bool PosicionarBarco(int x, int y, int tamanho, bool horizontal)
    {
        // Verifica se o barco pode ser posicionado
        if (horizontal)
        {
            if (x + tamanho > largura) return false;  // Excede os limites da grade
            for (int i = 0; i < tamanho; i++)
                if (grade[x + i, y]) return false;  // Verifica se há colisão

            // Posiciona o barco
            for (int i = 0; i < tamanho; i++)
                grade[x + i, y] = true;
        }
        else
        {
            if (y + tamanho > altura) return false;  // Excede os limites da grade
            for (int i = 0; i < tamanho; i++)
                if (grade[x, y + i]) return false;  // Verifica se há colisão

            // Posiciona o barco
            for (int i = 0; i < tamanho; i++)
                grade[x, y + i] = true;
        }

        // Exibe o barco na tela
        for (int i = 0; i < tamanho; i++)
        {
            if (horizontal)
                Instantiate(barcoPrefab, new Vector3(x + i, y, 0), Quaternion.identity);
            else
                Instantiate(barcoPrefab, new Vector3(x, y + i, 0), Quaternion.identity);
        }

        return true;
    }
}
