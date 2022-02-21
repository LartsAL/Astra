using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int dmg;
    public GameObject deathEffect;
    public Vector3 direction;
    public float speed;
    public float rotSpeed;
    private float time;
    public float lifetime;

    public bool isFrozing; // замораживает ли
    public float frostForse; //длительность заморозки
    public bool isFrostBased; //нужна ли заморозка, чтобы задамажить
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
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
        transform.rotation = Quaternion.Euler(0, 0, Time.frameCount * rotSpeed);

            transform.position += direction * speed;

    }

    //он колижен энтер 2д не вызывается
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(!isFrostBased || collision.gameObject.GetComponent<CharacterControllerScript>().isFrozen)
            {
                collision.gameObject.GetComponent<CharacterControllerScript>().hp-=dmg;
            }
            if (isFrozing && collision.gameObject.GetComponent<CharacterControllerScript>().frostDuration < frostForse)
            {
                collision.gameObject.GetComponent<CharacterControllerScript>().frostDuration = frostForse;
            }
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
