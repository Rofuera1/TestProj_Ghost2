using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileLoader
{
    public static T ReadJSON<T>(string path) where T:class
    {
        if (File.Exists(path))
        {
            string fileContents = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(fileContents);
        }
        else
        {
            return null;
        }
    }

    public static void WriteJSON<T>(T file, string path) where T: class
    {
        string json = JsonUtility.ToJson(file);

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(json);
        }
    }

    public static void SaveText(string text, string path)
    {
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(text);
        }
    }

    public static void SaveFile<T>(T fileObj, string fileName) where T : class
    {
        string path = Application.persistentDataPath + "/" +  fileName;
        FileStream file;

        if (File.Exists(path)) file = File.OpenWrite(path);
        else file = File.Create(path);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, fileObj);
        file.Close();
    }

    public static T ReadFromFile<T>(string fileName) where T: class
    {
        string path = Application.persistentDataPath + "/" + fileName;
        FileStream file;

        if (File.Exists(path)) file = File.OpenRead(path);
        else
        {
            Debug.LogError("File not found");
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        T data = (T)bf.Deserialize(file);
        file.Close();
        return data;
    }
}
