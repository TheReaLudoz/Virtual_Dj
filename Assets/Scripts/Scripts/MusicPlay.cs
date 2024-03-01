using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    public GameObject Emessive;
    // Riferimento all'AudioSource che riproduce la musica
    public AudioSource musicaAudioSource;


    // Indica se la musica   attualmente in riproduzione
    private bool isMusicaInRiproduzione = false;
    private Color initialColor;
    public Color redColor;

    // Memorizza la posizione di riproduzione corrente della musica
    private float posizioneDiRiproduzione = 0f;

    void Start()
    {

        initialColor=Emessive.GetComponent<Renderer>().material.color;

        
        // Assicuriamoci che all'avvio la musica non sia in riproduzione
        if (musicaAudioSource != null && musicaAudioSource.isPlaying)
        {
            musicaAudioSource.Stop();
            isMusicaInRiproduzione = false;
            Emessive.GetComponent<Renderer>().material.color=redColor;
        }
    }

    void Update()
    {
        if(!musicaAudioSource.isPlaying)
        {
            Emessive.GetComponent<Renderer>().material.color=initialColor;
        }
    }

    // Metodo chiamato quando il GameObject viene cliccato
    void OnMouseDown()
    {
        // Se l'AudioSource della musica esiste
        if (musicaAudioSource != null)
        {
            // Se la musica   attiva, interrompila e memorizza la posizione di riproduzione corrente
            if (musicaAudioSource.isPlaying)
            {
                posizioneDiRiproduzione = musicaAudioSource.time;
                musicaAudioSource.Stop();
                isMusicaInRiproduzione = false;
                Debug.Log("stoppata");
                Emessive.GetComponent<Renderer>().material.color=initialColor;
            }
            else // Altrimenti, se la musica non   attiva, iniziala e imposta la posizione di riproduzione corrente
            {
                Emessive.GetComponent<Renderer>().material.color=redColor;
                Debug.Log("partita");
                musicaAudioSource.time = posizioneDiRiproduzione;
                musicaAudioSource.Play();
                isMusicaInRiproduzione = true;
            }
        }
        
    }
}