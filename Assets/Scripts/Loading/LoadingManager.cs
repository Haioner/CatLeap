using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private string sceneName;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneName);
        gameLevel.allowSceneActivation = false;

        while (!gameLevel.isDone)
        {
            progressBar.value = Mathf.Clamp01(gameLevel.progress / 0.9f);

            if (gameLevel.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                gameLevel.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
