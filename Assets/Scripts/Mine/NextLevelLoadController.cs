using UnityEngine;
using UnityEngine.Events;

public class NextLevelLoadController : MonoBehaviour
{
    [SerializeField] public GameObject nextLevelMenu;
    [SerializeField] private GameObject descentPrefab;
    [SerializeField] private BonusOresMenu bonusOresMenu;

    // global variables
    public UnityEvent onDescending;

    // local variables
    [HideInInspector] public bool descentWasSpawned = false;
    [HideInInspector] public GameObject descent;
    [HideInInspector] public GridBehavior _gridBehavior;
    [HideInInspector] public OreGenerator oreGenerator;
    [HideInInspector] public GenerateLevel generateLevel;
    [HideInInspector] public ProgressBar progressBar;
    private GameObject player;
    private CsGlobal csGlobal;

    // static variables
    public static bool needsToLoadNextLevel = true;

    private void Awake()
    {
        if (onDescending == null)
            new UnityEvent();

        player = GameObject.FindGameObjectWithTag("Player");
        _gridBehavior = FindObjectOfType<GridBehavior>();
        csGlobal = FindObjectOfType<CsGlobal>();
        oreGenerator = FindObjectOfType<OreGenerator>();
        generateLevel = FindObjectOfType<GenerateLevel>();
        progressBar = FindObjectOfType<ProgressBar>();
    }

    private void Start()
    {
        if ((needsToLoadNextLevel && OreGenerator.levelWasLoaded == false) ||
            GameManager.previousSceneIndex == 1)
        {
            LoadNextLevel();
        }
        else
        {
            oreGenerator.GenerateOres(false);
        }
    }

#region descent spawning

    public void LoadDescent(DescentData descentData)
    {
        if (!descentData.descentWasSpawned)
            return;

        Debug.Log("spawn descent");

        descent = Instantiate(descentPrefab,
                              new Vector3(descentData.position[0],
                                          descentData.position[1],
                                          descentData.position[2]),
                              Quaternion.identity);

        descentWasSpawned = true;
    }

    public void SpawnDescent()
    {
        if (progressBar.ReachedMaxVal())
            progressBar.DisableProgressBar();
        else
        {
            //TODO: popup "not enough progress"
            return;
        }

        DestroyOres();

        // TODO: Spawn a descent based on player`s rotation
        descent = Instantiate(descentPrefab,
                              Vector3Int.FloorToInt(player.transform.position) +
                              new Vector3(.5f, .5f) +
                              new Vector3(1, 0),
                              Quaternion.identity);
        _gridBehavior.RemoveArrayElement(
            player.transform.position + new Vector3(1, 0));

        descentWasSpawned = true;
    }

    void DestroyOres()
    {
        DestroyOre(1, 0); // destroys the block on the right
        DestroyOre(-1, 0); // destroys the block on the left
        DestroyOre(0, 1); // destroys the block at the top
        DestroyOre(0, -1); // destroys the block at the bottom
        DestroyOre(1, 1); // destroys the block in the top right corner
        DestroyOre(1, -1); // destroys the block in the bottom right corner
        DestroyOre(-1, 1); // destroys the block in the top left corner
        DestroyOre(-1, -1); // destroys the block in the bottom left corner
    }

    void DestroyOre(float x,
                    float y)
    {
        Vector3 offset = new Vector3(x, y);
        Collider2D hitInfo =
            Physics2D.OverlapPoint(player.transform.position + offset);

        if (hitInfo && hitInfo.tag == "Ore")
            hitInfo.GetComponent<OreDurability>().DestroyOre(false);
    }

#endregion

#region nextLevel

    // Invokes on player`s touch
    public void ChecksItThereADescent()
    {
        if (descentWasSpawned)
        {
            Collider2D hitInfo =
                Physics2D.OverlapPoint(csGlobal.g_mousePosition);
            if (hitInfo && hitInfo.CompareTag("Descent"))
                needsToLoadNextLevel = true;
            else
                needsToLoadNextLevel = false;
        }
    }

    // Invokes on player`s stop
    public void OpenNextLevelMenu()
    {
        if (needsToLoadNextLevel)
        {
            nextLevelMenu.SetActive(true);
            bonusOresMenu.UpdateValue();
        }
    }

    public void CheckIfNeedsToLoadNextLevel()
    {
        if (needsToLoadNextLevel)
            LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        Debug.Log("Load next level");

        onDescending.Invoke();
        // TODO: there should be a transition scene
        Destroy(descent);
        descentWasSpawned = false;
        needsToLoadNextLevel = false;
        nextLevelMenu.SetActive(false);

        generateLevel.GenerateRandomLevelPrefab();

        oreGenerator.GenerateNextLevelOres();

        _gridBehavior.ResetGrid();
        _gridBehavior.GenerateGrid();

        bonusOresMenu.UpdateValue();
        progressBar.EnableProgressBar();
    }

#endregion
}