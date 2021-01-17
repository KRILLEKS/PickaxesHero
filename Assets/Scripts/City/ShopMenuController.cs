using UnityEngine;
using TMPro;

public class ShopMenuController : MonoBehaviour
{
  [SerializeField]
  private GameObject pickaxeHeadContent;
  [SerializeField]
  private GameObject stickContent;
  [SerializeField]
  private GameObject hatContent;

  // global variables
  private SingleExtractedOresCounter extractedOresCounter;
  private SinglePlayerValues playerValues;
  private UpgradeCost[] PickaxeHeadUpgradeCosts;
  private UpgradeCost[] stickUpgradeCosts;
  private UpgradeCost[] hatUpgradeCosts;
  private PickaxeDamage[] pickaxeDamageValues;
  private MiningSpeed[] miningSpeedValues;
  private LightRadius[] lightRadiusValues;

  // local variables
  private int PickaxeHeadIndex = 0;
  private int stickIndex = 0;
  private int hatIndex = 0;

  private void Awake()
  {
    extractedOresCounter = FindObjectOfType<SingleExtractedOresCounter>();

    playerValues = FindObjectOfType<SinglePlayerValues>();

    pickaxeDamageValues = Resources.LoadAll<PickaxeDamage>("Upgrades/PickaxeDamage");
    miningSpeedValues = Resources.LoadAll<MiningSpeed>("Upgrades/MiningSpeed");
    lightRadiusValues = Resources.LoadAll<LightRadius>("Upgrades/LightRadius");

    PickaxeHeadUpgradeCosts = Resources.LoadAll<UpgradeCost>("UpgradeCosts/Pickaxe head");
    stickUpgradeCosts = Resources.LoadAll<UpgradeCost>("UpgradeCosts/Stick");
    hatUpgradeCosts = Resources.LoadAll<UpgradeCost>("UpgradeCosts/Hat");

    SetCosts();
  }

  #region Upgrade

  public void UpgradePickaxeHead() =>
    Upgrade(PickaxeHeadUpgradeCosts[PickaxeHeadIndex], Constants.Items.PickaxeHead);
  public void UpgradeStick() =>
  Upgrade(stickUpgradeCosts[stickIndex], Constants.Items.Stick);
  public void UpgradeHat() =>
  Upgrade(hatUpgradeCosts[hatIndex], Constants.Items.Hat);
  private void Upgrade(UpgradeCost upgradeCost, Constants.Items itemToUpgrade)
  {
    // TODO: it`s don`t fckng work
    if (IsUpgradable())
    {
      SubstractOres();
      ChangePlayerValues();
      SetCosts();
    }
    else
      Debug.Log("unable to upgrade");

    void ChangePlayerValues()
    {
      switch (itemToUpgrade)
      {
        case Constants.Items.PickaxeHead:
          playerValues.damageValue = pickaxeDamageValues[++PickaxeHeadIndex].damageValue;
          break;
        case Constants.Items.Stick:
          playerValues.miningSpeedValue = miningSpeedValues[++stickIndex].miningSpeedValue;
          break;
        case Constants.Items.Hat:
          playerValues.lightRadiusValue = lightRadiusValues[++hatIndex].radiusValue;
          break;
        default:
          Debug.LogError("Incorrect item to upgrade");
          break;
      }
    }
    void SubstractOres()
    {
      for (int i = 0; i < Constants.oresAmount; i++)
        if (upgradeCost.ores[i] != 0)
          extractedOresCounter.ores[i] -= upgradeCost.ores[i];
    }
    bool IsUpgradable()
    {
      for (int i = 0; i < Constants.oresAmount; i++)
        if (upgradeCost.ores[i] <= extractedOresCounter.ores[i] == false)
          return false;

      return true;
    }
  }

  #endregion

  public void SetCosts()
  {
    SetCost(PickaxeHeadUpgradeCosts, pickaxeHeadContent, PickaxeHeadIndex);
    SetCost(stickUpgradeCosts, stickContent, stickIndex);
    SetCost(hatUpgradeCosts, hatContent, hatIndex);

    void SetCost(UpgradeCost[] upgradeCosts, GameObject content, int index)
    {
      for (int i = 0; i < 23; i++)
      {
        if (upgradeCosts[index].ores[i] != 0)
        {
          content.transform.GetChild(i * 2).gameObject.SetActive(true);
          content.transform.GetChild(i * 2 + 1).gameObject.SetActive(true);
          content.transform.GetChild(i * 2 + 1).gameObject.GetComponent<TextMeshProUGUI>().text = upgradeCosts[index].ores[i].ToString();
        }
      }
    }
  }
}
