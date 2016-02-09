﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player_S : Singleton<Player_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Player_S() { }

    //variables
    public bool dream_state = true;
    public bool[] abilities = new bool[4];
    public bool lighter = false;
    public bool drinked = false;
    public bool[] pictures = new bool[4];
    public bool key;

    private bool is_dead;
    private Scene pause_menu;
    private Light player_light;

    //methods
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        player_light = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if (is_dead || Input.GetKeyDown("r"))
        {
            is_dead = false;
            Respawn();
        }
        if (Input.GetKeyDown("t"))
        {
            if (dream_state == true)
            {
                Wake_Sleep();
                Check_Dream_State();
            }

        }
        if (Input.GetKeyDown("i"))
        {
            gameObject.GetComponent<Animator>().SetBool("state",true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pause_menu = SceneManager.GetSceneByName("Pause");
            //if (pause_menu.name == null)
            //{
            //    Debug.Log("Change Scene");
            //    SceneManager.LoadScene(2, LoadSceneMode.Additive);
            //}
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Obstacle_S.Instance.SpawnObstacles();
        }
    }

    //action use
    public void Use(Collider col)
    {
        switch (col.tag)
        {
            case "bed":
                Wake_Sleep();
                Check_Dream_State();
                break;
            case "powerA":
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "powerB":
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "powerC":
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "powerD":
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "moving wall":
                if (abilities[1])
                    Wall_S.Instance.Move_Highlighted_Wall(Wall_S.Instance.Get_ID(col.gameObject));
                break;
            case "switch":
                Object_S.Instance.Use_Object(col.gameObject);
                break;
            case "fan":
                Object_S.Instance.Use_Object(col.gameObject);
                break;
            case "door":
                Object_S.Instance.Use_Object(col.transform.parent.gameObject);
                break;
            case "drawer":
                Object_S.Instance.Use_Object(col.transform.parent.gameObject);
                break;
            case "lighter":
                Destroy(col.gameObject);
                lighter = true;
                break;
            case "window":
                Object_S.Instance.Use_Object(col.transform.parent.gameObject);
                break;
            case "logs":
                Object_S.Instance.Light_Fireplace(col.transform.parent.gameObject);
                break;
            case "bottle":
                //Destroy(col.gameObject);
                Room_S.Instance.Drink();
                break;
            case "toilet lid":
                Object_S.Instance.Use_Object(col.transform.parent.gameObject);
                break;
            case "toilet":
                Room_S.Instance.Use_Toilet();
                break;
            case "room0":
                if (Maze_S.Instance.Get_Discovered(0))
                    Maze_S.Instance.Teleport_To_Room(0);
                break;
            case "room1":
                if (Maze_S.Instance.Get_Discovered(1))
                    Maze_S.Instance.Teleport_To_Room(1);
                break;
            case "room2":
                if (Maze_S.Instance.Get_Discovered(2))
                    Maze_S.Instance.Teleport_To_Room(2);
                break;
            case "room3":
                if (Maze_S.Instance.Get_Discovered(3))
                    Maze_S.Instance.Teleport_To_Room(3);
                break;
            case "fire":
                Fire_S.Instance.Kill_Fire(col.gameObject);
                break;
            case "picture":
                Object_S.Instance.Touch_Picture(col.gameObject);
                break;
            case "hiddenwall":
                if (pictures[0] && pictures[1] && pictures[2] && pictures[3] && key)
                    Wall_S.Instance.Destroy_Wall_2();
                break;
            case "key":
                Destroy(col.gameObject);
                key = true;
                break;






            default:
                Debug.Log("hit nothing");
                break;
        }
    }

    public void Drink()
    {
        Room_S.Instance.Drink();
    }


    public void Check_Dream_State()
    {
        player_light.enabled = dream_state;
    }

    public void Respawn()
    {
        Spawns_S.Instance.RespawnPlayer();
    }

    public void Wake_Sleep()
    {
        Spawns_S.Instance.Wake_Sleep();
    }


}
