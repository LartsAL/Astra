using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleMagicBlowController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.localScale += new Vector3(0.015f, 0.015f, 0);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.45f);
        Destroy(this.gameObject);
    }
}
