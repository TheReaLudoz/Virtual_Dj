using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// Knob listener for adjusting volumes of two AudioSources based on knob position.
    /// </summary>
    public class SliderTrackTutorialTransition : KnobListener
    {
        [Tooltip("The first AudioSource.")]
        [SerializeField] private AudioSource audioSource1;

        [Tooltip("The second AudioSource.")]
        [SerializeField] private AudioSource audioSource2;

        public override void OnKnobValueChange(float knobPercentValue)
        {
            // Calcoliamo il volume per la prima traccia proporzionale alla posizione dello slider
            float volume1 = 2f - 2*knobPercentValue;
            // Il volume per la seconda traccia sarà proporzionale alla posizione dello slider
            float volume2 = 2*knobPercentValue;

            // Impostiamo i volumi per le tracce audio
            audioSource1.volume = volume1;
            audioSource2.volume = volume2;

            // Conferma l'azione completata quando knobPercentValue raggiunge 1
            if (knobPercentValue == 0f)
            {
                // Trova l'istanza del TutorialManager e passa l'azione corrispondente
                TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
                if (tutorialManager != null)
                {
                    tutorialManager.ActionCompleted(TutorialManager.TutorialAction.SetMixerSx);
                }
                else
                {
                    Debug.LogWarning("TutorialManager not found.");
                }
            }
            if (knobPercentValue == 1f)
            {
                // Trova l'istanza del TutorialManager e passa l'azione corrispondente
                TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
                if (tutorialManager != null)
                {
                    tutorialManager.ActionCompleted(TutorialManager.TutorialAction.SetMixerDx);
                }
                else
                {
                    Debug.LogWarning("TutorialManager not found.");
                }
            }
            
        }
    }
}
