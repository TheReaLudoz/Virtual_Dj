using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class LightScript : MonoBehaviour
{
    [Tooltip("The Audio Mixer to affect a parameter on")]
    [SerializeField] private AudioSource audioSource = default;
    public Button3D changeLights;
    public Light _Light;
    public float Time;
    public bool BPM;
    public float BPMValue;
    private Coroutine flashingCoroutine;
    private float minBPM = 80f;

    private float maxBPM = 200f;
    private float GetBPMFromSpeed(float speed)
    {
        // Calcola il BPM corrispondente alla velocit� di riproduzione
        return Mathf.Lerp(minBPM, maxBPM, Mathf.InverseLerp(0.5f, 2f, speed));
    }
    // Start is called before the first frame update
    void Start()

    {
        changeLights.OnButtonPressed += OnChangeLightsButtonPressed;
        _Light = GetComponent<Light>();
        BPMValue = GetBPMFromSpeed(audioSource.pitch);
        StartFlashing();
    }

    void Update()
    {
        BPMValue = GetBPMFromSpeed(audioSource.pitch);
    }
    

    private void OnChangeLightsButtonPressed()
    {
        if (BPM)
        {
            BPM = false;
        }
        else
        {
            BPM = true;
        }
    }


    void StartFlashing()
    {
        if (flashingCoroutine != null)
        {
            StopCoroutine(flashingCoroutine);
        }

        if (BPM)
        {
            flashingCoroutine = StartCoroutine(FlashWithMusic());
        }
        else
        {
            flashingCoroutine = StartCoroutine(Flashing());
        }
    }

    IEnumerator Flashing()
    {
        while (BPM == false)
        {
            yield return new WaitForSeconds(Time);
            _Light.enabled = !_Light.enabled;
        }
        StartFlashing();

    }

    IEnumerator FlashWithMusic()
    {

        {
            yield return new WaitForSeconds(60f / BPMValue);
            _Light.enabled = !_Light.enabled;
        }
        StartFlashing();
    }
}