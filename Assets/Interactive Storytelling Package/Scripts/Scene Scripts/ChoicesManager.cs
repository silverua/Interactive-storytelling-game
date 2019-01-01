using UnityEngine;
using TMPro;

public class ChoicesManager: MonoBehaviour
{
    public static ChoicesManager Instance;
    public TextMeshProUGUI ChoicesText;
    private InteractiveElementAsset element;
    
    private void Awake()
    {
        Instance = this;
    }

    public void HandleEndOfInteraction(InteractiveElementAsset elementAsset)
    {
        element = elementAsset;

        if (elementAsset.CurrentInteraction.Consequences.Count == 0)
        {
            FinalizeChoiceActions();
            return;
        }

        if (elementAsset.CurrentInteraction.Consequences.Count == 1)
        {
            FinalizeChoiceActions();
            ChapterManager.InteractWith(elementAsset.name, elementAsset.CurrentInteraction.Consequences[0].Choice.name);
            return;
        }
        
        DisplayInteractions(elementAsset);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            MadeAChoice(0);
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            MadeAChoice(1);
        else if(Input.GetKeyDown(KeyCode.Alpha3))
            MadeAChoice(2);
        else if(Input.GetKeyDown(KeyCode.Alpha4))
            MadeAChoice(3);
        else if(Input.GetKeyDown(KeyCode.Alpha5))
            MadeAChoice(4);
        else if(Input.GetKeyDown(KeyCode.Alpha6))
            MadeAChoice(5);
        else if(Input.GetKeyDown(KeyCode.Alpha7))
            MadeAChoice(6);
        else if(Input.GetKeyDown(KeyCode.Alpha8))
            MadeAChoice(7);
        else if(Input.GetKeyDown(KeyCode.Alpha9))
            MadeAChoice(8);
    }

    private void MadeAChoice(int index)
    {
        if(element == null) return;
        if(index >= element.CurrentInteraction.Consequences.Count) return;

        element.CurrentInteraction.Consequences[index].TriggerAllConsequences();
        FinalizeChoiceActions();
    }

    private void FinalizeChoiceActions()
    {
        ChoicesText.text = "";
        element = null;
        // turn the controller back on:
        var controller = FindObjectOfType<SimpleController>();
        if (controller != null)
            controller.enabled = true;
    }

    private void DisplayInteractions(InteractiveElementAsset elementAsset)
    {
        element = elementAsset;
        var text = "";

        for (var i = 0; i < elementAsset.CurrentInteraction.Consequences.Count; i++)
            text += (i+1) + ". " + elementAsset.CurrentInteraction.Consequences[i].Response + System.Environment.NewLine;

        ChoicesText.text = text;
    }
}