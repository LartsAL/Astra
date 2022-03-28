using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftsController : MonoBehaviour
{

    float i;
    public GameObject CraftVisualisation;
    private RectTransform rt;
    public InventoryController IC;
    public Craft[] crafts;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        
    }
    void Update()
    {
    }
    public void UpdateCrafts()
    {
        for (int i = 0; i<transform.childCount; i++)
        {
            Destroy(this.gameObject.transform.GetChild(i).gameObject);
        }
        i = 0;
        for (int x = 0; x < crafts.Length; x++)
        {
            crafts[x].CheckAvailablity();
            if (crafts[x].isAvailable)
            {
                CreateVisualisation(crafts[x]);
                i += 70;
                
            }
        }
    }
    void CreateVisualisation(Craft cr)
    {
        GameObject cv = Instantiate(CraftVisualisation);
        cv.transform.SetParent(this.gameObject.transform);
        cv.GetComponent<RectTransform>().anchoredPosition = CraftVisualisation.GetComponent<RectTransform>().anchoredPosition;
        cv.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);       
        cv.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, i);
        cv.GetComponent<CraftVisualisationController>().Image.GetComponent<Image>().sprite = cr.result.GetComponent<SpriteRenderer>().sprite;
        cv.GetComponent<CraftVisualisationController>().CraftDescription.GetComponent<Text>().text = cr.description;
        cv.GetComponent<CraftVisualisationController>().craft = cr;
    }
}
