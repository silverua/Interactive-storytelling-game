using UnityEngine;
using System.Linq;
using Malee;

/// <summary>
/// An interactive element on one isolated level,
/// this might be an NPC or a lever or a door or a treasure chest, 
/// anything that player can interact with
/// </summary>
[CreateAssetMenu]
public class InteractiveElementAsset : ScriptableObject
{
    // All interactions include conversations with NPCs, trying to open a chest or a door
    [Reorderable(elementNameProperty = "name")]
    public InteractionAssetList AllInteractions;
    // this is the current state of this character/item: 
    // the interaction out of the list of all possible interactions that we get when the players approach this
    private int currentInteractionIndex = 0;
    public int CurrentInteractionIndex
    {
        get { return currentInteractionIndex;}
        set
        {
            currentInteractionIndex = value;

            // it might be null when we are running tests in the Editor
            if (CurrentInteractiveGameObject != null)
                CurrentInteractiveGameObject.OnSetNewInteraction(currentInteractionIndex);
        }
    }

    public InteractiveGameObject CurrentInteractiveGameObject { get; private set; }

    public InteractionAsset CurrentInteraction 
    { 
        get
        {
            if (CurrentInteractionIndex < 0 || CurrentInteractionIndex > AllInteractions.Length - 1)
                return null;
            return AllInteractions[CurrentInteractionIndex];
        }
    }

    public void SetCurrentInteractiveGameObjectInTheScene(InteractiveGameObject obj)
    {
        CurrentInteractiveGameObject = obj;
    }

    public void Interact(ChoiceAsset choiceAsset)
    {
        if (CurrentInteraction == null)
        {
            Debug.LogError("Tried to interact with interactive element: " + name + " with choice: " + choiceAsset.name +
                           " but CurrentInteraction was not set on this element. Interaction index is: " +
                           CurrentInteractionIndex);
            return;
        }

        var consequencesOfThisChoice =
            (from c in CurrentInteraction.Consequences where c.Choice == choiceAsset select c).ToList();

        if (consequencesOfThisChoice.Count == 0)
        {
            Debug.LogError("Tried to interact with interactive element: " + name + " with choice: " + choiceAsset.name +
                           " CurrentInteraction was: " + CurrentInteraction +
                           " but it did not contain consequences for this choice");
            return;
        }
        
        if (consequencesOfThisChoice.Count > 1)
        {
            Debug.LogError("Tried to interact with interactive element: " + name + " with choice: " + choiceAsset.name +
                           " CurrentInteraction was: " + CurrentInteraction +
                           " but it contained multiple consequences for this choice");
            return;
        }

        consequencesOfThisChoice[0].TriggerAllConsequences();
    }
    
    [System.Serializable]
    public class InteractionAssetList : ReorderableArray<InteractionAsset> {}
}
