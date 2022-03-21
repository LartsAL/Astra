using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelingDecorationController : MonoBehaviour
{
    public Sprite[] sprites;
    private RectTransform RT;
    private Image IM;
    public float speed;
    void Start()
    {
        RT = GetComponent<RectTransform>();
        IM = GetComponent<Image>();
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RT.anchoredPosition += new Vector2(speed, 0);
        if(RT.anchoredPosition.x >= 450)
        {
            RT.anchoredPosition = new Vector2(-450, Random.Range(-140, 140));
            speed = Random.Range(0.75f, 4f);
            int i = Random.Range(0, sprites.Length);
            IM.sprite = sprites[i];
        }
    }
}
