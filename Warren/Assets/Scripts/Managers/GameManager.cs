using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // handles scene loading and saving/loading data on scene load/unload.

    public static GameManager Instance;
    bool toDestroy;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); toDestroy = true; }

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += SceneLoad;
        SceneManager.sceneUnloaded += SceneUnloaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoad;
        SceneManager.sceneUnloaded -= SceneUnloaded;
    }


    #region change scene
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    #endregion

    private void SceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        if (toDestroy) return;

        SaveLoad.Load();
        Debug.Log("Scene loaded " + arg0.name);
    }

    private void SceneUnloaded(Scene arg0)
    {
        if (toDestroy) return;

        SaveLoad.Save();        
    }

}
