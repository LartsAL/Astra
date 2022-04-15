using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControllerScript : MonoBehaviour
{
    public TicksCounter TC;
    public WorldNumberContainer WNC;
    public MapGeneratorScript MGS;
    public GameObject CraftsList;
    private bool areCraftsOpened;

    public bool isFrozen;
    public float frostDuration;
    private Color startColor;
    private Color frostColor;

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
    void Start()
    {
        areCraftsOpened = true;
        startColor = GetComponent<SpriteRenderer>().color;
        normalSpeed = speed;
        startScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        hearts.Add(GameObject.Find("Heart 1"));
        hearts.Add(GameObject.Find("Heart 2"));
        hearts.Add(GameObject.Find("Heart 3"));
        maxHp = hearts.Count() * 2;
        hp = maxHp;
        frostColor = new Color(0.4f, 0.4f, 1f, 1);
    }


    void Update()
    {
        if (hp == 0)
        {
            Die();
        }
        if (Input.GetKeyDown("i"))
        {
            if (areCraftsOpened)
            {
                CraftsList.SetActive(false);
                areCraftsOpened = false;
            }
            else
            {
                CraftsList.SetActive(true);
                areCraftsOpened = true;
            }
        }

        if (Input.GetKeyDown("v"))
        {
            isFrozen = true;
        }
        if (Input.GetKeyDown("b"))
        {
            isFrozen = false;
        }
        RenderHearts();

        Move();

        Cheats();

        StatusCheck();
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
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
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
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
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

    void StatusCheck()
    {
        if (frostDuration>0)
        {
            isFrozen = true;
            frostDuration -= Time.deltaTime;
        }
        else
        {
            isFrozen = false;
        }
        if (isFrozen)
        {
            GetComponent<SpriteRenderer>().color = frostColor;
            speed = normalSpeed * 0.5f;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = startColor;

            if (!speedHackEnabled) {
                speed = normalSpeed;
            }
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
                hearts[i].GetComponent<Image>().sprite = fullHeart;
            }
            if(i == hp / 2)
            {
                if (hp % 2 == 1) 
                { 
                    hearts[i].GetComponent<Image>().sprite = halfHeart;
                }
                else
                {
                    hearts[i].GetComponent<Image>().sprite = emptyHeart;
                }
            }
            if (i > hp /2)
            {
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }

    void Die()
    {
        Debug.Log("Раньше сядем - раньше выйдем");
        WNC.isRestarting[MGS.worldNumber] = true;
        GetComponent<DeathScreenCaller>().GameOver();
        
    }
}
