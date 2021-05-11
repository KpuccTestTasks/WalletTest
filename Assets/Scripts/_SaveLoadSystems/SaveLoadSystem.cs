public interface ISaveLoadSystem
{
    void Save<T>(string path, T data) where T : class;
    T Load<T>(string path) where T : class;
}
