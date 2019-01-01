using UnityEngine;

public class ChoicesManager: MonoBehaviour
{
    public static ChoicesManager Instance;
    private InteractiveElementAsset element;
    
    private void Awake()
    {
        Instance = this;
    }

    public void HandleEndOfInteraction(InteractiveElementAsset elementAsset)
    {
        element = elementAsset;
        
        if(elementAsset.CurrentInteraction.Consequences.Count == 0)
            return;

        if (elementAsset.CurrentInteraction.Consequences.Count == 1)
        {
            ChapterManager.InteractWith(elementAsset.name, elementAsset.CurrentInteraction.Consequences[0].Choice.name);
            return;
        }
        
        DisplayInteractions(elementAsset);
    }

    private void DisplayInteractions(InteractiveElementAsset elementAsset)
    {
        
    }
}