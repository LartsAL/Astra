using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftVisualisationController : MonoBehaviour
{
    public Craft craft;
    public GameObject CraftButton;
    public GameObject CraftDescription;
    public GameObject Image;

    public void Craft()
    {
        craft.ExecuteCraft();
    }
}
