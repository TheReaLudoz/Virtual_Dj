using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] Senteces;
    private int Index = 0;
    public float DialogueSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
    
    void NextSentence()
    {
        if(Index < Senteces.Length - 1) 
        {
            DialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
    }
 
    IEnumerator WriteSentence()
    {
        foreach(char Character in Senteces[Index].ToCharArray())
        {
            DialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
        Index++;
    }
}
