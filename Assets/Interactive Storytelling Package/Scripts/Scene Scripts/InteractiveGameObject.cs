using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class InteractiveGameObject : MonoBehaviour, IEventSystemHandler
{
    public InteractiveElementAsset Asset;
    public List<UnityEvent> ConsequenceEvents;
    public TextMeshPro Text;
    public float TimeToShowOneText = 3f;
    public float DistanceThreshold = 3f;

    private int displayingLineWithIndex = -1; // negative value = we are not displaying lines right now
    private WaitForSeconds delay;

    private void Awake()
    {
        if (Asset != null)
            Asset.SetCurrentInteractiveGameObjectInTheScene(this);
        delay = new WaitForSeconds(TimeToShowOneText);
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

    private void OnMouseDown()
    {
        // turn off the controller: 
        var controller = FindObjectOfType<SimpleController>();
        
        if(controller == null)
            return;

        var distance = (transform.position - controller.transform.position).magnitude;
        if (distance > DistanceThreshold)
            return;
        
        controller.enabled = false;
        
        StartCoroutine(ShowCurrentInteraction());
    }

    private void Update()
    {
        if (displayingLineWithIndex < 0) return; // if we are not showing any text

        if (!Input.GetMouseButtonDown(0)) return;
        
        // skip the current interaction:
        StopAllCoroutines();
        StartCoroutine(ShowCurrentInteraction(displayingLineWithIndex + 1));
    }

    private IEnumerator ShowCurrentInteraction(int startAtLine = 0)
    {
        if (Asset == null) yield break;

        for (var i = startAtLine; i < Asset.CurrentInteraction.AllTextLines.Length; i++)
        {
            displayingLineWithIndex = i;
            Text.text = Asset.CurrentInteraction.AllTextLines[i];
            yield return delay;
            Text.text = "";
        }

        displayingLineWithIndex = -1;
        Text.text = "";
        // show choices:
        ChoicesManager.Instance.HandleEndOfInteraction(Asset);
    }
}
