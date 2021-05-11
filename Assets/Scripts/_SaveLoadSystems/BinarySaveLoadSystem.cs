using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class BinarySaveLoadSystem : ISaveLoadSystem
{
    public T Load<T>(string path) where T : class
    {
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        return null;
    }

    public void Save<T>(string path, T data) where T : class
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            binaryFormatter.Serialize(stream, data);

            Debug.Log($"saved at {path}");
        }
    }
}
