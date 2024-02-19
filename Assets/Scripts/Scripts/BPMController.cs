using UnityEngine;
using TMPro;

namespace KnobsAsset
{
    /// <summary>
    /// Knob listener for adjusting the BPM of an audio track.
    /// </summary>
    public class BPMModifierKnobListener : KnobListener
    {
        [Tooltip("The Audio Source playing the audio track")]
        [SerializeField] private AudioSource audioSource = default;

        [Tooltip("Minimum BPM value")]
        [SerializeField] private float minBPM = 60f;

        [Tooltip("Maximum BPM value")]
        [SerializeField] private float maxBPM = 180f;

        [Tooltip("TextMeshProUGUI to display the current BPM")]
        [SerializeField] private TextMeshProUGUI bpmText = null;

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

            // Calcola il nuovo BPM
            float newBPM = GetBPMFromSpeed(newSpeed);

            // Aggiorna il testo dell'UI Text con il nuovo BPM
            if (bpmText != null)
            {
                bpmText.text = "BPM: " + Mathf.RoundToInt(newBPM);
            }
        }
    }
}
