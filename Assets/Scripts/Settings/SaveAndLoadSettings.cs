using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TBOB
{
    public static class SaveAndLoadSettings
    {
        public static void SaveSettings(SO_Settings set)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/settings.set";
            Debug.Log("Saving at: " + path);
            FileStream stream = new FileStream(path, FileMode.Create);
            SettingsFile data = new SettingsFile(set);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static SettingsFile LoadSettings()
        {
            string path = Application.persistentDataPath + "/settings.set";
            if (File.Exists(path))
            {
                Debug.Log("Setting file found: " + path);
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                SettingsFile data = formatter.Deserialize(stream) as SettingsFile;
                stream.Close();
                return data;
            }
            else
            {
                Debug.Log("Setting file not found");
                return null;
            }
        }
    }
}
