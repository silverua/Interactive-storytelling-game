using System.Collections.Generic;
using UnityEngine;

public enum Condition
{
    None, Equal, Less, More, LessOrEqual, MoreOrEqual
}

[CreateAssetMenu]
public class InteractionAsset: ScriptableObject
{
    [TextArea(10, 20)] 
    public string Lines;
    public List<ChoiceAndConsequences> Consequences = new List<ChoiceAndConsequences>();
}

[System.Serializable]
public class ChoiceAndConsequences
{
    public ChoiceAsset Choice;
    
    [Header("Requirements")] 
    public ItemAsset Item;
    public Condition Condition;
    public int Amount;
    
    public List<Consequence> Consequences = new List<Consequence>();

    public void TriggerAllConsequences()
    {
        if (!ConditionIsFulfilled())
        {
            Debug.LogError("Condition was not fulfilled for choice: " + Choice.name);
            return;
        }

        foreach (var c in Consequences)
            c.Trigger();
    }

    private bool ConditionIsFulfilled()
    {
        var inventoryAmount = ChapterManager.Inventory.ContainsKey(Item) ? ChapterManager.Inventory[Item] : 0; 
        switch (Condition)
        {
            case Condition.None:
                return true;
            case Condition.Equal:
                return inventoryAmount == Amount;
            case Condition.Less:
                return inventoryAmount < Amount;
            case Condition.More:
                return inventoryAmount > Amount;
            case Condition.LessOrEqual:
                return inventoryAmount <= Amount;
            case Condition.MoreOrEqual:
                return inventoryAmount >= Amount;
            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }
}

[System.Serializable]
public class Consequence
{
    public InteractiveElementAsset ItemOrNpc;
    public int NewInteractionIndex;
    public ItemAsset ItemToGiveOrRemove;

    [Tooltip("Negative values remove items from the inventory")]
    public int AmountToGiveOrRemove;

    public void Trigger()
    {
        if (ItemToGiveOrRemove != null && AmountToGiveOrRemove != 0)
            ChapterManager.AddOrRemoveItem(ItemToGiveOrRemove, AmountToGiveOrRemove);
        ItemOrNpc.CurrentInteractionIndex = NewInteractionIndex;
    }
}
