using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadSceneDelay(int sceneIndex, float delay)
    {
        StartCoroutine(DelaySceneLoad(sceneIndex, delay));
    }
    public void LoadSceneDelay(string sceneName, float delay)
    {
        StartCoroutine(DelaySceneLoad(sceneName, delay));
    }
    private IEnumerator DelaySceneLoad(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(sceneIndex);
    }
    private IEnumerator DelaySceneLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(sceneName);
    }
}
