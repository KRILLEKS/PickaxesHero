using UnityEngine;

public class OreDurability : MonoBehaviour
{
    [SerializeField] private OreStats oreStats;

    // global variables
    private GridBehavior _gridBehavior;
    private ProgressBar progressBar;
    private SingleExtractedOresCounter extractedOresCounter;
    private ChracterAnimatorController animatorController;

    // local variables
    private float durability = 100f;
    private float armor = 10f;
    private int index;


    private void Awake()
    {
        _gridBehavior = FindObjectOfType<GridBehavior>();
        progressBar = FindObjectOfType<ProgressBar>();
        extractedOresCounter = FindObjectOfType<SingleExtractedOresCounter>();
        animatorController = FindObjectOfType<ChracterAnimatorController>();

        durability = oreStats.durability;
        armor = oreStats.armor;
        index = oreStats.index;
        gameObject.GetComponent<SpriteRenderer>().sprite = oreStats.sprites[Random.Range(0, oreStats.sprites.Length)];
    }

    // Decrease ore health
    public void TakeDamage(float damage)
    {
        durability -= damage - armor;

        if (durability <= 0)
            DestroyOre(true);
    }

    public void DestroyOre(bool getResource)
    {
        if (getResource)
        {
            extractedOresCounter.GetOre(index);
            animatorController.InvertIsMiningVar();
            progressBar.IncreaseValue();
        }

        Destroy(gameObject);
        _gridBehavior.InstantiateNewGridPrefab(transform.position);
    }
}