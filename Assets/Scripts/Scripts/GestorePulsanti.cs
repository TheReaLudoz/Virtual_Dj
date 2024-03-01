using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GestorePulsanti : MonoBehaviour
{
    [Tooltip("TextMeshProUGUI to display the current BPM")]
    public TextMeshProUGUI[] testiDeiBottoni; // Assicurati di assegnare i testi dei tuoi bottoni nell'Inspector
    private int indiceUltimoBottoneCliccato = -1;

    private void Start()
    {
        // Nascondi tutti i testi all'inizio
        NascondiTuttiTesti();
    }

    public void OnButtonClick(int indiceBottone)
    {
        // Nascondi il testo dell'ultimo bottone cliccato
        NascondiTestoUltimoBottoneCliccato();

        // Mostra il testo del nuovo bottone cliccato
        MostraTestoBottone(indiceBottone);

        // Aggiorna l'indice dell'ultimo bottone cliccato
        indiceUltimoBottoneCliccato = indiceBottone;
    }

    private void NascondiTestoUltimoBottoneCliccato()
    {
        if (indiceUltimoBottoneCliccato != -1)
        {
            testiDeiBottoni[indiceUltimoBottoneCliccato].gameObject.SetActive(false);
        }
    }

    private void MostraTestoBottone(int indiceBottone)
    {
        testiDeiBottoni[indiceBottone].gameObject.SetActive(true);
    }

    private void NascondiTuttiTesti()
    {
        foreach (TextMeshProUGUI testoBottone in testiDeiBottoni)
        {
            testoBottone.gameObject.SetActive(false);
        }
    }
}
