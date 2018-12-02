using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanOffice : MonoBehaviour {
    public GameObject lineoffice;
    public bool isleft;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
        {
            if (isleft)
            {
                lineoffice.GetComponent<LineOffice>().LeftDown();
            }
            else
            {
                lineoffice.GetComponent<LineOffice>().RightDown();
            }
        }
    }
}
