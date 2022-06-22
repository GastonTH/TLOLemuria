using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Json_Serializer
{
    public static class SaveManager
    {
        public static void SavePlayerData(Heroe h)
        {
            string dataPath = Application.persistentDataPath + "/player.save";
            FileStream fs = new FileStream(dataPath, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fs, h);
            fs.Close();
        }
    }
}