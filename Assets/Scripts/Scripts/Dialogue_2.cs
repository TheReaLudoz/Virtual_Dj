using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject interactionPanel;
    public GameObject dialoguePanel;
    public GameObject startMessagePanel;
    public TextMeshProUGUI startMessageText;
    public string startMessage;
    public Animator objectAnimator;
    public string[] lines;
    public float textSpeed;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public AnimationClip startAnimation; // Animazione da riprodurre all'inizio del dialogo
    public AnimationClip endAnimation; // Animazione da riprodurre alla fine del dialogo

    private int index;
    private bool inRange = false;
    private bool dialogueStarted = false;
    private bool firstLineDisplayed = false;
    private bool dialogueStartedOnce = false;
    private bool dialogueEnded = false;
    private bool allowDialogueRestart = true;
    private GameObject player;
    private CharacterController playerController;

    void Start()
    {
        interactionPanel.SetActive(false);
        dialoguePanel.SetActive(false);
        startMessagePanel.SetActive(true);
        startMessageText.text = startMessage;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<CharacterController>();
        }

        if (objectAnimator != null)
        {
            objectAnimator.enabled = false;
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && allowDialogueRestart)
        {
            if (!dialogueStarted && !dialogueEnded)
            {
                StartDialogue();
            }
            else if (dialogueStarted && !dialogueEnded)
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
            startMessagePanel.SetActive(false);
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

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (objectAnimator != null)
        {
            objectAnimator.enabled = true;
            // Avvia l'animazione all'inizio del dialogo solo se è definita
            if (startAnimation != null)
            {
                objectAnimator.Play(startAnimation.name);
            }
        }

        if (audioSource != null && audioClips != null && audioClips.Length > 0)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
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
        else
        {
            dialogueEnded = true;
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
                if (audioSource != null && audioClips != null && audioClips.Length > index)
                {
                    audioSource.clip = audioClips[index];
                    audioSource.Play();
                }
            }
            else
            {
                dialogueStarted = false;
                textComponent.gameObject.SetActive(false);
                dialoguePanel.SetActive(false);
                interactionPanel.SetActive(false);

                if (playerController != null)
                {
                    playerController.enabled = true;
                }

                if (objectAnimator != null)
                {
                    objectAnimator.enabled = true;
                    // Avvia l'animazione alla fine del dialogo solo se è definita
                    if (endAnimation != null)
                    {
                        objectAnimator.Play(endAnimation.name);
                    }
                }

                if (audioSource != null)
                {
                    audioSource.Stop();
                }

                GetComponent<Collider>().enabled = false;
                firstLineDisplayed = false;
                dialogueStartedOnce = true;
                allowDialogueRestart = false;
            }
        }
    }
}