using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [Tooltip("The Audio Mixer to affect a parameter on")]
    [SerializeField] private AudioMixer Mixer1 = default;
    [SerializeField] private AudioMixer Mixer2 = default;

    public Slider volumeSlider;

    private float currentVolume1 = 0.5f; 
    private float currentVolume2 = 0.5f; // Volume iniziale, puoi impostarlo come preferisci

    void Start()
    {
        volumeSlider.value = currentVolume1;
        volumeSlider.value = currentVolume2;

    }

    public void OnVolumeSliderChanged()
    {
        currentVolume1 = volumeSlider.value;
        currentVolume2 = volumeSlider.value;
        // Aggiungi qui la logica per applicare il volume al tuo sistema audio o qualsiasi altra cosa necessaria
    }

    
}
