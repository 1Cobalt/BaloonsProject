using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSerial : MonoBehaviour
{
    private string SAVE_FOLDER;
    
    void Start()
    {
        SAVE_FOLDER = (Application.persistentDataPath + "/SaveData.dat");
        Debug.Log(SAVE_FOLDER);

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");
        SaveScript data = new SaveScript();

        for (uint i=0; i<30; i++)
        {
            data.levelIsCompleted[i] = StaticDataStorage.GetLevelCompleteStatus(i);
        }

        data.bulletsShot = StaticDataStorage.GetBulletsShot();
        data.baloonsDestroyed = StaticDataStorage.GetBaloonDestroyed();
        data.lvl5bossIsDestroyed = StaticDataStorage.GetBossStatus();
        data.lvl2isBeatenPerfectly = StaticDataStorage.GetLevelStatus();
        data.isSoundsOn = StaticDataStorage.GetSoundsStatus();


        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Game data saved!");
    }
    public void LoadGame()
    {
        Debug.Log("Attempt to load data...");
        if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
            SaveScript data = (SaveScript)bf.Deserialize(file);
            file.Close();

            for (uint i = 0; i < 30; i++)
            {
                StaticDataStorage.SetLevelCompleteStatus(i, data.levelIsCompleted[i]);
            }

            StaticDataStorage.SetBulletsShot(data.bulletsShot);
            StaticDataStorage.SetBaloonDestroyed(data.baloonsDestroyed);
            StaticDataStorage.SetBossStatus(data.lvl5bossIsDestroyed);
            StaticDataStorage.SetLevelStatus(data.lvl2isBeatenPerfectly);
            StaticDataStorage.SetSoundsStatus(data.isSoundsOn);

        }
        else
        {
            SaveGame();
            Debug.Log("There is no saved data");
        }
    }
}


[Serializable]
class SaveScript
{
    public bool[] levelIsCompleted = new bool[30]
    {false, false, false,false,false,false,false,false,false,false
    ,false,false,false,false,false,false,false,false,false,false
    ,false,false,false,false,false,false,false,false,false,false};

    public uint bulletsShot = 0;
    public uint baloonsDestroyed = 0;

    public bool isSoundsOn = true;

    public bool lvl5bossIsDestroyed = false;

    public bool lvl2isBeatenPerfectly = false;

}
