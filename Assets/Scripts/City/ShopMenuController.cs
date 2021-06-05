using UnityEngine;
using TMPro;

public class ShopMenuController : MonoBehaviour
{
    // it`s shown ores
    [SerializeField] private GameObject pickaxeHeadContent;
    [SerializeField] private GameObject stickContent;
    [SerializeField] private GameObject hatContent;

    // global variables
    private Upgrade[] pickaxeUpgrade;
    private Upgrade[] stickUpgrade;
    private Upgrade[] hatUpgrade;

    // local variables
    [HideInInspector] public int pickaxeHeadIndex = 0;
    [HideInInspector] public int stickIndex = 0;
    [HideInInspector] public int hatIndex = 0;

    private void Awake()
    {
        pickaxeUpgrade =
            Resources.LoadAll<Upgrade>("Upgrades/pickaxeHead");
        stickUpgrade =
            Resources.LoadAll<Upgrade>("Upgrades/stick");
        hatUpgrade = Resources.LoadAll<Upgrade>("Upgrades/hat");

        SetCosts(Constants.Items.PickaxeHead);
        SetCosts(Constants.Items.Stick);
        SetCosts(Constants.Items.Hat);
    }

#region Upgrade

    public void UpgradePickaxeHead() =>
        Upgrade(pickaxeUpgrade[pickaxeHeadIndex],
            Constants.Items.PickaxeHead);

    public void UpgradeStick() =>
        Upgrade(stickUpgrade[stickIndex], Constants.Items.Stick);

    public void UpgradeHat() =>
        Upgrade(hatUpgrade[hatIndex], Constants.Items.Hat);

    private void Upgrade(Upgrade upgrade,
        Constants.Items itemToUpgrade)
    {
        if (IsUpgradable())
        {
            SubstractOres();
            ChangePlayerValues();
            SetCosts(itemToUpgrade);
        }
        else
            Debug.Log("unable to upgrade");

        void ChangePlayerValues()
        {
            switch (itemToUpgrade)
            {
                case Constants.Items.PickaxeHead:
                    SinglePlayerValues.damageValue =
                        pickaxeUpgrade[++pickaxeHeadIndex].value;
                    break;
                case Constants.Items.Stick:
                    SinglePlayerValues.miningSpeedValue =
                        stickUpgrade[++stickIndex].value;
                    break;
                case Constants.Items.Hat:
                    SinglePlayerValues.lightRadiusValue =
                        hatUpgrade[++hatIndex].value;
                    break;
                default:
                    Debug.LogError("Incorrect item to upgrade");
                    break;
            }
        }

        void SubstractOres()
        {
            for (int i = 0; i < Constants.oresAmount; i++)
                if (upgrade.cost[i] != 0)
                    SingleExtractedOresCounter.ores[i] -= upgrade.cost[i];
        }

        bool IsUpgradable()
        {
            for (int i = 0; i < Constants.oresAmount; i++)
                if (upgrade.cost[i] <= SingleExtractedOresCounter.ores[i] ==
                    false)
                    return false;

            return true;
        }
    }

#endregion

    public void SetCosts(Constants.Items item)
    {
        switch (item)
        {
            case Constants.Items.PickaxeHead:
                SetCost(pickaxeUpgrade,
                    pickaxeHeadContent,
                    pickaxeHeadIndex);
                break;
            case Constants.Items.Stick:
                SetCost(stickUpgrade, stickContent, stickIndex);
                break;
            case Constants.Items.Hat:
                SetCost(hatUpgrade, hatContent, hatIndex);
                break;
        }

        void SetCost(Upgrade[] upgrade,
            GameObject content,
            int index)
        {
            SetActiveFalse(content);

            for (int i = 0; i < Constants.oresAmount; i++)
            {
                if (upgrade[index].cost[i] != 0)
                {
                    content.transform.GetChild(i * 2).gameObject
                           .SetActive(true);
                    content.transform.GetChild(i * 2 + 1).gameObject
                           .SetActive(true);
                    content.transform.GetChild(i * 2 + 1).gameObject
                           .GetComponent<TextMeshProUGUI>().text =
                        upgrade[index].cost[i].ToString();
                }
            }
        }
    }

    private void SetActiveFalse(GameObject content)
    {
        for (int i = 0; i < Constants.oresAmount * 2; i++)
        {
            content.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void LoadIndexes(ShopMenuData shopMenuData)
    {
        pickaxeHeadIndex = shopMenuData.pickaxeHeadIndex;
        stickIndex = shopMenuData.stickIndex;
        hatIndex = shopMenuData.hatIndex;
    }
}