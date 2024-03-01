using UnityEngine;
using TMPro;

public class TutorialComponent : MonoBehaviour
{
    public TMP_Text messageText;
    public string[] messages;
    public Canvas[] associatedCanvases; // Array di Canvas associati per mostrare il testo
    public AudioSource audioSource; // AudioSource associabile dall'Inspector
    public AudioClip[] audioClips; // Array di AudioClip associabili dall'Inspector

    private int currentMessageIndex = -1;
    private bool isMessageDisplayed = false;
    private float messageSpeed = 25f; // Velocità di scrittura del messaggio (caratteri al secondo)
    private float nextCharTime = 0f; // Tempo del prossimo carattere del messaggio
    private int currentCharIndex = 0; // Indice del carattere corrente del messaggio

    void Start()
    {
        ShowNextMessage();
    }

    void Update()
    {
        // Mostra il messaggio corrente e il Canvas associato quando viene premuto il tasto 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowNextMessage();
        }

        // Scrive il messaggio uno carattere alla volta se il messaggio è attualmente visualizzato
        if (isMessageDisplayed && Time.time > nextCharTime)
        {
            if (currentCharIndex < messages[currentMessageIndex].Length)
            {
                messageText.text += messages[currentMessageIndex][currentCharIndex];
                currentCharIndex++;
                nextCharTime = Time.time + 1f / messageSpeed;
            }
        }
    }

    public void ShowNextMessage()
    {
        // Nasconde il messaggio precedente
        if (currentMessageIndex >= 0)
        {
            messageText.text = "";
        }

        currentMessageIndex++;
        // Mostra il messaggio corrente
        if (currentMessageIndex < messages.Length)
        {
            messageText.text = "";
            currentCharIndex = 0;
            messageText.gameObject.SetActive(true);
            isMessageDisplayed = true;
            nextCharTime = Time.time + 1f / messageSpeed;

            // Riproduci l'AudioClip associato
            if (currentMessageIndex < audioClips.Length && audioSource != null)
            {
                audioSource.clip = audioClips[currentMessageIndex];
                audioSource.Play();
            }
        }
        else
        {
            // Se non ci sono più messaggi, resetta l'indice del messaggio
            currentMessageIndex = -1;
            messageText.gameObject.SetActive(false);
            isMessageDisplayed = false;
        }

        // Nasconde i Canvas precedenti
        for (int i = 0; i < associatedCanvases.Length; i++)
        {
            if (i != currentMessageIndex)
            {
                associatedCanvases[i].gameObject.SetActive(false);
            }
            else
            {
                associatedCanvases[i].gameObject.SetActive(true);
            }
        }
    }
}