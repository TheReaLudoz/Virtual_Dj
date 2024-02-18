using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    // Riferimento all'AudioSource che riproduce la musica
    public AudioSource musicaAudioSource;

    // Indica se la musica è attualmente in riproduzione
    private bool isMusicaInRiproduzione = false;

    // Memorizza la posizione di riproduzione corrente della musica
    private float posizioneDiRiproduzione = 0f;

    void Start()
    {
        // Assicuriamoci che all'avvio la musica non sia in riproduzione
        if (musicaAudioSource != null && musicaAudioSource.isPlaying)
        {
            musicaAudioSource.Stop();
            isMusicaInRiproduzione = false;
        }
    }

    // Metodo chiamato quando il GameObject viene cliccato
    void OnMouseDown()
    {
        // Se l'AudioSource della musica esiste
        if (musicaAudioSource != null)
        {
            // Se la musica è attiva, interrompila e memorizza la posizione di riproduzione corrente
            if (isMusicaInRiproduzione)
            {
                posizioneDiRiproduzione = musicaAudioSource.time;
                musicaAudioSource.Stop();
                isMusicaInRiproduzione = false;
            }
            else // Altrimenti, se la musica non è attiva, iniziala e imposta la posizione di riproduzione corrente
            {
                musicaAudioSource.time = posizioneDiRiproduzione;
                musicaAudioSource.Play();
                isMusicaInRiproduzione = true;
            }
        }
    }
}
