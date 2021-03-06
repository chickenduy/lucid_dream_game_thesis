﻿using UnityEngine;
using System.Collections;

public class Spawns_OBJ : MonoBehaviour
{
    // Use this for initialization
    private int id = 0;
    void Awake()
    {

        //register object in Singleton dictionary
        Check_For_ID();
    }
    private void Check_For_ID()
    {
        if (gameObject.tag == "Respawn")
        {
            id = GetComponentInParent<Maze_Room_OBJ>().id;
            //if gameObject is a respawn point register with maze room ID and their tag
            Maze_S.Instance.Register(id, gameObject.transform, gameObject.tag);
        }
        else
            //else register gameObject with their name
            Maze_S.Instance.Register(id, gameObject.transform, gameObject.name);
    }
}
