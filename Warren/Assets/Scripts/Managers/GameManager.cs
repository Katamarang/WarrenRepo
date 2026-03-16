using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += SceneLoad;
        SceneManager.sceneUnloaded += SceneUnloaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoad;
        SceneManager.sceneUnloaded -= SceneUnloaded;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    private void SceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        SaveLoad.Load();
    }

    private void SceneUnloaded(Scene arg0)
    {
        SaveLoad.Save();
    }

}
