using System;
using System.IO;//INput OUTput
using UnityEngine;

public class PlayerData11
{
    public string name;
    public string location;//���� ����� ����ġ
    public DateTime time;//������ ��¥�ͽð�
}

public class DataManager : MonoBehaviour
{
    //�̱����� �����ϱ� ����
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
