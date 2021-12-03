using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicksCounter : MonoBehaviour
{
    public int previousTickNumber;
    public int tickNumberChange;
    public int tickNumber;
    void Start()
    {
        StartCoroutine("Tick");
    }

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
