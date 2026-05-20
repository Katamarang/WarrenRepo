using UnityEngine;

public class WorldStats : MonoBehaviour
{
    

    public static WorldStats Instance;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }
}
