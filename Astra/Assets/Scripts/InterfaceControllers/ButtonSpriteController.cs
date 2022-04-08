using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSpriteController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite enterSprite;
    public Sprite exitSprite;
    public Sprite onClickSprite;
    public float pressedTime;

    private Image buttonIMG;

    private void Start()
    {
        buttonIMG = gameObject.GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonIMG.sprite = enterSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonIMG.sprite = exitSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonIMG.sprite = onClickSprite;
    }
}
