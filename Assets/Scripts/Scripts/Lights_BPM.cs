using System.Collections;
using System.Collections.Generic;
using KnobsAsset;
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
    public float BPMValueTOT;
    private Coroutine flashingCoroutine;
    private float minBPM = 80f;
    private float maxBPM = 200f;
    [SerializeField] private BPMModifierKnobListener bpmModifier1; // Riferimento allo script BPMModifierKnobListener
    [SerializeField] private BPMModifierKnobListener bpmModifier2; // Riferimento allo script BPMModifierKnobListener
    [SerializeField] private SliderTrack Perc; // Riferimento allo script BPMModifierKnobListener
    private float BPMValue1;
    private float BPMValue2;
    private float perc1;
    private float perc2;
    private float BPMtot;


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
        BPMValue1 = bpmModifier1.BPM;
        BPMValue2 = bpmModifier2.BPM;
        perc1 = Perc.volume1;
        perc2 = Perc.volume2;
        StartFlashing();
    }

    void Update()
    {
        BPMValue1 = bpmModifier1.BPM;
        BPMValue2 = bpmModifier2.BPM;
        perc1 = Perc.volume1;
        perc2 = Perc.volume2;
        // Calcolo della media pesata
        BPMtot = ((perc1 * BPMValue1) + (perc2 * BPMValue2)) / (perc1 + perc2);

        // Output di debug per verificare il valore di BPMtot
        Debug.Log("BPMtot: " + BPMtot);
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
            yield return new WaitForSeconds(60f / BPMtot);
            _Light.enabled = !_Light.enabled;
        }
        StartFlashing();
    }
}
