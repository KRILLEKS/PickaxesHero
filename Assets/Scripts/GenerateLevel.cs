using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLevel : MonoBehaviour
{
  // global variables
  private GameObject[] levelPrefabs;
  private GameObject player;

  // local variables
  public int rand;
  private GameObject levelPrefab;
  private Tilemap barrier;
  private Vector3 spawnPoint;

  private void Awake()
  {
    levelPrefabs = Resources.LoadAll<GameObject>("LevelPrefabs");
    player = GameObject.FindGameObjectWithTag("Player");
  }

  private void SetPlayerPosition(int rand)
  {
    spawnPoint = levelPrefabs[rand].transform.GetChild(1).transform.position;
    player.transform.position = spawnPoint;
  }


  #region level Generation

  public void GenerateRandomLevelPrefab() =>
    GenerateLevelPrefab(Random.Range(0, levelPrefabs.Length), true);

  public void LoadLevelPrefab(int num) =>
      GenerateLevelPrefab(num, false);

  private void GenerateLevelPrefab(int rand, bool newLevel)
  {
    this.rand = rand;
    if (barrier != null)
    {
      Destroy(levelPrefab);
      barrier = null;
    }

    levelPrefab = Instantiate(levelPrefabs[rand], new Vector3(0, 0, 0), Quaternion.identity);
    levelPrefab.transform.SetParent(gameObject.transform);
    barrier = levelPrefab.transform.GetChild(0).GetComponent<Tilemap>();

    if (newLevel)
      SetPlayerPosition(rand);
  }

  #endregion

}