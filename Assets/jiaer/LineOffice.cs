using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOffice : MonoBehaviour {
    public GameObject lineleft;
    public GameObject lineright;
    public GameObject panleft;
    public GameObject panright;
    public GameObject high;
    public GameObject low;
    public GameObject wheel;
    public float speed;
    public float rotatespeed;
    private float panspeed;
    
	// Use this for initialization
	void Start () {
        panspeed = (high.transform.position.y - low.transform.position.y) * speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.I))
        {
            LeftDown();
        }
        else if (Input.GetKey(KeyCode.O))
        {
            RightDown();
        }
	}

    public void LeftDown()
    {
        if (lineright.transform.localScale.y > 0.3)
        {
            lineleft.transform.localScale += new Vector3(0, speed * Time.deltaTime, 0);
            lineright.transform.localScale -= new Vector3(0, speed * Time.deltaTime, 0);
            panleft.transform.Translate(0, -panspeed * Time.deltaTime, 0);
            panright.transform.Translate(0, panspeed * Time.deltaTime, 0);
            wheel.transform.Rotate(0, 0, rotatespeed * Time.deltaTime);
        }
    }

    public void RightDown()
    {
        if (lineleft.transform.localScale.y > 0.3)
        {
            lineright.transform.localScale += new Vector3(0, speed * Time.deltaTime, 0);
            lineleft.transform.localScale -= new Vector3(0, speed * Time.deltaTime, 0);
            panleft.transform.Translate(0, panspeed * Time.deltaTime, 0);
            panright.transform.Translate(0, -panspeed * Time.deltaTime, 0);
            wheel.transform.Rotate(0, 0, -rotatespeed * Time.deltaTime);
        }
    }
}
