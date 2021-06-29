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
    private NextLevelLoadController _nextLevelLoadController;
    private ProgressBar progressBar;
    private SingleExtractedOresCounter extractedOresCounter;
    private ShopMenuController _shopMenuController;
    private ChancesGenerator _chancesGenerator;

    enum Scenes
    {
        mainMenu = 0,
        city = 1,
        mine = 2
    }

    private void OnEnable()
    {
        currentScene = (Scenes) SceneManager.GetActiveScene().buildIndex;

        // Awake
        switch (currentScene)
        {
            case Scenes.mainMenu:
                GlobalStart();
                break;
            case Scenes.city:
                CityStart();
                break;
            case Scenes.mine:
                MineStart();
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

    private void GlobalStart()
    {
        extractedOresCounter = FindObjectOfType<SingleExtractedOresCounter>();
    }

    private void GlobalSave()
    {
        Debug.Log("Save global");

        SaveSystem.Save("extractedOres",
                        new ExtractedOresData(SingleExtractedOresCounter.ores));

        SaveSystem.Save("playerValues", new PlayerValuesData());

        SaveSystem.Save("sellingController", new SellingControllerData());

        SaveSystem.Save("gameManager", new GameManagerData());
        
        SaveSystem.Save("statistics", new StatisticsData());
    }

    private void GlobalLoad()
    {
        Debug.Log("Load global");

        extractedOresCounter.LoadOres(
            LoadSystem.Load<ExtractedOresData>("extractedOres"));

        SinglePlayerValues.LoadData(LoadSystem.Load<PlayerValuesData>
                                        ("playerValues"));

        SellingController.LoadSellingController(
            LoadSystem.Load<SellingControllerData>("sellingController"));

        GameManager.LoadIndex(LoadSystem.Load<GameManagerData>("gameManager"));
        
        Statistics.LoadData(LoadSystem.Load<StatisticsData>("statistics"));
    }

#endregion

#region City

    private void CityStart()
    {
        _shopMenuController = FindObjectOfType<ShopMenuController>();
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
        Bank.LoadData(LoadSystem.Load<BankData>("bank"));
    }

#endregion

#region Mine

    private void MineStart()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        generateLevel = FindObjectOfType<GenerateLevel>();
        _gridBehavior = FindObjectOfType<GridBehavior>();
        oreGenerator = FindObjectOfType<OreGenerator>();
        _nextLevelLoadController = FindObjectOfType<NextLevelLoadController>();
        progressBar = FindObjectOfType<ProgressBar>();
        _chancesGenerator = FindObjectOfType<ChancesGenerator>();
    }

    public void MineSave()
    {
        Debug.Log("Save mine");

        if (!autoSaveAndLoad)
            return;

        
        // TODO: how pros make it (scalable)
        SaveSystem.Save("player", new PlayerPositionData(playerGO));
        SaveSystem.Save("level", new LevelPrefabData(generateLevel.rand));
        SaveSystem.Save("grid", new GridData(_gridBehavior));
        SaveSystem.Save("ores", new OresData(oreGenerator));
        SaveSystem.Save("descent", new DescentData(_nextLevelLoadController));
        SaveSystem.Save("progressBar", new ProgressBarData(progressBar));
        SaveSystem.Save("chances", new ChancesData(_chancesGenerator));

        if (saveGlobalWhenSaveMineOrCity)
            GlobalSave();
    }

    private void MineLoad()
    {
        if (GameManager.previousSceneIndex != 0) // if we load not from main menu
            return;
        
        Debug.Log("Load mine");

        _chancesGenerator.LoadChances(LoadSystem.Load<ChancesData>("chances"));
        
        generateLevel.LoadLevelPrefab(LoadSystem.Load<LevelPrefabData>("level")
                                                .rand);

        PlayerPositionData playerPositionData =
            LoadSystem.Load<PlayerPositionData>("player");
        playerGO.transform.position = new Vector3(
            playerPositionData.position[0],
            playerPositionData.position[1],
            playerPositionData.position[2]);

        _gridBehavior.LoadGrid(LoadSystem.Load<GridData>("grid"));

        oreGenerator.LoadOres(LoadSystem.Load<OresData>("ores"));


        _nextLevelLoadController.LoadDescent(
            LoadSystem.Load<DescentData>("descent"));

        progressBar.LoadProgressBar(
            LoadSystem.Load<ProgressBarData>("progressBar"));
    }

#endregion
}