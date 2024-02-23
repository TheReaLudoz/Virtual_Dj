using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    // Array di clip audio, ognuno rappresenta una traccia audio
    public AudioClip[] audioClips;

    // Metodo chiamato da un pulsante per riprodurre una traccia audio
    public void PlayAudio(int index)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            audioSource.clip = audioClips[index];

        }
        else
        {
            Debug.LogWarning("Indice di traccia audio non valido.");
        }
    }
}