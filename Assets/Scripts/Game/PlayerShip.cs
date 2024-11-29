using UnityEngine;

[System.Serializable]
public class PlayerShip
{
    public Vector2Int position; // Posição do navio
    public int size; // Tamanho do navio
    public bool[] hit; // Array para rastrear acertos

    public PlayerShip(Vector2Int position, int size)
    {
        this.position = position;
        this.size = size;
        hit = new bool[size]; // Inicializa o array de acertos
    }

    public bool RegisterHit(int hitIndex)
    {
        if (hitIndex < 0 || hitIndex >= size) return false;
        hit[hitIndex] = true; // Marca como atingido
        return true;
    }

    public bool IsSunk()
    {
        foreach (bool partHit in hit)
        {
            if (!partHit) return false; // Se alguma parte não foi atingida, o navio ainda está flutuando
        }
        return true; // Se todas as partes foram atingidas, o navio está afundado
    }
}