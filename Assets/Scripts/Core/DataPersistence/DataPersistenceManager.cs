using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Strage ConFig")]
    [SerializeField] private string fileName;

    private GameData gameData;
    public static DataPersistenceManager instance { get; private set; }
    private List <IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public bool saveOnQuit = true;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one DataPersistence Manager in system");
        }
        instance = this;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistencObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No Data was found. GameData would be set to default values.");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }

    public bool CheckGameDataExist()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No Data was found. GameData would be set to default values.");
            return false;
        }
        return true;
    }


    public bool SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
        return true;
    }

    //maybe I should change this to when click button
    //but this... on App Quit
    private void OnApplicationQuit()
    {
        if (saveOnQuit)
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistencObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
        
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
