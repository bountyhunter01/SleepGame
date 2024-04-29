using System;
using System.IO;//INput OUTput
using UnityEngine;

public class PlayerData11
{
    public string name;
    public string location;//내가 저장된 맵위치
    public DateTime time;//저장한 날짜와시간
}

public class DataManager : MonoBehaviour
{
    //싱글톤은 접근하기 좋다
    public static DataManager instance;

    //PlayerData nowPlayer = new PlayerData();

    private string path;
    private string fileName = "save";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        path = Application.persistentDataPath + "/";
    }
 
    public void SaveData()
    {
      //  string data = JsonUtility.ToJson(nowPlayer);
       // File.WriteAllText(path + fileName, data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path + fileName);
        //nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
}
