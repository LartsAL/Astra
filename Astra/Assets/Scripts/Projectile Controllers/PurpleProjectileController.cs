using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleProjectileController : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float rotSpeed;
    public bool isReady = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Appear());
    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, Time.frameCount*rotSpeed);
        if (isReady)
        {
            transform.position += direction*speed;
        }

    }
    IEnumerator Appear()
    {
        yield return new WaitForSeconds(1);
        isReady = true;
        transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterControllerScript>().hp--;
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterControllerScript>().hp--;
            Destroy(this.gameObject);
        }
    }
}
