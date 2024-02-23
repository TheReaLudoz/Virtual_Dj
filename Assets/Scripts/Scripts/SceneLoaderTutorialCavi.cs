using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderTutorialCavi : MonoBehaviour
{

    public GameObject loadingPanel;
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    public GameObject cable; // Aggiunta della variabile per il GameObject del cable

    public GameObject vuoto;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(loadingPanel.transform.parent);
        
    }

    // Update is called once per frame
    void Update()
    {
        OnCableDestroyed(cable);
    
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadScene("Menu"));
        }
    }
    
    private void OnCableDestroyed(GameObject cable)
    {
        if (cable == null && !Object.ReferenceEquals(cable, null) && vuoto != null) // Verifica se il GameObject nel trigger è il playerObject
        {
            Debug.Log("Daje");
            StartCoroutine(LoadScene("TutorialComponentiConsoleProva"));
            cable = vuoto;
        }
    }
    IEnumerator LoadScene(string sceneName)
    {
        loadingPanel.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }
        StartCoroutine(LoadingScreenFadeOut(2f));
    }
    IEnumerator LoadingScreenFadeOut(float duration)
    {
        float timePassed = 0f;
        float startAlpha = canvasGroup.alpha;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, timePassed / duration);
            yield return null;
        }
        loadingPanel.SetActive(false);
        canvasGroup.alpha = 1f;
    }
    
}
