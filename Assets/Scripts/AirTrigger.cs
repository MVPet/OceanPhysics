﻿using UnityEngine;
using System.Collections;

public class AirTrigger : MonoBehaviour {

    public GameObject ball;
    public RoomControlGUI roomControlGUI;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag(Tags.ball);
        roomControlGUI = GameObject.FindGameObjectWithTag(Tags.roomCamera).GetComponent<RoomControlGUI>();
    }

    void OnTriggerEnter()
    {
        //if (!roomControlGUI.ballBob && roomControlGUI.fillRoom)
          //  roomControlGUI.ballBob = true;

        ball.rigidbody.isKinematic = true;
    }
}
