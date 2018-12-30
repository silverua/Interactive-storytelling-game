using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChapterAsset : ScriptableObject
{
    public List<InteractiveElementAsset> AllInteractiveElements;

    public InteractiveElementAsset GetElementByName(string elementName)
    {
        foreach (var element in AllInteractiveElements)
            if (element.name == elementName)
                return element;

        Debug.LogError("Could not get the element: " + elementName + " in chapter: " + name);
        return null;
    }
}
