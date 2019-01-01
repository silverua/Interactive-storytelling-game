using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIconDisplay : MonoBehaviour
{
    public GameObject PermanentIcon;
    public GameObject Glow;

    private void OnMouseEnter()
    {
        Glow.SetActive(true);
    }

    private void OnMouseExit()
    {
        Glow.SetActive(false);
    }
}
