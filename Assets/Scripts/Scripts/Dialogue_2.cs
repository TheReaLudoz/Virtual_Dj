using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject panel;
    public string[] lines;
    public float textSpeed;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public Animation animation;
    public string idleAnimationName; // Nome dell'animazione di idle
    public string talkAnimationName; // Nome dell'animazione "parla" (Jump)
    private int index;
    private bool inRange = false;
    private bool dialogueStarted = false;
    private bool blockMovement = false;
    private bool firstLineDisplayed = false; // Flag per verificare se la prima riga di dialogo è stata visualizzata
    private bool dialogueStartedOnce = false; // Flag per verificare se il dialogo è già stato avviato almeno una volta

    private GameObject player;
    private CharacterController playerController;

    void Start()
    {
        textComponent.gameObject.SetActive(false);
        panel.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<CharacterController>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (animation != null)
        {
            animation.wrapMode = WrapMode.Loop;
            animation.Stop();
            if (!string.IsNullOrEmpty(idleAnimationName))
            {
                animation.Play(idleAnimationName);
            }
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
            inRange = true;
            if (!dialogueStarted)
            {
                panel.SetActive(true);
            }
            else if (!firstLineDisplayed) // Se il dialogo è in corso ma la prima riga non è stata visualizzata
            {
                panel.SetActive(false); // Disattiva il pannello
                animation.Play(idleAnimationName); // Passa all'animazione "idle"
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            if (!firstLineDisplayed)
            {
                // Disattiva il pannello solo se non è stata visualizzata la prima riga di dialogo
                panel.SetActive(false);
            }
            if (!dialogueStarted)
            {
                // Disattiva il collider solo se il dialogo non è in corso
                GetComponent<Collider>().enabled = false;
            }
            else if (!firstLineDisplayed) // Se il dialogo è in corso ma la prima riga non è stata visualizzata
            {
                animation.Play(idleAnimationName); // Passa all'animazione "idle"
            }
        }
    }

    public void StartDialogue()
    {
        dialogueStarted = true;
        index = dialogueStartedOnce ? 1 : 0; // Parti dalla seconda riga se il dialogo è già stato avviato almeno una volta
        textComponent.text = "";
        StartCoroutine(TypeLine());

        textComponent.gameObject.SetActive(true);
        panel.SetActive(true);

        blockMovement = true;

        if (index < audioClips.Length && audioClips[index] != null)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }

        if (animation != null && !string.IsNullOrEmpty(talkAnimationName))
        {
            animation.Play(talkAnimationName);
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
    }

    void NextLine()
    {
        if (textComponent.text.Length == lines[index].Length)
        {
            audioSource.Stop();

            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = "";
                StartCoroutine(TypeLine());

                if (index < audioClips.Length && audioClips[index] != null)
                {
                    audioSource.clip = audioClips[index];
                    audioSource.Play();
                }
            }
            else
            {
                dialogueStarted = false;
                textComponent.gameObject.SetActive(false);
                panel.SetActive(false);

                if (playerController != null)
                {
                    playerController.enabled = true;
                }

                if (animation != null && !string.IsNullOrEmpty(idleAnimationName))
                {
                    animation.Play(idleAnimationName);
                }

                // Disattiva il collider alla fine del dialogo
                GetComponent<Collider>().enabled = false;

                // Resetta il flag per la prima riga visualizzata per consentire la ripetizione del dialogo
                firstLineDisplayed = false;

                // Imposta il flag per indicare che il dialogo è stato avviato almeno una volta
                dialogueStartedOnce = true;
            }
        }
    }

    void LateUpdate()
    {
        if (blockMovement && index > 0)
        {
            if (playerController != null)
            {
                playerController.enabled = false;
            }
            blockMovement = false;
        }
    }
}
