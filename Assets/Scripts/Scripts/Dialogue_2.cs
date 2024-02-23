using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject interactionPanel;
    public GameObject dialoguePanel;
    public GameObject objectToAnimate;
    public Animator objectAnimator;
    public string[] lines;
    public float textSpeed;
    // public AudioSource audioSource;
    // public AudioClip[] audioClips;
    // public Animation animation;
    // public string idleAnimationName;
    // public string talkAnimationName;

    private int index;
    private bool inRange = false;
    private bool dialogueStarted = false;
    private bool firstLineDisplayed = false;
    private bool dialogueStartedOnce = false;

    private GameObject player;
    private CharacterController playerController;

    void Start()
    {
        interactionPanel.SetActive(false);
        dialoguePanel.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<CharacterController>();
        }

        // Se l'Animator è presente, disattiva l'animazione all'inizio
        if (objectAnimator != null)
        {
            objectAnimator.enabled = false;
        }
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueStarted)
            {
                StartDialogue();
            }
            else
            {
                NextLine();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the interaction zone.");
            inRange = true;
            interactionPanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the interaction zone.");
            inRange = false;
            interactionPanel.SetActive(false);
            if (!firstLineDisplayed)
            {
                dialoguePanel.SetActive(false);
            }
        }
    }

    public void StartDialogue()
    {
        dialogueStarted = true;
        index = dialogueStartedOnce ? 1 : 0;
        textComponent.text = "";
        StartCoroutine(TypeLine());

        dialoguePanel.SetActive(true);
        interactionPanel.SetActive(false);

        // Disabilita il CharacterController del giocatore
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Se l'Animator è presente, disattiva l'animazione all'inizio del dialogo
        if (objectAnimator != null)
        {
            objectAnimator.enabled = false;
        }

        // Se necessario, puoi inserire qui la logica per avviare l'audio o l'animazione di idle
        // ...
    }

    IEnumerator TypeLine()
    {
        if (index < lines.Length)
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    void NextLine()
    {
        if (textComponent.text.Length == lines[index].Length)
        {
            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = "";
                StartCoroutine(TypeLine());
            }
            else
            {
                dialogueStarted = false;
                textComponent.gameObject.SetActive(false);
                dialoguePanel.SetActive(false);
                interactionPanel.SetActive(false);

                // Riattiva il CharacterController del giocatore alla fine del dialogo
                if (playerController != null)
                {
                    playerController.enabled = true;
                }

                // Riattiva l'Animator alla fine del dialogo
                if (objectAnimator != null)
                {
                    objectAnimator.enabled = true;
                    // Avvia l'animazione associata all'Animator alla fine della conversazione
                    objectAnimator.SetTrigger("EndConversation"); // Assicurati di impostare il nome del trigger nella tua animazione
                }

                // Resetta il collider alla fine del dialogo
                GetComponent<Collider>().enabled = false;
                firstLineDisplayed = false;
                dialogueStartedOnce = true;

                // Se necessario, puoi inserire qui la logica per avviare l'audio o l'animazione di idle
                // ...
            }
        }
    }
}
