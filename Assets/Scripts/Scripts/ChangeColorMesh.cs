using UnityEngine;

public class CambioColoreMouse : MonoBehaviour
{
    // Colore quando il mouse è sopra il GameObject
    public Color coloreMouseSopra = Color.green;
    // Colore normale del GameObject
    private Color coloreNormale;
    // Riferimento al componente Renderer del GameObject
    private Renderer rend;

    void Start()
    {
        // Ottieni il riferimento al componente Renderer del GameObject
        rend = GetComponent<Renderer>();
        // Salva il colore normale del materiale del GameObject
        coloreNormale = rend.material.color;
    }

    // Metodo chiamato quando il cursore entra nel collider del GameObject
    void OnMouseEnter()
    {
        // Cambia il colore del materiale del GameObject al colore del mouse sopra
        rend.material.color = coloreMouseSopra;
    }

    // Metodo chiamato quando il cursore esce dal collider del GameObject
    void OnMouseExit()
    {
        // Ripristina il colore normale del materiale del GameObject
        rend.material.color = coloreNormale;
    }
}
