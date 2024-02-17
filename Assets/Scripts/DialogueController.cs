using UnityEngine;
using TMPro;

public class InteractiveObject : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    public Color standardColor = Color.white;
    public GameObject interactionCanvas;
    public TMP_Text messageText;
    public GameObject interactionHintCanvas; // Nuovo canvas per il messaggio di hint
    public TMP_Text interactionHintText; // Nuovo testo per il messaggio di hint

    [SerializeField]
    private string[] messages;

    private int currentMessageIndex = -1;
    private Material originalMaterial;
    private bool isMouseOver = false;

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        interactionCanvas.SetActive(false);
        interactionHintCanvas.SetActive(false); // Assicura che il canvas di hint sia disattivato all'avvio
    }

    void Update()
    {
        if (isMouseOver && (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (currentMessageIndex < messages.Length - 1)
            {
                currentMessageIndex++;
                DisplayMessage(messages[currentMessageIndex]);
            }
            else
            {
                interactionCanvas.SetActive(false);
                currentMessageIndex = -1;
            }
        }
    }

    void OnMouseOver()
    {
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material.color = highlightColor;
        }
        isMouseOver = true;

        // Attiva il canvas di hint quando il mouse passa sopra all'oggetto
        interactionHintCanvas.SetActive(true);
    }

    void OnMouseExit()
    {
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material.color = standardColor;
        }
        isMouseOver = false;

        // Disattiva il canvas di hint quando il mouse esce dall'oggetto
        interactionHintCanvas.SetActive(false);
    }

    void DisplayMessage(string message)
    {
        interactionCanvas.SetActive(true);
        messageText.text = message;
    }

    // Metodo per aggiornare la lista dei messaggi e resettare l'indice corrente
    public void UpdateMessages(string[] newMessages)
    {
        messages = newMessages;
        currentMessageIndex = -1;
    }
}
