﻿using UnityEngine;
using System.Collections;

public class Maze_Room_OBJ : MonoBehaviour
{
    public int id;

    // Use this for initialization
    void Awake()
    {
        Maze_S.Instance.Register(gameObject, id);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Entered Room " + id);
            Maze_S.Instance.Enter_Room(id, gameObject.tag, gameObject);
            print(gameObject.tag);
        }
        else
        {
            Debug.Log("Entering Room failed");
        }

    }
}
