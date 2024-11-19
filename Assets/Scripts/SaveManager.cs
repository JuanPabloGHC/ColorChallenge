using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveManager 
{
    public static void SaveData(GController controller)
    {
        GameData gameData = new GameData(controller);
        string dataPath = Application.persistentDataPath + "/game.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, gameData);
        fileStream.Close();
    }

    public static GameData LoadData()
    {
        string dataPath = Application.persistentDataPath + "/game.save";
        
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            GameData gameData = (GameData) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return gameData;
        }
        else
        {
            return null;
        }

    }
}
