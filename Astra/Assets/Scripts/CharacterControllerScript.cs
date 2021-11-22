using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();
    public int maxHp;
    public int hp;
    // Спрайты сердец
    public Sprite emptyHeart;
    public Sprite halfHeart;
    public Sprite fullHeart;

    Animator animator;
    public float speed;
    private float normalSpeed;
    private Rigidbody2D rb;
    private Vector3 startScale;

    bool speedHackEnabled = false;
    bool noColliderHackEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = speed;
        startScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Heart"))
        {
            hearts.Add(i);
        }
        maxHp = hearts.Count() * 2;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        RenderHearts();

        Move();

        Cheats();
    }
    private void Move()
    {
        if (Input.GetKey("d"))
        {
            animator.SetInteger("State", 1);
            if (Input.GetKey("w") || Input.GetKey("s"))
            {
                rb.velocity = new Vector3(speed / Mathf.Sqrt(2), rb.velocity.y, 0);
            }
            else
            {
                rb.velocity = new Vector3(speed, rb.velocity.y, 0);
            }
            transform.localScale = startScale;
        }
        if (Input.GetKey("a"))
        {
            animator.SetInteger("State", 1);
            if (Input.GetKey("w") || Input.GetKey("s"))
            {
                rb.velocity = new Vector3(-speed / Mathf.Sqrt(2), rb.velocity.y, 0);
            }
            else
            {
                rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
            }
            transform.localScale = new Vector3(startScale.x * -1, startScale.y, startScale.z);
        }
        if (!(Input.GetKey("a")) && !(Input.GetKey("d")))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (Input.GetKey("w"))
        {
            animator.SetInteger("State", 1);
            if (Input.GetKey("a") || Input.GetKey("d"))
            {
                rb.velocity = new Vector3(rb.velocity.x, speed / Mathf.Sqrt(2), 0);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, speed, 0);
            }
        }
        if (Input.GetKey("s"))
        {
            animator.SetInteger("State", 1);
            if (Input.GetKey("a") || Input.GetKey("d"))
            {
                rb.velocity = new Vector3(rb.velocity.x, -speed / Mathf.Sqrt(2), 0);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, -speed, 0);
            }
        }
        if (!(Input.GetKey("w")) && !(Input.GetKey("s")))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        }
        if (!(Input.GetKey("w")) && !(Input.GetKey("s")) && !(Input.GetKey("a")) && !(Input.GetKey("d")))
        {
            animator.SetInteger("State", 0);
        }
    }

    void Cheats()
    {
        if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.T))
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                speedHackEnabled = true;
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                speedHackEnabled = false;
            }
        }

        if (speedHackEnabled)
        {
            speed = 40;
        }
        else
        {
            speed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.C))
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                noColliderHackEnabled = true;
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                noColliderHackEnabled = false;
            }
        }

        if (noColliderHackEnabled)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            hp++;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            hp--;
        }
    }
    void RenderHearts()
    {
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        if (hp < 0)
        {
            hp = 0;
        }

        for (int i = 0; i<hearts.Count(); i++)
        {
            if (i < hp / 2)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
            }
            if(i == hp / 2)
            {
                if (hp % 2 == 1) 
                { 
                    hearts[i].GetComponent<SpriteRenderer>().sprite = halfHeart;
                }
                else
                {
                    hearts[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
                }
            }
            if (i > hp /2)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            }
        }
    }
}
