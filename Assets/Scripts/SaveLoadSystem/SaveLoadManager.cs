using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveLoadSystem
{
    public static class SaveLoadManager
    {
        //almacena la informacion del juego actual
        public static PlayerData currentSaveData = new PlayerData();
        //direccion de la carpeta donde se guardan los datos 
        public const string SaveDirectory = "/SaveData";
        //nombre del archivo 
        public const string fileName = "/SaveGame.txt";

        public static bool Save()
        {
            // Se crea la ruta de la carpeta de guardado
            string dir = Application.persistentDataPath + SaveDirectory;
            if (!Directory.Exists(dir))             //Si no existe la carpeta se crea
                Directory.CreateDirectory(dir);
            string json = JsonUtility.ToJson(currentSaveData, true); //convierte la informacion del juego en formato JSON

            //guarda la informacion en un archivo de texto dentro de la carpeta de guardado
            File.WriteAllText(dir + fileName, json);
            GUIUtility.systemCopyBuffer = dir; //copia la ruta de la carpeta en el portapapeles del sistema

            return true;
        }

        public static bool Load()
        {
            //crea la ruta completa del archivo de guardado
            string fullPath = Application.persistentDataPath + SaveDirectory + fileName;
            // Si el archivo existe se carga la informacion
            if (File.Exists(fullPath))
            {
                //lee la informacion del archivo y se convierte a PlayerData
                string json = File.ReadAllText(fullPath);
                currentSaveData = JsonUtility.FromJson<PlayerData>(json);
                return true;
            }
            else
            {
                Debug.LogError("Save file not found.");
                return false;
            }
        }
    }
}