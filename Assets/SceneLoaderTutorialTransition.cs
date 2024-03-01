using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KnobsAsset;

public class SceneLoaderTutorialTransition : MonoBehaviour
{

    public GameObject loadingPanel;
    public CanvasGroup canvasGroup;


    private void OnEnable()
    {
        TutorialManager.TransitionCompleted += TransitionCompleted;
    }

    private void OnDisable()
    {
        TutorialManager.TransitionCompleted -= TransitionCompleted;
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
    
    private void TransitionCompleted()
    {
        StartCoroutine(DelayedLoadScene("PreLive", 5f));
    }

    IEnumerator DelayedLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(LoadScene(sceneName));
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
