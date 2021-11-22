using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;
    Vector3[] Anchors;
    public GameObject player;
    public GameObject enemy;

    public Rigidbody2D rb;

    private void Start()
    {
        Anchors = pathCreator.path.localPoints;
        rb = enemy.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.position = new Vector3(0, 0, 0);
        Anchors[0] = enemy.transform.position / 3;// - transform.position;
        Anchors[1] = player.transform.position / 3;// - transform.position;
        //enemy1.transform.position = pathCreator.path.GetPointAtDistance(0);
        //enemy2.transform.position = pathCreator.path.GetPointAtDistance(pathCreator.path.length-0.0000001f);
        float length = Mathf.Sqrt(Mathf.Pow(Anchors[0].x - Anchors[1].x, 2) + Mathf.Pow(Anchors[0].y - Anchors[1].y, 2));
        //enemy.transform.position = pathCreator.path.GetPointAtDistance(speed*Time.deltaTime/length);
        rb.MovePosition(pathCreator.path.GetPointAtDistance(speed * Time.deltaTime / length));
        //rb.velocity = pathCreator.path.GetPointAtDistance(speed * Time.deltaTime / length) - enemy.transform.position;
    }
    private void FixedUpdate()
    {
            GameObject hit = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y), new Vector2((player.transform.position.x - enemy.transform.position.x), (player.transform.position.y - enemy.transform.position.y))).collider.gameObject;
    }
}
