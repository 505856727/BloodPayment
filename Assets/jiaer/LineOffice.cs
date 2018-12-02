using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOffice : MonoBehaviour {
    public GameObject lineleft;
    public GameObject lineright;
    public float speed;
	// Use this for initialization
	void Start () {
		
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

    void LeftDown()
    {
        if (lineright.transform.localScale.y > 0.3)
        {
            lineleft.transform.localScale += new Vector3(0, speed * Time.deltaTime, 0);
            lineright.transform.localScale -= new Vector3(0, speed * Time.deltaTime, 0);
        }
    }

    void RightDown()
    {
        if (lineleft.transform.localScale.y > 0.3)
        {
            lineright.transform.localScale += new Vector3(0, speed * Time.deltaTime, 0);
            lineleft.transform.localScale -= new Vector3(0, speed * Time.deltaTime, 0);
        }
    }
}
