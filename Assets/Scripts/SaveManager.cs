using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    private string path = Application.dataPath + "/SaveData.txt";
    private Database database;
    public static SaveManager instance;
    private void Awake()
    {
        instance = this;
        database = Resources.Load("Data/Database") as Database;
    }

    public SaveManager GetInstance()
    {
        return instance;
    }

    public void SaveData<T>(T data,string name)
    {
        ES2.Save(data, path + "?tag=" + name);
        for(int i = 0; i < database.savenames.Count; i++)
        {
            if (name == database.savenames[i])
            {
                return;
            }
        }
        database.savenames.Add(name);
    }


    public T LoadData<T>(string name)
    {
        return ES2.Load<T>(path + "?tag=" + name);
    }


}
