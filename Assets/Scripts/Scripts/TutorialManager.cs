using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace KnobsAsset
{
    public class TutorialManager : MonoBehaviour
    {
        public delegate void TransitionCompletedDelegate();
        public static event TransitionCompletedDelegate TransitionCompleted;
        
        [System.Serializable]
        public struct TutorialActionData
        {
            public TutorialAction action;
            public string message;
            public GameObject targetObject; // Aggiunta del campo per l'oggetto target
            public GameObject canvas; // Aggiunta del campo per il canvas associato al messaggio
            public AudioClip audioClip; // Aggiunta del campo per l'audioclip
        }

        public enum TutorialAction
        {
            None,
            MusicPlay,
            SetBPM,
            SetVolume,
            DecreaseEQValue,
            SetMixerSx,
            MaxVolume,
            RightBPM,
            SetMixerDx,
        }

        [Header("Initial Message")]
        [SerializeField] private string initialMessageText = "Premi E per iniziare";
        [SerializeField] private AudioClip initialMessageAudioClip = null;

        [Header("Tutorial Actions")]
        [SerializeField] private List<TutorialActionData> tutorialActions = new List<TutorialActionData>();

        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI messageText = null;
        [SerializeField] private AudioSource audioSource = null;

        private bool initialMessageShown = false; // Flag per indicare se il messaggio iniziale è stato mostrato

        // Array di canvas associati ai messaggi
        private GameObject[] messageCanvases;

        // Indice corrente del messaggio
        private int currentMessageIndex = -1;

        private void Start()
        {
            // Inizializza l'array di canvas
            InitializeMessageCanvases();
            // Visualizza il messaggio iniziale
            ShowInitialMessage();
        }

        private void Update()
        {
            // Controlla se il messaggio iniziale è stato mostrato e il tasto E è premuto
            if (initialMessageShown && Input.GetKeyDown(KeyCode.E))
            {
                // Interrompi l'audioclip del messaggio iniziale
                if (initialMessageAudioClip != null && audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                // Passa al tutorial action
                ShowNextMessage();
            }
        }

        // Inizializza l'array di canvas
        private void InitializeMessageCanvases()
        {
            messageCanvases = new GameObject[tutorialActions.Count];
            for (int i = 0; i < tutorialActions.Count; i++)
            {
                messageCanvases[i] = tutorialActions[i].canvas;
            }
        }

        // Visualizza il messaggio iniziale
        private void ShowInitialMessage()
        {
            // Visualizza il messaggio iniziale
            if (!string.IsNullOrEmpty(initialMessageText))
            {
                messageText.text = initialMessageText;
            }

            // Riproduci l'audioclip del messaggio iniziale
            if (initialMessageAudioClip != null)
            {
                audioSource.PlayOneShot(initialMessageAudioClip);
            }

            initialMessageShown = true;
        }

        // Visualizza il prossimo messaggio
        private void ShowNextMessage()
        {
            currentMessageIndex++;
            if (currentMessageIndex < tutorialActions.Count)
            {
                // Disattiva tutti i canvas
                foreach (GameObject canvas in messageCanvases)
                {
                    canvas.SetActive(false);
                }

                // Attiva il canvas corrispondente al messaggio corrente
                messageCanvases[currentMessageIndex].SetActive(true);

                // Riproduci l'audioclip del messaggio corrente
                if (tutorialActions[currentMessageIndex].audioClip != null)
                {
                    audioSource.PlayOneShot(tutorialActions[currentMessageIndex].audioClip);
                }

                // Aggiorna il testo del messaggio
                messageText.text = tutorialActions[currentMessageIndex].message;
            }
            else
            {
                // Se non ci sono più messaggi, nascondi il testo del messaggio
                messageText.text = "";

                // Aggiungi il debug qui per indicare che il tutorial è stato completato
                messageCanvases[currentMessageIndex-1].SetActive(false);
                
                TransitionCompleted?.Invoke();

                // Puoi eseguire ulteriori azioni o attivare un altro comportamento qui dopo il completamento del tutorial
            }
        }

        // Metodo per segnalare che un'azione è stata completata
        public void ActionCompleted(TutorialAction action, GameObject targetObject = null)
        {
            // Controlla se l'azione corrente corrisponde all'azione completata
            if (currentMessageIndex >= 0 && currentMessageIndex < tutorialActions.Count)
            {
                if (tutorialActions[currentMessageIndex].action == action)
                {
                    // Interrompi l'audioclip associato all'azione corrente
                    if (tutorialActions[currentMessageIndex].audioClip != null && audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }

                    // Se non è specificato un targetObject o se il targetObject corrisponde a quello associato all'azione,
                    // passa al messaggio successivo
                    if (targetObject == null || tutorialActions[currentMessageIndex].targetObject == targetObject)
                    {
                        ShowNextMessage();
                    }
                }
            }
        }
    }
    
}