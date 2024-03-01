using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingPanel;
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(loadingPanel.transform.parent);
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void PlayLive()
    {
        SceneManager.LoadScene("Live");
    }

    public void QuitGame()
    {
        Application.Quit();
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
