using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Mono.Cecil;
using System;

public class FileManager
{
    public static void SaveText(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
    }
    public static string LoadText(string filePath)
    {
        return File.ReadAllText(filePath);
    }

    public static void SaveBinary<T>(string filePath, T data)
    {
        using (FileStream fs = File.Create(filePath))
        {
            BinaryFormatter format = new BinaryFormatter();
            format.Serialize(fs, data);
            fs.Close();
        }
    }
    public static T LoadBinary<T>(string filePath)
    {
        T data = default;
        if(File.Exists(filePath))
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                BinaryFormatter format = new BinaryFormatter();
                data = (T)format.Deserialize(fs);
                fs.Close();
            }
        }return data;
    }

    public static void SaveBinaryArray<T>(string filePath, T[] datas)
    {
        using(FileStream fs = File.Create(filePath))
        {
            BinaryFormatter format = new BinaryFormatter();
            format.Serialize(fs, datas.Length);
            foreach(T data in datas)
            {
                format.Serialize(fs, data);
            }
            fs.Close();
        }
    }

    public static T[] LoadBinaryArray<T>(string filePath)
    {
        T[] datas = default;
        if(File.Exists(filePath))
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                BinaryFormatter format = new BinaryFormatter();
                int Count = (int)format.Deserialize(fs);
                datas = new T[Count];
                for(int i = 0; i < Count; i++)
                {
                    datas[i] = (T)format.Deserialize(fs);
                }
                fs.Close();
            }
        }
        return datas;
    }
}
