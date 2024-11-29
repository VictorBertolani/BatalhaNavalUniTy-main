using UnityEngine;

public class DrawSquareOutline : MonoBehaviour
{
    void Start()
    {
        // Obtém o componente LineRenderer
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        // Define o número de pontos (5 para fechar o quadrado)
        lineRenderer.positionCount = 5;

        // Define a espessura da linha
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // Define a cor da linha
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;

        // Define os pontos para o quadrado contornado
        lineRenderer.SetPosition(0, new Vector3(-0.5f, -0.5f, 0));  // inferior esquerdo
        lineRenderer.SetPosition(1, new Vector3(0.5f, -0.5f, 0));   // inferior direito
        lineRenderer.SetPosition(2, new Vector3(0.5f, 0.5f, 0));    // superior direito
        lineRenderer.SetPosition(3, new Vector3(-0.5f, 0.5f, 0));   // superior esquerdo
        lineRenderer.SetPosition(4, new Vector3(-0.5f, -0.5f, 0));  // Fecha o quadrado (volta ao início)
    }
}