using KnobsAsset;
using UnityEngine;

public class MusicPlayTutorialTransition : MonoBehaviour
{
    public GameObject Emessive;
    public TutorialManager tutorialManager;
    public AudioSource musicaAudioSource;
    private Color initialColor;
    public Color redColor;

    private bool isMusicaInRiproduzione = false;
    private float posizioneDiRiproduzione = 0f;

    void Start()
    {
        initialColor=Emessive.GetComponent<Renderer>().material.color;

        if (musicaAudioSource != null && musicaAudioSource.isPlaying)
        {
            musicaAudioSource.Stop();
            isMusicaInRiproduzione = false;
            Emessive.GetComponent<Renderer>().material.color=redColor;
        }
    }

    void OnMouseDown()
    {
        if (musicaAudioSource != null)
        {
            if (isMusicaInRiproduzione)
            {
                posizioneDiRiproduzione = musicaAudioSource.time;
                musicaAudioSource.Stop();
                isMusicaInRiproduzione = false;
                Emessive.GetComponent<Renderer>().material.color=initialColor;
                
            }
            else
            {
                 Emessive.GetComponent<Renderer>().material.color=redColor;
                musicaAudioSource.time = posizioneDiRiproduzione;
                musicaAudioSource.Play();
                isMusicaInRiproduzione = true;
                tutorialManager.ActionCompleted(TutorialManager.TutorialAction.MusicPlay);
            }
        }
    }

}
