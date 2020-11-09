using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveSystem
{
  public static void Save(string path, object data)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string savePath = Path.Combine(Application.persistentDataPath, path + ".save");
    FileStream fileStream = new FileStream(savePath, FileMode.Create);

    formatter.Serialize(fileStream, data);

    fileStream.Close();
  }
}
