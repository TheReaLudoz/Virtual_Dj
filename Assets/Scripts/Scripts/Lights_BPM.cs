using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Button3D changeLights;
    public Light _Light;
    public float Time;
    public bool BPM;
    public float BPMValue;
    private Coroutine flashingCoroutine;

    // Start is called before the first frame update
    void Start()

    {
        changeLights.OnButtonPressed += OnChangeLightsButtonPressed;
        _Light = GetComponent<Light>();
        StartFlashing();

    }

    void Update()
    {

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