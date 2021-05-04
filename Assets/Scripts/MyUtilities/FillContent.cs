using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// u should put this script in content folder
public class FillContent : MonoBehaviour
{
    // defines, what components does each ore have
    // like name, shard, shard
    public UnityEvent components;

    // local variables
    private int componentIndex; // it`s defines what component we`re changing
    private Ore[] ores = new Ore[Constants.oresAmount];

    public void Execute()
    {
        ores = Constants.GetOres();

        componentIndex = 0;

        // first we need to destroy all objects except one
        DestroyExtraObjects();

        // then we need to make as many GO duplicates as many ores we have
        MakeDuplicates();
        SetDuplicatesName();

        // then we need to change components
        components.Invoke();

        void DestroyExtraObjects()
        {
            while (transform.childCount > 1)
            {
                DestroyImmediate(transform.GetChild(1).gameObject);
            }
        }

        void MakeDuplicates()
        {
            // i=1, because we already have 1 duplicate
            for (int i = 1; i < Constants.oresAmount; i++)
            {
                var componentToDuplicate =
                    transform.GetChild(0).gameObject;

                GameObject.Instantiate(componentToDuplicate).transform
                          .SetParent(transform, false);
                // second argument help objects spawn with right scale
            }
        }

        void SetDuplicatesName()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).name = Constants.oresNames[i];
            }
        }
    }

#region Components

    public void Value()
    {
        for (var i = 0; i < Constants.oresAmount; i++)
        {
            Text("0", i);
        }

        componentIndex++;
    }

    public void Name()
    {
        for (var i = 0; i < Constants.oresAmount; i++)
        {
            Text(ores[i].name, i);
        }

        componentIndex++;
    }

    private void Text(string text,
        int childIndex)
    {
        var component = transform.GetChild(childIndex).GetChild(componentIndex)
                                 .gameObject;
        component.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void Shard()
    {
        for (var i = 0; i < Constants.oresAmount; i++)
        {
            var component = transform.GetChild(i).GetChild(componentIndex)
                                     .gameObject;
            component.GetComponent<RawImage>().texture = ores[i].shardTexture;
        }

        componentIndex++;
    }

    // Value component should be above OpenModalMenu to make it work
    public void SetOreFieldWithoutChangingIndex()
    {
        for (int i = 0; i < Constants.oresAmount; i++)
        {
            var component = transform.GetChild(i).GetChild(componentIndex)
                                     .gameObject;
            component.GetComponent<OpenModalMenu>().SetOre(ores[i]);
        }
    }

    public void SetOreBuyPriceInButton()
    {
        for (int i = 0; i < Constants.oresAmount; i++)
        {
            var component = transform.GetChild(i).GetChild(componentIndex)
                                     .GetChild(0).gameObject;
            component.GetComponent<TextMeshProUGUI>().text =
                Constants.GetOre(i).buyPrice.ToString() + " <sprite index= 0>";
        }
    }

#endregion
}