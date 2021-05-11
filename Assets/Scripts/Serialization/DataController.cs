using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    [SerializeField] private bool autoSaveAndLoad = true;
    [SerializeField] private bool saveGlobalWhenSaveMineOrCity = true;
    [SerializeField] private Scenes currentScene;

    // local variables
    private GameObject playerGO;
    private GenerateLevel generateLevel;
    private OreGenerator oreGenerator;
    private GridBehavior _gridBehavior;
    private DescentToTheNextLevel descentToTheNextLevel;
    private ProgressBar progressBar;
    private SingleExtractedOresCounter extractedOresCounter;
    private ShopMenuController _shopMenuController;
    private Bank bank;

    enum Scenes
    {
        mainMenu = 0,
        city = 1,
        mine = 2
    }

    private void Awake()
    {
        currentScene = (Scenes) SceneManager.GetActiveScene().buildIndex;

        // Awake
        switch (currentScene)
        {
            case Scenes.mainMenu:
                GlobalAwake();
                break;
            case Scenes.city:
                CityAwake();
                break;
            case Scenes.mine:
                MineAwake();
                break;
            default:
                Debug.LogError("ERROR");
                break;
        }

        // load
        switch (autoSaveAndLoad)
        {
            case true when currentScene == Scenes.mainMenu:
                GlobalLoad();
                break;
            case true when currentScene == Scenes.city:
                CityLoad();
                break;
            case true when currentScene == Scenes.mine:
                MineLoad();
                break;
            case true:
                Debug.LogError("ERROR");
                break;
        }
    }

    private void OnApplicationQuit()
    {
        if (autoSaveAndLoad)
        {
            switch (currentScene)
            {
                case Scenes.city:
                    CitySave();
                    break;
                case Scenes.mine:
                    MineSave();
                    break;
            }
            
            GlobalSave();
        }
    }

#region Global

    private void GlobalAwake()
    {
        extractedOresCounter = FindObjectOfType<SingleExtractedOresCounter>();
    }

    private void GlobalSave()
    {
        Debug.Log("Save global");
        
        SaveSystem.Save("extractedOres",
                        new ExtractedOresData(SingleExtractedOresCounter.ores));

        SaveSystem.Save("playerValues", new PlayerValuesData());
    }

    private void GlobalLoad()
    {
        Debug.Log("Load global");
        
        extractedOresCounter.LoadOres(
            LoadSystem.Load<ExtractedOresData>("extractedOres"));

        SinglePlayerValues.LoadData(LoadSystem.Load<PlayerValuesData>
                                        ("playerValues"));
    }

#endregion

#region City

    private void CityAwake()
    {
        _shopMenuController = FindObjectOfType<ShopMenuController>();
        bank = FindObjectOfType<Bank>();
    }

    public void CitySave()
    {
        if (!autoSaveAndLoad)
            return;
        
        Debug.Log("Save city");
        
        SaveSystem.Save("shopIndexes", new ShopMenuData(_shopMenuController));
        SaveSystem.Save("bank", new BankData());
        
        if (saveGlobalWhenSaveMineOrCity)
            GlobalSave();
    }

    private void CityLoad()
    {
        Debug.Log("Load city");
        
        _shopMenuController.LoadIndexes(
            LoadSystem.Load<ShopMenuData>("shopIndexes"));
        bank.LoadData(LoadSystem.Load<BankData>("bank"));
    }

#endregion

#region Mine

    private void MineAwake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        generateLevel = FindObjectOfType<GenerateLevel>();
        _gridBehavior = FindObjectOfType<GridBehavior>();
        oreGenerator = FindObjectOfType<OreGenerator>();
        descentToTheNextLevel = FindObjectOfType<DescentToTheNextLevel>();
        progressBar = FindObjectOfType<ProgressBar>();
    }

    public void MineSave()
    {
        Debug.Log("Save mine");
        
        if (!autoSaveAndLoad)
            return;
        
        SaveSystem.Save("player", new PlayerPositionData(playerGO));
        SaveSystem.Save("level", new LevelPrefabData(generateLevel.rand));
        SaveSystem.Save("grid", new GridData(_gridBehavior));
        SaveSystem.Save("ores", new OresData(oreGenerator));
        SaveSystem.Save("descent", new DescentData(descentToTheNextLevel));
        SaveSystem.Save("progressBar", new ProgressBarData(progressBar));
        
        if (saveGlobalWhenSaveMineOrCity)
            GlobalSave();
    }

    private void MineLoad()
    {
        Debug.Log("Load mine");
        PlayerPositionData playerPositionData =
            LoadSystem.Load<PlayerPositionData>("player");
        _gridBehavior.LoadGrid(LoadSystem.Load<GridData>("grid"));
        oreGenerator.LoadOres(LoadSystem.Load<OresData>("ores"));
        playerGO.transform.position = new Vector3(
            playerPositionData.position[0],
            playerPositionData.position[1],
            playerPositionData.position[2]);

        generateLevel.LoadLevelPrefab(LoadSystem.Load<LevelPrefabData>("level")
                                                .rand);

        descentToTheNextLevel.LoadDescent(
            LoadSystem.Load<DescentData>("descent"));

        progressBar.LoadProgressBar(
            LoadSystem.Load<ProgressBarData>("progressBar"));
    }

#endregion
    
    // TODO: make reset progress button in utilities 
}