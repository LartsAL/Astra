using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    public string[] tags;
    public int[] layers;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (tags.Contains(other.gameObject.tag))
        {
            other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (tags.Contains(other.gameObject.tag))
        {
            other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = layers[Array.IndexOf(tags, other.gameObject.tag)];
        }
    }
}
