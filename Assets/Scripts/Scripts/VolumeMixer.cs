using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// Knob listener for adjusting volumes of two AudioSources based on knob position.
    /// </summary>
    public class SliderTrack : KnobListener
    {
        [Tooltip("The first AudioSource.")]
        [SerializeField] private AudioSource audioSource1;

        [Tooltip("The second AudioSource.")]
        [SerializeField] private AudioSource audioSource2;
        public float volume1;
        public float volume2;

        public override void OnKnobValueChange(float knobPercentValue)
        {
            // Calcoliamo il volume per la prima traccia proporzionale alla posizione dello slider
            volume1 = 1f - knobPercentValue;
            // Il volume per la seconda traccia sarà proporzionale alla posizione dello slider
            volume2 = knobPercentValue;

            // Impostiamo i volumi per le tracce audio
            audioSource1.volume = volume1;
            audioSource2.volume = volume2;
        }
    }
}
