using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadSystem
{
  public static T Load<T>(string path) where T : class
  {
    string loadPath = Path.Combine(Application.persistentDataPath, path + ".save");
    if (File.Exists(loadPath))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream fileStream = new FileStream(loadPath, FileMode.Open);

      T data = formatter.Deserialize(fileStream) as T;

      fileStream.Close();

      return data;
    }
    else
    {
      Debug.LogError("Can`t load " + path);
      return null;
    }
  }
}
