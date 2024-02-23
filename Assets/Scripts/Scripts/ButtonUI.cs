using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button3D))]
public class ButtonUI : Interactable
{
    private Button3D button3D;
    public Button uiButton; // Assicurati di assegnare l'oggetto Button UI dall'editor Unity

    // Use this for initialization
    void Start()
    {
        button3D = GetComponent<Button3D>();
        if (uiButton == null)
        {
            Debug.LogError("Assicurati di assegnare l'oggetto Button UI dall'editor Unity.");
        }
    }

    public override void Interact(GameObject caller)
    {
        button3D.Press();
        Debug.Log("Bottone Premuto");

        // Attiva il bottone UI
        if (uiButton != null)
        {
            uiButton.onClick.Invoke();
        }
    }
}
