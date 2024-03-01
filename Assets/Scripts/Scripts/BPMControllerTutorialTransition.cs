using TMPro;
using UnityEngine;

namespace KnobsAsset
{
    public class BPMModifierKnobListenerTutorialTransition : KnobListener
    {
        [Tooltip("The Audio Source playing the audio track")]
        [SerializeField] private AudioSource audioSource = default;

        [Tooltip("TextMeshProUGUI to display the current BPM")]
        [SerializeField] private TextMeshProUGUI bpmText = null;
        public float BPM;
        private float timer = 0f;
        private float durationThreshold = 0.41f; // 2 secondi

        public float minBPM;
        public float maxBPM;

        // Imposta il BPM iniziale
        public void SetInitialBPM(float bpm)
        {
            minBPM = bpm;
            UpdateBPMText();
            Debug.Log("Initial BPM set to: " + bpm);
        }

        private void UpdateBPMText()
        {
            float newBPM = GetBPMFromSpeed(audioSource.pitch);
            ChangeBPM(newBPM);
            if (bpmText != null)
                bpmText.text = "BPM: " + Mathf.RoundToInt(newBPM);
        }

        public void ChangeBPM(float newBPM)
        {
            BPM = Mathf.RoundToInt(newBPM);
        }

        private float GetBPMFromSpeed(float speed)
        {
            return Mathf.Lerp(minBPM, maxBPM, Mathf.InverseLerp(0.333f, 1.667f, speed));
        }

        public override void OnKnobValueChange(float knobPercentValue)
        {
            float newSpeed = Mathf.Lerp(0f, 2f, knobPercentValue);
            audioSource.pitch = newSpeed;

            UpdateBPMText();

            if (BPM == 135)
            {
                // Incrementa il timer
                timer += Time.deltaTime;
                Debug.Log(timer);
                Debug.Log(durationThreshold);
                // Se il timer raggiunge la soglia desiderata (2 secondi), esegui l'azione
                if (timer >= durationThreshold)
                {
                    // Trova l'istanza del TutorialManager e passa l'azione corrispondente
                    TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
                    if (tutorialManager != null)
                    {
                        tutorialManager.ActionCompleted(TutorialManager.TutorialAction.SetBPM);
                    }
                    else
                    {
                        Debug.LogWarning("TutorialManager not found.");
                    }

                    // Resetta il timer
                    timer = 0f;
                }
            }
            else
            {
                // Se il BPM è diverso dal valore desiderato, resetta il timer
                timer = 0f;
            }
            if (BPM==132)
            {
                // Trova l'istanza del TutorialManager e passa l'azione corrispondente
                TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
                if (tutorialManager != null)
                {
                    tutorialManager.ActionCompleted(TutorialManager.TutorialAction.RightBPM);
                }
                else
                {
                    Debug.LogWarning("TutorialManager not found.");
                }
            }
        }
    }
}

