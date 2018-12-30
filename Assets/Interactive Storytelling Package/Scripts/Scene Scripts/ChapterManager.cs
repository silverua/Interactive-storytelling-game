using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ChapterManager
{
    public static ChapterAsset CurrentChapter { get; private set; }
    public static ChoiceAsset[] AllChoices { get; private set; }

    /// <summary>
    /// Storage for inventory items or stats
    /// </summary>
    public static Dictionary<ItemAsset, int> Inventory { get; private set; }

    public static void SetChapter(ChapterAsset asset)
    {
        CurrentChapter = asset;
        AllChoices = Resources.LoadAll<ChoiceAsset>("");
        // TODO: load the items that we already have on this level:
        Inventory = new Dictionary<ItemAsset, int>();
        // TODO: include some data about default conversations based on previous chapter:
        ResetAllInteractions(); // TEMP
    }

    private static void ResetAllInteractions()
    {
        foreach (var element in CurrentChapter.AllInteractiveElements)
            element.CurrentInteractionIndex = 0;
    }

    public static void SetChapter(string chapterName)
    {
        var chapters = Resources.LoadAll<ChapterAsset>("");
        var chaptersWithThisName = (from c in chapters where c.name == chapterName select c).ToList();
        
        if (chaptersWithThisName.Count == 0)
        {
            Debug.Log("Chapter with name: " + chapterName + " not found in project");
            return;
        }

        if (chaptersWithThisName.Count > 1)
        {
            Debug.LogWarning("Multiple chapters found with name: " + chapterName +
                             " The first one will be selected, check naming.");
        }

        SetChapter(chaptersWithThisName[0]);
    }

    // negative amount == remove item.
    public static void AddOrRemoveItem(ItemAsset item, int amountToAdd)
    {
        if (!Inventory.ContainsKey(item))
        {
            Inventory.Add(item, Mathf.Max(0, amountToAdd));
            return;
        }

        Inventory[item] = Mathf.Max(0, Inventory[item] + amountToAdd);
    }

    private static ChoiceAsset GetChoiceWithName(string choiceName)
    {
        foreach (var choice in AllChoices)
            if (choice.name == choiceName)
                return choice;
        return null;
    }

    public static void InteractWith(string interactiveElementAssetName, string choiceName)
    {
        if (CurrentChapter == null)
        {
            Debug.LogError(
                "Current chapter was not set in ChapterManager and we are trying to interact with element: " +
                interactiveElementAssetName);
            return;
        }

        var element = CurrentChapter.GetElementByName(interactiveElementAssetName);

        if (element == null)
        {
            Debug.LogError("Element not found!!!");
            return;
        }

        InteractWith(element, GetChoiceWithName(choiceName));
    }

    private static void InteractWith(InteractiveElementAsset asset, ChoiceAsset choice)
    {
        asset.Interact(choice);
    }
}