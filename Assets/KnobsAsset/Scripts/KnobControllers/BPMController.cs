using UnityEngine;
using TMPro;

namespace KnobsAsset
{
    /// <summary>
    /// Knob listener for adjusting the BPM of an audio track.
    /// </summary>
    public class BPMModifierKnobListener : KnobListener
    {
        public delegate void BPMValueChangedEventHandler(float newBPM);

        [Tooltip("The Audio Source playing the audio track")]
        [SerializeField] private AudioSource audioSource = default;
        public float BPM;
        private float minBPM;
        private float maxBPM = 200f;

        [Tooltip("TextMeshProUGUI to display the current BPM")]
        [SerializeField] private TextMeshProUGUI bpmText = null;

        // Imposta il BPM iniziale
        public void SetInitialBPM(float bpm)
        {
            minBPM = bpm;
            UpdateBPMText();
            Debug.Log("Bottone BPM SKu");
        }

        private void UpdateBPMText()
        {
            // Calcola il nuovo BPM
            float newBPM = GetBPMFromSpeed(audioSource.pitch);
            ChangeBPM(newBPM);
            // Aggiorna il testo dell'UI Text con il nuovo BPM
            if (bpmText != null)
            {
               
                bpmText.text = "BPM: " + Mathf.RoundToInt(newBPM);
            }
        }
        public void ChangeBPM(float newBPM)
        {
            BPM = Mathf.RoundToInt(newBPM);
        }

        private float GetBPMFromSpeed(float speed)
        {
            // Calcola il BPM corrispondente alla velocità di riproduzione
            return Mathf.Lerp(minBPM, maxBPM, Mathf.InverseLerp(0.5f, 2f, speed));
        }

        public override void OnKnobValueChange(float knobPercentValue)
        {
            // Calcola la nuova velocità di riproduzione basata sul valore della manopola
            float newSpeed = Mathf.Lerp(0.5f, 2f, knobPercentValue);
            audioSource.pitch = newSpeed;

            // Aggiorna il testo dell'UI Text con il nuovo BPM
            UpdateBPMText();
        }
    }
}
