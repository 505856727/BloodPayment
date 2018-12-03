using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoButtomOffice : MonoBehaviour {
    public GameObject leftbuttom;
    public GameObject rightbuttom;
    public GameObject office;
    public float officeleft;
    public float officeright;
    public bool islast;
    public bool ismove;
    public float speed;
    private void Update()
    {
        if (leftbuttom.GetComponent<ButtomDown>().isdown && rightbuttom.GetComponent<ButtomDown>().isdown && !ismove)
        {
            StopCoroutine("RightMove");
            StartCoroutine("LeftMove");
            ismove = true;
        }
        else if((!leftbuttom.GetComponent<ButtomDown>().isdown || !rightbuttom.GetComponent<ButtomDown>().isdown)&& ismove)
        {
            StopCoroutine("LeftMove");
            StartCoroutine("RightMove");
            ismove = false;
        }
    }

    IEnumerator LeftMove()
    {
        while (office.transform.position.x < officeright)
        {
            office.transform.Translate(speed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }

    IEnumerator RightMove()
    {
        while (office.transform.position.x > officeleft)
        {
            office.transform.Translate(-speed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }
}
