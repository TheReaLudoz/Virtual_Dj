using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IlluminationController : MonoBehaviour
{
    // Oggetti GameObject nell'editor Unity che rappresentano gli oggetti 1 e 2
    public GameObject object_1;
    public GameObject object_2;
    
    // Riferimento al nuovo oggetto da istanziare
    public GameObject replacementObject;

    // Flag che indica se object_1 è illuminato di verde
    private bool isObject_1Green = false;

    // Flag che indica se object_2 è illuminato di verde
    private bool isObject_2Green = false;

    // Riferimento al cavo grabbable
    private GameObject grabbableCable;

    // Colori iniziali degli oggetti 1 e 2
    private Color initialColorObject_1;
    private Color initialColorObject_2;

    public TextMeshProUGUI textMesh;
    public string grabbedText = "Oggetto afferrato";


    // Metodo chiamato una volta all'avvio dello script
    private void Start()
    {
        // Imposta automaticamente grabbableCable sul GameObject a cui è collegato lo script
        grabbableCable = gameObject;

        // Salva i colori iniziali degli oggetti
        initialColorObject_1 = GetObjectColor(object_1);
        initialColorObject_2 = GetObjectColor(object_2);

        textMesh.text = "Afferra il cavo Jack qui a sinistra";
    }

    // Metodo che restituisce il colore del renderer dell'oggetto
    private Color GetObjectColor(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        return renderer != null ? renderer.material.color : Color.white;
    }

    // Metodo che imposta il colore del renderer dell'oggetto
    private void SetObjectColor(GameObject obj, Color color)
    {
        // Ottieni il renderer dell'oggetto
        Renderer renderer = obj.GetComponent<Renderer>();

        // Se il renderer è presente, imposta il colore
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }

    // Metodo chiamato quando lo script diventa attivo
    private void OnEnable()
    {
        // Aggiungi il metodo OnCableGrabbed come ascoltatore all'evento OnCableGrabbed di FPSInteractionManager
        FPSInteractionManager.OnCableGrabbed += OnCableGrabbed;
        ObjectInteraction.OnObjectPressed += OnObjectPressed;

        Debug.Log("Events connected");
    }

    // Metodo chiamato quando lo script diventa inattivo
    private void OnDisable()
    {
        // Rimuovi il metodo OnCableGrabbed dagli ascoltatori dell'evento OnCableGrabbed di FPSInteractionManager
        FPSInteractionManager.OnCableGrabbed -= OnCableGrabbed;
        ObjectInteraction.OnObjectPressed -= OnObjectPressed;

        Debug.Log("Events disconnected");
    }

    // Metodo chiamato quando l'evento OnCableGrabbed di FPSInteractionManager è attivato
    public void OnCableGrabbed(GameObject grabbedObject)
    {
        // Cerca l'IlluminationController associato all'oggetto afferrato
        IlluminationController grabbedController = grabbedObject.GetComponent<IlluminationController>();

        // Quando il cavo viene afferrato, illumina gli oggetti di verde
        SetObjectColor(grabbedController.object_1, Color.green);
        SetObjectColor(grabbedController.object_2, Color.green);

        // Imposta i flag per indicare che entrambi gli oggetti sono illuminati di verde
        isObject_1Green = true;
        isObject_2Green = true;

        textMesh.text = grabbedController.grabbedText;
    }

    // Metodo chiamato quando l'evento OnObjectPressed di ObjectInteraction è attivato
    public void OnObjectPressed(GameObject pressedObject)
    {   
        // Se è stato premuto object_1 e object_1 è verde
        if (pressedObject == object_1 && isObject_1Green)
        {
            // Se object_1 è stato premuto per ultimo
            if (!isObject_2Green)
            {
                // Esegui il cambio di mesh
                ChangeMeshLogic();
            }

            // Riporta isObject_1Green a false
            isObject_1Green = false;
            // Ripristina il colore iniziale di object_1
            SetObjectColor(object_1, initialColorObject_1);
        }
        // Se è stato premuto object_2 e object_2 è verde
        else if (pressedObject == object_2 && isObject_2Green)
        {
            // Se object_2 è stato premuto per ultimo
            if (!isObject_1Green)
            {
                // Esegui il cambio di mesh
                ChangeMeshLogic();
            }

            // Riporta isObject_2Green a false
            isObject_2Green = false;
            // Ripristina il colore iniziale di object_2
            SetObjectColor(object_2, initialColorObject_2);
        }
    }

     // Metodo per il cambio di mesh
    private void ChangeMeshLogic()
    {
        // Distruggi il cavo
        Destroy(grabbableCable);

        // Istanzia il nuovo oggetto alla posizione desiderata
        Instantiate(replacementObject);

        textMesh.text = "Afferra il cavo piu' a destra";
    }
}
