using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanOffice : MonoBehaviour {
    public GameObject lineoffice;
    public bool isleft;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (isleft)
            {
                lineoffice.GetComponent<LineOffice>().LeftDown(2);
            }
            else
            {
                lineoffice.GetComponent<LineOffice>().RightDown(2);
            }
        }
        else if (collision.gameObject.layer == 8)
        {
            if (isleft)
            {
                lineoffice.GetComponent<LineOffice>().LeftDown(1);
            }
            else
            {
                lineoffice.GetComponent<LineOffice>().RightDown(1);
            }
        }
    }
}
