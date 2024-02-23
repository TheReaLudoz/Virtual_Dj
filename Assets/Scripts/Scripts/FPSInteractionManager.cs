using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using KnobsAsset;
using UnityEngine.EventSystems;

public class FPSInteractionManager : MonoBehaviour
{
    // SerializedFields permettono di visualizzare e modificare queste variabili nell'Inspector di Unity
    [SerializeField] private Transform _fpsCameraT;
    [SerializeField] private float _interactionDistance;
    [SerializeField] private Image _target;
    [SerializeField] private CanvasController _canvasController1;
    [SerializeField] private CanvasController _canvasController2;
    [SerializeField] private CanvasController _canvasController3;


    // Dichiarazione di un delegate per gestire l'evento di afferrare il cavo
    public delegate void CableGrabbedDelegate(GameObject grabbedObject);    
    
    // Evento statico associato al delegate per notificare la presa del cavo
    public static event CableGrabbedDelegate OnCableGrabbed;

    // Riferimenti agli oggetti che il giocatore sta interagendo
    private Interactable _pointingInteractable;
    private Grabbable _pointingGrabbable;

    // Riferimento al CharacterController del giocatore
    private CharacterController _fpsController;
    private Knob _pointingKnob;

    // Punto di origine del raggio di interazione
    private Vector3 _rayOrigin;

    // Oggetto attualmente afferrato dal giocatore
    private Grabbable _grabbedObject = null;


    // Metodo chiamato all'avvio del gioco
    void Start()
    {
        // Ottieni il CharacterController del giocatore
        _fpsController = GetComponent<CharacterController>();
    }

    // Metodo chiamato ad ogni frame
    void Update()
    {
        // Calcola il punto di origine del raggio di interazione
        _rayOrigin = _fpsCameraT.position + _fpsController.radius * _fpsCameraT.forward;

        // Controlla l'interazione con gli oggetti circostanti
        CheckInteraction();

        // Aggiorna il visuale del mirino
        UpdateUITarget();
    }

    // Metodo per controllare l'interazione con gli oggetti circostanti
    private void CheckInteraction()
    {
        // Lancia un raggio in avanti dalla telecamera del giocatore
        Ray ray = new Ray(_rayOrigin, _fpsCameraT.forward);
        RaycastHit hit;

        // Se il raggio colpisce qualcosa entro la distanza di interazione
        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            // Verifica se l'oggetto colpito è interagibile
            _pointingInteractable = hit.transform.GetComponent<Interactable>();
            _pointingKnob = hit.transform.GetComponent<Knob>();

            if (_pointingInteractable)
            {
                // Se il giocatore preme il pulsante sinistro del mouse, interagisce con l'oggetto
                if (Input.GetMouseButtonDown(0))
                {
                    _pointingInteractable.Interact(gameObject);
                }
                if (_pointingInteractable.gameObject.CompareTag("Computer") && Input.GetMouseButtonDown(0))
                {
                    _canvasController1.OpenCanvas();
                }
                if (_pointingInteractable.gameObject.CompareTag("Song1") && Input.GetMouseButtonDown(0))
                {
                    _canvasController2.OpenCanvas();
                }
                if (_pointingInteractable.gameObject.CompareTag("Song2") && Input.GetMouseButtonDown(0))
                {
                    _canvasController3.OpenCanvas();
                }

            }
    

            // Verifica se l'oggetto colpito è afferrabile
            _pointingGrabbable = hit.transform.GetComponent<Grabbable>();
            if (_grabbedObject == null && _pointingGrabbable)
            {
                // Se il giocatore preme il pulsante destro del mouse, afferra l'oggetto
                if (Input.GetMouseButtonDown(1))
                {
                    _pointingGrabbable.Grab(gameObject);
                    Grab(_pointingGrabbable);
                }
            }
        }
        // Se non viene rilevato nulla, azzera i riferimenti agli oggetti interagibili
        else
        {
            _pointingInteractable = null;
            _pointingGrabbable = null;
        }
    }


    // Metodo per aggiornare il visuale del mirino in base agli oggetti interagibili
    private void UpdateUITarget()
    {
        if (_pointingInteractable)
            _target.color = Color.green;
        else if (_pointingGrabbable)
            _target.color = Color.yellow;
        else if (_pointingKnob)
            _target.color=Color.green;
        else
            _target.color = Color.red;
    }

    // Metodo per afferrare un oggetto
    private void Grab(Grabbable grabbable)
    {
        // Assegna l'oggetto afferrato
        _grabbedObject = grabbable;

        // Imposta la telecamera come genitore dell'oggetto per seguirne i movimenti
        grabbable.transform.SetParent(_fpsCameraT);

        // Imposta l'offset per simulare la distanza dell'oggetto afferrato
        Vector3 grabOffset = new Vector3(0.5f, -0.2f, 1.0f);
        grabbable.transform.localPosition = grabOffset;
        grabbable.transform.localRotation = Quaternion.identity;

        // Invoca l'evento OnCableGrabbed per notificare che il cavo è stato afferrato
        OnCableGrabbed?.Invoke(_grabbedObject.gameObject);
    }
}