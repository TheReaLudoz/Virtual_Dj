using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderTutorialCavi : MonoBehaviour
{

    public GameObject loadingPanel;
    public CanvasGroup canvasGroup;


    private void OnEnable()
    {
        IlluminationController.OnAllCableGrabbed += OnAllCableGrabbed;
    }

    private void OnDisable()
    {
        IlluminationController.OnAllCableGrabbed -= OnAllCableGrabbed;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(loadingPanel.transform.parent);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadScene("Menu"));
        }
    }
    
    private void OnAllCableGrabbed()
    {
        StartCoroutine(LoadScene("TutorialComponentiConsoleProva"));
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
