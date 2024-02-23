using UnityEngine;
using TMPro;
using KnobsAsset;

public class ButtonBPMSetter : MonoBehaviour
{
    [SerializeField] private float minBPM = 60f; // Imposta il valore predefinito del minimo BPM
    [SerializeField] private BPMModifierKnobListener bpmModifier; // Riferimento allo script BPMModifierKnobListener

    // Metodo chiamato quando il bottone viene cliccato
    public void OnButtonClick()
    {
        // Assicurati che lo script BPMModifierKnobListener sia collegato
        if (bpmModifier != null)
        {
            // Imposta il BPM iniziale nel listener
            bpmModifier.SetInitialBPM(minBPM);
            
        }
        else
        {
            Debug.LogWarning("BPMModifierKnobListener non è collegato a questo script.");
        }
    }
}
