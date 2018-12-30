using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChapterSetter : MonoBehaviour
{
    public ChapterAsset Chapter;
    public List<InteractiveGameObject> InteractiveObjects { get; private set; }

    private void Awake()
    {
        InteractiveObjects = FindObjectsOfType<InteractiveGameObject>().ToList();
        ChapterManager.SetChapter(Chapter);
    }
}
