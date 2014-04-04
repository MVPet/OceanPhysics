using UnityEngine;
using System.Collections;

public class GraphScript : MonoBehaviour {

    // variables we will need
    public bool fillRoom = false;
    public bool stage2 = false;
    public bool changeGraph = false;
    public bool start = false;
    public float bLineX;
    public Vector2 graphSpeed = new Vector2(.1f, .05f);
    public float fadeSpeed = .1f;
    public float graphOriginX = -2;

    public float sliderPercent;
    public bool fillDone = false;

    public bool fadeIn = true;
    public bool fadeOut = false;
    public float fadeDelay = 1;

    public string scaleLabel1 = "0.00";
    public string scaleLabel2 = "1.00";
    public string scaleLabel3 = "2.00";

    public Color labelColor = new Color(1, 1, 1, 1);

    //our textures for the graph
    public SpriteRenderer graph;
    public SpriteRenderer fader;
    public SpriteRenderer blueLine;
    public SpriteRenderer redLine;
    public RoomControlGUI roomControlGUI;


    void Start()
    {
        redLine.transform.localPosition = new Vector3(graphOriginX + (3.97f * .5f), redLine.transform.localPosition.y, redLine.transform.localPosition.z);
        roomControlGUI = GameObject.FindGameObjectWithTag(Tags.roomCamera).GetComponent<RoomControlGUI>();
    }

    void OnGUI()
    {
        if (start)
        {
            //GUI.color = Color.Lerp(GUI.color, labelColor, fadeSpeed);
            GUI.color = labelColor;

            // Our scale labels
            GUI.Label(new Rect(480.5f, 539.6f, 100, 100), scaleLabel1);
            GUI.Label(new Rect(671, 539.6f, 100, 100), scaleLabel2);
            GUI.Label(new Rect(863, 539.6f, 100, 100), scaleLabel3);

            // The little marks we need
        }
        else
            GUI.color = new Color(1, 1, 1, 0);

    }

    void Update()
    {
        if (start && fadeIn)
        {
            FadeIn(); 
            if (fader.color.a <= .05f)
            {
                fadeIn = false;
                fader.color = new Color(1, 1, 1, 0);
            }           
        }

        if (fillRoom)
        {
            if (!changeGraph)
            {
                //Animation stuff, room full = move right, room empty = move left
                if (!fillDone)
                {
                    bLineX = Mathf.Lerp(blueLine.transform.localPosition.x, redLine.transform.localPosition.x, graphSpeed.x);
                    if (blueLine.transform.localPosition.x >= (graphOriginX + (3.97f * .4f)) && !stage2)
                    {
                        changeGraph = true;
                        fadeOut = true;
                    }

                    if (blueLine.transform.localPosition.x >= (redLine.transform.localPosition.x - .01))
                    {
                        blueLine.transform.localPosition = redLine.transform.localPosition;
                        roomControlGUI.moveBall = true;
                        fillDone = true;
                        roomControlGUI.hSliderValue = 50.0f;
                    }
                }
                else
                    bLineX = graphOriginX + (3.97f * sliderPercent);
            }
            else
                StartCoroutine("ChangeGraph");
        }
        else if (!fillRoom)
        {
            if (!changeGraph)
            {
                bLineX = Mathf.Lerp(blueLine.transform.localPosition.x, graphOriginX, graphSpeed.x);
                fillDone = false;
            }
            else
            {
                bLineX = Mathf.Lerp(blueLine.transform.localPosition.x, graphOriginX, graphSpeed.x);
                StartCoroutine("ChangeGraph");
            }
        }
        else
            bLineX = graphOriginX + (3.97f * sliderPercent);

        blueLine.transform.localPosition = new Vector3(bLineX, blueLine.transform.localPosition.y, blueLine.transform.localPosition.z);
    }

    void FadeIn()
    {
        labelColor = Color.Lerp(labelColor, new Color(1, 1, 1, 1), fadeSpeed);
        fader.color = Color.Lerp(fader.color, new Color(1, 1, 1, 0), fadeSpeed  - .05f);
    }

    void FadeOut()
    {
        labelColor = Color.Lerp(labelColor, new Color(0, 0, 0, 0), fadeSpeed);
        fader.color = Color.Lerp(fader.color, new Color(1, 1, 1, 1), fadeSpeed);
    }

    IEnumerator ChangeGraph()
    {
        if(fadeOut)
        {
            FadeOut();
            if (fader.color.a > .99f)
            {
                fader.color = new Color(1, 1, 1, 1);
                fadeOut = false;
                GraphSwitch();
                fadeIn = true;
            }
        }
        if (fadeIn)
        {
            FadeIn();
            if (fader.color.a < .1f)
            {
                yield return new WaitForSeconds(fadeDelay);
                fadeDelay = 1f;
                fader.color = new Color(1, 1, 1, 0);
                changeGraph = false;
                fadeIn = false;
            }
        }

    }

    void GraphSwitch()
    {
        if (!stage2)
        {
            //blueLine.transform.localPosition = new Vector3(graphOriginX + (3.97f * .3f), blueLine.transform.localPosition.y, blueLine.transform.localPosition.z);
            blueLine.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 5));
            scaleLabel1 = "0.75";
            scaleLabel3 = "1.50";
            stage2 = true;
        }
        else
        {
            //blueLine.transform.localPosition = new Vector3(graphOriginX + (3.97f * .3f), blueLine.transform.localPosition.y, blueLine.transform.localPosition.z);
            blueLine.transform.localRotation = new Quaternion(0, 0, 0, blueLine.transform.localRotation.w);
            scaleLabel1 = "0.00";
            scaleLabel3 = "2.00";
            stage2 = false;
        }
    }
}
