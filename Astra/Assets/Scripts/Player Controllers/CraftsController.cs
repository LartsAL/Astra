using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftsController : MonoBehaviour
{
    float i;
    public GameObject CraftVisualisation;
    private RectTransform rt;
    public Craft[] crafts;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            UpdateCrafts();
        }
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
                GameObject cv = Instantiate(CraftVisualisation);
                cv.transform.SetParent(this.gameObject.transform);
                cv.GetComponent<RectTransform>().anchoredPosition = CraftVisualisation.GetComponent<RectTransform>().anchoredPosition;
                cv.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, i);
                i += 70;
                
            }
        }
    }
}
