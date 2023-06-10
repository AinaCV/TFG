using System.IO;
using UnityEngine;
using static PlayerSaveData;

namespace SaveLoadSystem
{
    public static class SaveGameManager
    {
        public static PlayerData currentSaveData = new PlayerData();
        public const string SaveDirectory = "/SaveData";
        public const string fileName = "/SaveGame.txt";

        public static bool Save() //para guardar las cosas con nombre diferentes le pasamos--> string _fileName al argumento
        {
            string dir = Application.persistentDataPath + SaveDirectory;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string json = JsonUtility.ToJson(currentSaveData, prettyPrint: true); //escribe los datos

            File.WriteAllText(path: dir + fileName, contents: json);

            GUIUtility.systemCopyBuffer = dir; //para abrir la carpeta más facil cuando guarde va a seguir la dirección del Portapapeles
            return true;
        }

        public static void Load()
        {
            string fullPath = Application.persistentDataPath + SaveDirectory + fileName;
            PlayerData tempData = new PlayerData();

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<PlayerData>(json); //recuperamos el archivo json

                //foreach (ItemData item in tempData.itemsList) //por cada item guardado añadelo a itemList
                    //Debug.LogError(item.name);
            }
            else
            {
                Debug.LogError("File doesn't exist");
            }
            currentSaveData = tempData;
        }
    }
}