using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    private Text statsText;
    private GameObject player;

    public float fps;
    public float posx, posy;
    public int screenx, screeny;
    public int hp;
    public int chosenSlot;
    public int enemiesCount;

    void Start()
    {
        statsText = GameObject.Find("StatsText").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");

        screenx = Screen.width;
        screeny = Screen.height;
    }

    void Update()
    {
        fps = 1.0f / Time.deltaTime;
        posx = player.transform.position.x;
        posy = player.transform.position.y;
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>().hp;
        chosenSlot = player.GetComponent<InventoryController>().chosenSlot;
        enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void FixedUpdate()
    {
        statsText.text =
            "FPS: " + $"{(int)fps}\n"
            + "Screen Size: " + $"{screenx}" + "*" + $"{screeny}\n"
            + "X: " + $"{Math.Round(posx, 2)}\n"
            + "Y: " + $"{Math.Round(posy, 2)}\n"
            + "Hp: " + $"{hp}\n"
            + "Chosen Slot: " + $"{chosenSlot}\n"
            + "Enemies Count: " + $"{enemiesCount}\n";
    }
}
