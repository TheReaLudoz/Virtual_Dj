using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;



[System.Serializable]
public class IlluminationData
{   
    [SerializeField]
    public GameObject cable;

    [SerializeField]
    public TupleData tupleData1;

    [SerializeField]
    public TupleData tupleData2;
}

[System.Serializable]
public class TupleData
{
    [SerializeField]
    public GameObject gameObject;

    public AudioClip audioClipCable;

    [SerializeField]
    public Canvas canvas;

    [SerializeField]
    public Canvas cableCanva;

    public AudioClip audioClipMain;

    [SerializeField]
    public Canvas mainCanva;

    [SerializeField]
    public GameObject cablePlugged;

    [SerializeField] 
    public AudioSource audioSource = null;
}



public class IlluminationController : MonoBehaviour
{
    public List<IlluminationData> illuminationDataList = new List<IlluminationData>();
    private int cablesPluggedCount = 0;
    private Dictionary<GameObject, (TupleData, TupleData)> cableDictionary = new Dictionary<GameObject, (TupleData, TupleData)>();
    private TupleData usedTupleData;
    private GameObject grabbedCable;
    public delegate void AllCableGrabbedDelegate();
    public static event AllCableGrabbedDelegate OnAllCableGrabbed;


    private void OnEnable()
    {
        FPSInteractionManager.OnCableGrabbed += OnCableGrabbed;
        ObjectInteraction.OnObjectPressed += OnObjectPressed;
    }

    private void OnDisable()
    {
        FPSInteractionManager.OnCableGrabbed -= OnCableGrabbed;
        ObjectInteraction.OnObjectPressed -= OnObjectPressed;
    }

    private void Start()
    {
        foreach (var illuminationData in illuminationDataList)
        {
            cableDictionary.Add(illuminationData.cable, (illuminationData.tupleData1, illuminationData.tupleData2));
            illuminationData.tupleData1.mainCanva.gameObject.SetActive(true);
            illuminationData.tupleData1.audioSource.PlayOneShot(illuminationData.tupleData1.audioClipMain);
        }
    }


    private void OnCableGrabbed(GameObject cable)
    {
        if (cableDictionary.TryGetValue(cable, out var tupleData))
        {
            grabbedCable = cable;
            
            tupleData.Item1.audioSource.Stop();

            tupleData.Item1.canvas.gameObject.SetActive(true);
            tupleData.Item2.canvas.gameObject.SetActive(true);
            tupleData.Item1.cableCanva.gameObject.SetActive(true);
            
            tupleData.Item1.audioSource.PlayOneShot(tupleData.Item1.audioClipCable);
            
            tupleData.Item1.mainCanva.gameObject.SetActive(false);
        }
    }

    private void OnObjectPressed(GameObject pressedObject)
    {
        HashSet<GameObject> processedCables = new HashSet<GameObject>();
        
        // Cerca tutte le tuple che contengono pressedObject come uno dei loro elementi
        var tuplesContainingPressedObject = cableDictionary
            .Where(kv => (kv.Value.Item1.gameObject == pressedObject || kv.Value.Item2.gameObject == pressedObject)
                          && kv.Key == grabbedCable)
            .ToList();

        foreach (var kvp in tuplesContainingPressedObject)
        {
            var tupleContainingPressedObject = kvp.Value;

            // Disattiva il Canvas associato all'oggetto premuto
            if (tupleContainingPressedObject.Item1.gameObject == pressedObject)
            {
                tupleContainingPressedObject.Item1.canvas.gameObject.SetActive(false);
            }
            else if (tupleContainingPressedObject.Item2.gameObject == pressedObject)
            {
                tupleContainingPressedObject.Item2.canvas.gameObject.SetActive(false);
            }

            // Se entrambi i Canvas associati a un cable sono disattivati
            if (!tupleContainingPressedObject.Item1.canvas.gameObject.activeSelf && !tupleContainingPressedObject.Item2.canvas.gameObject.activeSelf)
            {
                // Se il cable non è già stato processato
                if (!processedCables.Contains(kvp.Key))
                {
                    // Aumenta il conteggio dei cables collegati
                    cablesPluggedCount++;

                    // Aggiungi il cable al set dei cavi processati
                    processedCables.Add(kvp.Key);

                    // Memorizza quale TupleData è stata utilizzata
                    TupleData currentTupleData = tupleContainingPressedObject.Item1;

                    // Attiva il nuovo cavo_plugged
                    currentTupleData.cablePlugged.SetActive(true);

                    currentTupleData.cableCanva.gameObject.SetActive(false);

                    currentTupleData.mainCanva.gameObject.SetActive(true);
                    
                    //currentTupleData.audioSource.PlayOneShot(currentTupleData.audioClipMain);

                    // Se tutti i cables sono collegati, passa alla scena successiva
                    if (cablesPluggedCount == illuminationDataList.Count)
                    {
                        currentTupleData.mainCanva.gameObject.SetActive(false);
                        
                        // Qui inserisci la logica per passare alla scena successiva
                        Debug.Log("Passa alla scena successiva!");
                        OnAllCableGrabbed?.Invoke();
                    }

                    // Distruggi il cable corrente
                    Object.Destroy(kvp.Key);

                    // Esci dal ciclo dopo aver elaborato la prima chiave
                    break;
                }
            }
        }
    }
}
