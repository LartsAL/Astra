using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleProjectileController : MonoBehaviour
{
    public GameObject deathEffect;
    public Vector3 direction;
    public float speed;
    public float rotSpeed;
    public bool isReady = false;
    private float time;
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        StartCoroutine(Appear());
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= lifetime)
        {
            Destroy(this.gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
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

    //он колижен энтер 2д не вызывается
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterControllerScript>().hp--;
            Destroy(this.gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterControllerScript>().hp--;
            Destroy(this.gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }
}
