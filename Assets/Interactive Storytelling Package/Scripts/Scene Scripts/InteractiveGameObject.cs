using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InteractiveGameObject : MonoBehaviour, IEventSystemHandler
{
    public InteractiveElementAsset Asset;
    public List<UnityEvent> ConsequenceEvents;

    private void Awake()
    {
        Asset.SetCurrentInteractiveGameObjectInTheScene(this);
    }

    /// <summary>
    /// A callback to show visual stuff when this object enters a new interactive state
    /// </summary>
    /// <param name="interactionIndex"> Index of the new state on the asset </param>
    public void OnSetNewInteraction(int interactionIndex)
    {
        if(interactionIndex >= ConsequenceEvents.Count)
            return;

        ConsequenceEvents[interactionIndex].Invoke();
    }
}
