using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Vector3 direction;
    public float rotSpeed;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, Time.frameCount * rotSpeed);
        transform.position += direction * speed;
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
