﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    //public variables
    public CameraScript _CameraManager;
    public StateScript _StateManager;
    public ObjectScript _ObjectManager;
    public AnimationScript _AnimationManager;

    public Transform _SpawnPoints;
    public Transform _RoomPositionPoint;
    public Transform _MazePositionPoint;
    

    //private variables
    private Transform[] spawnPoint; //all spawn points
    private bool abilties;
    private bool isDead;
    private Scene pauseMenu;

    // Use this for initialization
    void Start () {
        spawnPoint = _SpawnPoints.GetComponentsInChildren<Transform>();
        _RoomPositionPoint = _RoomPositionPoint.GetComponent<Transform>();
        _MazePositionPoint = _MazePositionPoint.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("r"))
        {
            Respawn();
        }
        if (Input.GetKeyDown("t"))
        {
            _StateManager.CheckDreamState();
            WakeSleep();
        }
        if (Input.GetKeyDown("i"))
        {
            _StateManager.temperatureIndex++;
        }
        if (Input.GetKeyDown("k"))
        {
            _StateManager.temperatureIndex--;
        }
        if (Input.GetKeyDown("o"))
        {
            _StateManager.peeIndex = 0.2f;
        }
        if (Input.GetKeyDown("l"))
        {
            _StateManager.peeIndex = 0.65f;
        }
        if (Input.GetKeyDown("y"))
        {
            pauseMenu = SceneManager.GetSceneByName("Pause");
            if (pauseMenu.name == null)
            {
                Debug.Log("Change Scene");
                SceneManager.LoadScene(2,LoadSceneMode.Additive);
            }
        }
    }

    //respawn player in a random spawn point
    private void Respawn()
    {
        int i = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[i].transform.position;
    }

    //saves position in maze, teleports to room and back to maze; sets dreamState
    public void WakeSleep()
    {
        if (_StateManager.dreamState == true)
        {
            _MazePositionPoint.transform.position = transform.position;
            transform.position = _RoomPositionPoint.transform.position;
            _StateManager.dreamState = false;
            _CameraManager.fog.enabled = false;
            _CameraManager.blur.enabled = false;
        }
        else
        {
            transform.position = _MazePositionPoint.transform.position;
            _StateManager.dreamState = true;
            _ObjectManager.SpawnObstacles(_StateManager.temperatureIndex);

        }
        print(_StateManager.temperatureIndex);
        _StateManager.CheckDreamState();
    }

    //action use
    public void Use(Collider col)
    {
        if (col.gameObject.name == "Switch Light Bathroom")
        {
            _AnimationManager.TurnLightSwitchBathroom(_StateManager.switchLightBathroomOn);
        }
        if (col.gameObject.name == "Switch Light")
        {
            _AnimationManager.TurnLightSwitch(_StateManager.switchLightOn);
        }
        if (col.gameObject.name == "Switch Fan")
        {
            _AnimationManager.TurnFanSwitch(_StateManager.switchFanOn);
        }
        if (col.gameObject.tag == "bathroomdoor")
        {
            _AnimationManager.OpenDoor(_StateManager.doorOpen);
        }
        if (col.gameObject.tag == "exit")
        {
            //ChangeScene(3);
        }
        if (col.gameObject.tag == "drawer")
        {
            _AnimationManager.OpenDrawer(_StateManager.drawerOpen);
        }
        if (col.gameObject.name == "Toilet")
        {
            _AnimationManager.OpenToilet(_StateManager.toiletOpen);
        }
        if (col.gameObject.tag == "window")
        {
            _AnimationManager.OpenWindow(_StateManager.windowOpen);
        }
        if (col.gameObject.name == "Logs")
        {
            _AnimationManager.LightFire(_StateManager.firePlaceOn);
        }
        if (col.gameObject.name == "Lighter")
        {
            _StateManager.haveLighter = true;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "power")
        {
            //either using tags or using name
            if (col.gameObject.name == "BookOpenA(Clone)")
            {
                Debug.Log("Picked Up " + col.gameObject.name);
                _ObjectManager.TakePower(col.gameObject);
            }
            else if (col.gameObject.name == "BookOpenB(Clone)")
            {
                Debug.Log("Picked Up " + col.gameObject.name);
                _ObjectManager.TakePower(col.gameObject);
            }
            else if (col.gameObject.name == "BookOpenC(Clone)")
            {
                Debug.Log("Picked Up " + col.gameObject.name);
                _ObjectManager.TakePower(col.gameObject);
            }
        }

    }

    private void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }


}
