using UnityEngine;
using System.IO;

public class SaveLoad 
{
    private static SavaData _savaData = new();


    [System.Serializable]
    public struct SavaData
    {
        public StartingDeckData StartingDeck;
    }

    public static string FilePath()
    {
        return Application.persistentDataPath + "/save" + ".save";
    }

    #region Save
    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(FilePath(), JsonUtility.ToJson(_savaData, true));
    }

    private static void HandleSaveData()
    {
        CardManager.Instance.Save(ref _savaData.StartingDeck);
    }
    #endregion

    #region Load

    public static void Load()
    {
        string saveContent = File.ReadAllText(FilePath());

        _savaData = JsonUtility.FromJson<SavaData>(saveContent);
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        CardManager.Instance.Load(ref _savaData.StartingDeck);
    }
    #endregion
}
