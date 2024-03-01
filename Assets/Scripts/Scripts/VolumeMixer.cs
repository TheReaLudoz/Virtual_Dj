using UnityEngine;

namespace KnobsAsset
{
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
            volume1 = 2f - 2*knobPercentValue;
            // Il volume per la seconda traccia sarà proporzionale alla posizione dello slider
            volume2 = 2*knobPercentValue;

            // Impostiamo i volumi per le tracce audio
            audioSource1.volume = volume1;
            audioSource2.volume = volume2;
        }
    }
}
