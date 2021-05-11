using System.IO;
using UnityEngine;

public class JsonSaveLoadSystem : ISaveLoadSystem
{
    public T Load<T>(string path) where T : class
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);

            return JsonUtility.FromJson<T>(data);
        }

        return null;
    }

    public void Save<T>(string path, T data) where T : class
    {
        File.WriteAllText(path, JsonUtility.ToJson(data));

        Debug.Log($"saved at {path}");
    }
}
