using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

    public float endPoint;
    public float cameraSpeed = .1f;

    private RoomTurn roomTurn;

    void Start()
    {
        endPoint = transform.position.x - 41.5f;
        roomTurn = GameObject.FindGameObjectWithTag(Tags.room).GetComponent<RoomTurn>();
    }

    void Update()
    {
        if (transform.position.x >= endPoint)
            transform.Translate(Vector3.forward * (cameraSpeed * Time.deltaTime));
        else
            roomTurn.turn = true;
    }
}
