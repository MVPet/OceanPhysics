using UnityEngine;
using System.Collections;

public class RoomControlGUI : MonoBehaviour {

    //is the room full of air?
    public bool fillRoom = false;
    public bool start = false;

    //data about the room animation
    public float vSpeed = .1f;
    public float endPoint;
    public float curBallY;
    public float gradSpeed = 1.5f;
    public float fadeSpeed = .1f;
    //public float bobSpeed = 1f;

    //things we need references to for this all to work
    private GameObject room;
    private GameObject ball;
    private Color newColor;
    private SpriteRenderer[] walls;
    private GraphScript graphScript;

    public float hSliderValue = 50.0F;
    public float GraphX = 485.6f;
    public float GraphY = 28f;
    public float width = 390.6f;
    public float height = 100;

    public float labelwidth = 100;

    public bool moveBall = false;


    void Start()
    {
        //get all requirements and set them
        newColor = Color.white;

        room = GameObject.FindGameObjectWithTag(Tags.room);
        ball = GameObject.FindGameObjectWithTag(Tags.ball);
        walls = new SpriteRenderer[6];
        walls = room.GetComponentsInChildren<SpriteRenderer>();
        graphScript = GameObject.FindGameObjectWithTag(Tags.graphCamera).GetComponent<GraphScript>();
        curBallY = ball.transform.position.y;
    }

    void OnGUI()
    {
        if (start)
        {
            GUI.color = Color.Lerp(GUI.color, new Color(0, 0, 0, 1), fadeSpeed);
            //Draw all the things!

            if(graphScript.fillDone)
                hSliderValue = GUI.HorizontalSlider(new Rect(GraphX, GraphY, width, height), hSliderValue, 0.0F, 100.0F);

            // make the first button. If pressed, do the function
            if (GUI.Button(new Rect(20, 40, 90, 20), "RESET"))
                EmptyRoom();

            // make the first button. If pressed, do the function
            if (GUI.Button(new Rect(20, 100, 90, 20), "Fill Room"))
                if (!fillRoom)
                    FillRoom();

            GUI.color = Color.blue;
            GUI.Label(new Rect(541.5f, 541.5f, 167, 100), "Blue: Room Density");

            GUI.color = Color.red;
            GUI.Label(new Rect(541.5f+167, 541.5f, 167, 100), "Red: Ball Density");
        }
        else
            GUI.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        graphScript.start = start;
        graphScript.sliderPercent = hSliderValue / 100;
        endPoint = 4 + (36 * hSliderValue / 100);
        
        if (start)
        {
            if (moveBall)
            {
                curBallY = Mathf.Lerp(curBallY, endPoint, vSpeed);
                ball.transform.position = new Vector3(ball.transform.position.x, curBallY, ball.transform.position.z);
            }
            curBallY = ball.transform.position.y;

            if (!graphScript.changeGraph || graphScript.fillDone)
            {
                foreach (SpriteRenderer i in walls)
                    i.color = Color.Lerp(i.color, newColor, gradSpeed * Time.deltaTime);
            }
        }
        else
            ResetBallTransform();
    }

    // Empty the room
    void EmptyRoom()
    {
        moveBall = false;
        graphScript.fadeDelay = 0f;
        graphScript.changeGraph = true;
        graphScript.fadeOut = true;
        fillRoom = false;
        newColor = Color.white;
        ball.rigidbody.isKinematic = false;
        ball.rigidbody.velocity = Vector3.zero;
        ball.rigidbody.useGravity = true;
        graphScript.fillRoom = false;
    }

    // Fill the room with air?
    void FillRoom()
    {
        fillRoom = true;
        newColor = Color.blue;
        graphScript.fillRoom = true;
    }

    public void ResetBallTransform()
    {
        ball.transform.position = new Vector3(0,4.1f,-2);
        ball.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
