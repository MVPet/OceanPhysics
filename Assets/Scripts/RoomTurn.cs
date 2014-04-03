using UnityEngine;
using System.Collections;

public class RoomTurn : MonoBehaviour {

    //the data we use
    public bool turn = false;
    public int rotateSpeed = 10;

    private RoomControlGUI roomControlGUI;


    void Start()
    {
        roomControlGUI = GameObject.FindGameObjectWithTag(Tags.roomCamera).GetComponent<RoomControlGUI>();
    }

    void FixedUpdate()
    {
        //if we're turning, then turn until we hit our angle of 0
        if (turn)
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

            if (transform.rotation.y >= 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                turn = false;
                roomControlGUI.start = true;
            }
        }
    }
}
