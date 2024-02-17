using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    public Material outlineMaterial; // Materiale per il contorno
    public Color highlightColor;
    public GameObject interactionCanvas;
    public TMP_Text interactionText;

    [TextArea(3, 10)] // Permette l'inserimento di testo su più righe nell'Inspector di Unity
    public string defaultInteractionText = "Benvenuto! Questo è un esempio di interazione.";

    private Material originalMaterial;
    private Renderer objectRenderer;
    private bool isMouseOver = false;

    public float typingSpeed = 0.05f; // Velocità di scrittura del testo

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        objectRenderer = GetComponent<Renderer>();
        interactionCanvas.SetActive(false); // Assicura che il canvas sia disattivato all'avvio
    }

    void Update()
    {
        if (isMouseOver)
        {
            if (Input.GetMouseButtonDown(0)) // Controllo se è stato premuto il pulsante sinistro del mouse
            {
                // Avvia l'interazione solo se il canvas non è già attivo
                if (!interactionCanvas.activeSelf)
                {
                    interactionCanvas.SetActive(true);
                    StartCoroutine(TypeText(defaultInteractionText));
                }
            }
        }
    }

    void OnMouseOver()
    {
        isMouseOver = true;
        // Aggiunge il contorno all'oggetto quando il mouse è sopra di esso
        objectRenderer.material = outlineMaterial;
        interactionText.text = "Click sinistro per parlare"; // Imposta il testo di interazione
    }

    void OnMouseExit()
    {
        isMouseOver = false;
        // Rimuove il contorno e ripristina il materiale originale quando il mouse non è più sopra l'oggetto
        objectRenderer.material = originalMaterial;
        interactionText.text = ""; // Rimuove il testo di interazione
    }

    // Metodo per impostare il testo di interazione
    public void SetInteractionText(string text)
    {
        interactionText.text = text;
    }

    // Coroutine per scrivere il testo gradualmente
    IEnumerator TypeText(string text)
    {
        interactionText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            interactionText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
