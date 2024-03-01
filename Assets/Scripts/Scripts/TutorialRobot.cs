using UnityEngine;
using TMPro;

public class TutorialRobot : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public AudioSource audioSource;

    public string[] messages;
    public AudioClip[] audioClips;

    private int currentMessageIndex = 0;

    void Start()
    {
        ShowMessage(currentMessageIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentMessageIndex == 0)
        {
            currentMessageIndex = 1;
            ShowMessage(currentMessageIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentMessageIndex == 1)
        {
            currentMessageIndex = 2;
            ShowMessage(currentMessageIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && currentMessageIndex == 2)
        {
            currentMessageIndex = 3;
            ShowMessage(currentMessageIndex);
        }
        else if (Input.GetKeyDown(KeyCode.P) && currentMessageIndex == 3)
        {
            Debug.Log("Cambio scena");
            // Cambio scena o altro
        }
    }

    void ShowMessage(int index)
    {
        if (index < messages.Length && index < audioClips.Length)
        {
            messageText.text = messages[index];
            if (audioSource != null)
            {
                audioSource.clip = audioClips[index];
                audioSource.Play();
            }
        }
    }
}
