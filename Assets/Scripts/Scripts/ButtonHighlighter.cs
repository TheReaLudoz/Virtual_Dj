using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlighter : MonoBehaviour
{
    private Button lastClickedButton;

    void Start()
    {
        // Trova tutti i pulsanti nel Canvas (inclusi i pulsanti nidificati nei pannelli)
        Button[] buttons = GetComponentsInChildren<Button>(true);

        // Aggiungi un listener per gestire il clic su ogni pulsante
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    private void OnButtonClick(Button clickedButton)
{
    // Disattiva l'effetto luminoso sull'ultimo pulsante cliccato
    if (lastClickedButton != null)
    {
        Text lastClickedText = lastClickedButton.GetComponentInChildren<Text>();
        if (lastClickedText != null)
        {
            lastClickedText.color = Color.white; // Cambia il colore al testo
        }

        Image lastClickedImage = lastClickedButton.image;
        if (lastClickedImage != null)
        {
            lastClickedImage.color = Color.white; // Cambia il colore al pulsante stesso
        }
    }

    // Attiva l'effetto luminoso sul pulsante cliccato
    Text clickedText = clickedButton.GetComponentInChildren<Text>();
    if (clickedText != null)
    {
        clickedText.color = Color.magenta; // Cambia il colore al testo
    }

    Image clickedImage = clickedButton.image;
    if (clickedImage != null)
    {
        clickedImage.color = Color.magenta; // Cambia il colore al pulsante stesso
    }

    // Memorizza l'ultimo pulsante cliccato
    lastClickedButton = clickedButton;
}

}