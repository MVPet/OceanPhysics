using UnityEngine;
using System.Collections;

public class GraphScript : MonoBehaviour {

    // variables we will need
    public bool fillRoom = false;
    public bool start = false;
    public float bLineX;
    public Vector2 graphSpeed = new Vector2(.1f, .05f);
    public float fadeSpeed = .1f;
    public float graphOriginX = -2;

    public float sliderPercent;
    public bool fillDone = false;

    //our textures for the graph
    public SpriteRenderer graph;
    public SpriteRenderer fader;
    public SpriteRenderer blueLine;
    public SpriteRenderer redLine;

    public float point;

    void Update()
    {
        if (start)
        {
            FadeIn();
        }
        redLine.transform.localPosition = new Vector3(graphOriginX + (3.97f * .5f), redLine.transform.localPosition.y, redLine.transform.localPosition.z);

        //Animation stuff, room full = move right, room empty = move left
        if (fillRoom && !fillDone)
        {
            bLineX = Mathf.Lerp(blueLine.transform.localPosition.x, redLine.transform.localPosition.x, graphSpeed.x);
            //bLineX = Mathf.Lerp(blueLine.transform.localPosition.x, point, graphSpeed.x);
            if (blueLine.transform.localPosition.x >= (redLine.transform.localPosition.x - .01))
            {
                blueLine.transform.localPosition = redLine.transform.localPosition;
                fillDone = true;
            }
        }
        else if (!fillRoom)
        {
            bLineX = Mathf.Lerp(blueLine.transform.localPosition.x, graphOriginX, graphSpeed.x);
            fillDone = false;
        }
        else
            bLineX = graphOriginX + (3.97f * sliderPercent);


        blueLine.transform.localPosition = new Vector3(bLineX, blueLine.transform.localPosition.y, blueLine.transform.localPosition.z);
    }

    void FadeIn()
    {
        fader.color = Color.Lerp(fader.color, new Color(1, 1, 1, 0), fadeSpeed);
    }
}
