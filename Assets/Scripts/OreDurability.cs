using UnityEngine;

public class OreDurability : MonoBehaviour
{
  [SerializeField]
  private OreStats oreStats;

  // global variables
  private GridBehavior gridBehavior;
  private ProgressBar progressBar;
  private ExtractedOresCounter extractedOresCounter;

  // local variables
  private float durability = 100f;
  private float armor = 10f;


  private void Awake()
  {
    gridBehavior = FindObjectOfType<GridBehavior>();
    progressBar = FindObjectOfType<ProgressBar>();
    extractedOresCounter = FindObjectOfType<ExtractedOresCounter>();

    durability = oreStats.durability;
    armor = oreStats.armor;
    gameObject.GetComponent<SpriteRenderer>().sprite = oreStats.sprites[Random.Range(0, oreStats.sprites.Length)];
  }

  // Decrease ore health
  public void TakeDamage(float damage)
  {
    durability -= damage - armor;

    if (durability <= 0)
      DestroyOre(true);
  }

  public void DestroyOre(bool getResourse)
  {
    if (getResourse)
      extractedOresCounter.GetOre(gameObject.name);

    Destroy(gameObject);
    gridBehavior.InstantiateNewGridPrefab(transform.position);
    progressBar.IncreaseValue();
  }
}
