using UnityEngine;

public class PlayerPrefsSaveLoadSystem : ISaveLoadSystem
{
    public T Load<T>(string key) where T : class
    {
        if (PlayerPrefs.HasKey(key))
        {
            string data = PlayerPrefs.GetString(key);

            return JsonUtility.FromJson<T>(data);
        }

        return null;
    }

    public void Save<T>(string key, T data) where T : class
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }
}
