using UnityEngine;

public class CuePoint : MonoBehaviour
{
    // Riferimento all'AudioSource che riproduce la musica
    [SerializeField] private AudioSource musicaAudioSource;

    // Punto di partenza desiderato nella traccia audio (in secondi)
    [SerializeField] private float puntoDiPartenza = 0.0f;

    void Start()
    {
        // Assicurati che all'avvio la musica non sia in riproduzione
        if (musicaAudioSource != null && musicaAudioSource.isPlaying)
        {
            musicaAudioSource.Stop();
        }
    }

    // Metodo chiamato quando il GameObject viene cliccato
    void OnMouseDown()
    {
        // Se l'AudioSource della musica esiste
        if (musicaAudioSource != null)
        {
            // Se la musica è già in riproduzione, imposta solo il punto di partenza
            if (musicaAudioSource.isPlaying)
            {
                musicaAudioSource.time = puntoDiPartenza;
            }
            else // Altrimenti, avvia la riproduzione dalla posizione specificata
            {
                musicaAudioSource.time = puntoDiPartenza;
                musicaAudioSource.Play();
            }
        }
    }
}
