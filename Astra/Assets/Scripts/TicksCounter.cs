using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicksCounter : MonoBehaviour
{
    public int previousTickNumber;
    public int tickNumberChange;
    public int tickNumber;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Tick");
    }

    // Update is called once per frame
    void Update()
    {
        CalculateTickNumberChange();
    }

    private void CalculateTickNumberChange()
    {
        tickNumberChange = tickNumber - previousTickNumber;
        previousTickNumber = tickNumber;
    }

    IEnumerator Tick()
    {
        yield return new WaitForSeconds(0.05f);
        tickNumber += 1;
        StartCoroutine("Tick");
    }
}
