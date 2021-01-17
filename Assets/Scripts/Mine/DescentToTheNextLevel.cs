using UnityEngine;

public class DescentToTheNextLevel : MonoBehaviour
{
  // global variables
  private GameObject player;
  private GameObject descentPrefab;
  private GridBehavior gridBehavior;
  private CsGlobal csGlobal;
  private OreGenerator oreGenerator;
  private GenerateLevel generateLevel;
  private ProgressBar progressBar;

  // local variables
  public bool descentWasSpawned = false;
  public bool needsToLoadNextLevel = false;
  public GameObject descent;

  private void Awake()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    descentPrefab = Resources.Load("Descent") as GameObject;
    gridBehavior = FindObjectOfType<GridBehavior>();
    csGlobal = FindObjectOfType<CsGlobal>();
    oreGenerator = FindObjectOfType<OreGenerator>();
    generateLevel = FindObjectOfType<GenerateLevel>();
    progressBar = FindObjectOfType<ProgressBar>();
  }

  #region descent spawning

  public void LoadDescent(DescentData descentData)
  {
    if (!descentData.descentWasSpawned)
      return;

    descent = Instantiate(descentPrefab, new Vector3(descentData.position[0], descentData.position[1], descentData.position[2]), Quaternion.identity);

    descentWasSpawned = true;
  }

  public void SpawnDescent()
  {
    progressBar.DisableProgressBar();

    DestroyOres();

    // TODO: Spawn a descent based on player`s rotation
    descent = Instantiate(descentPrefab, Vector3Int.FloorToInt(player.transform.position) + new Vector3(.5f, .5f) + new Vector3(1, 0), Quaternion.identity);
    gridBehavior.RemoveArrayElement(player.transform.position + new Vector3(1, 0));
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

    void DestroyOre(int x, int y)
    {
      Vector3 offset = new Vector3(x, y);
      Collider2D hitInfo = Physics2D.OverlapPoint(player.transform.position + offset);

      if (hitInfo && hitInfo.tag == "Ore")
        hitInfo.GetComponent<OreDurability>().DestroyOre(false);
    }
  }

  #endregion

  #region nextLevel

  // Invokes on player`s touch
  public void ChecksItThereADescent()
  {
    if (descentWasSpawned)
    {
      Collider2D hitInfo = Physics2D.OverlapPoint(csGlobal.g_mousePosition);
      if (hitInfo && hitInfo.tag == "Descent")
        needsToLoadNextLevel = true;
      else
        needsToLoadNextLevel = false;
    }
  }

  // Invokes on player`s stop
  public void LoadNextLevel()
  {
    if (needsToLoadNextLevel)
    {
      // TODO: there should be a transition scene
      Destroy(descent);
      descentWasSpawned = false;
      needsToLoadNextLevel = false;

      generateLevel.GenerateRandomLevelPrefab();

      oreGenerator.GenerateNextLevelOres();

      gridBehavior.ResetGrid();
      gridBehavior.GenerateGrid();

      progressBar.EnableProgressBar();
      progressBar.Reset();
    }
  }

  #endregion
}
