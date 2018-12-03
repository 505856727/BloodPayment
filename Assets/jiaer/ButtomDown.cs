using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomDown : MonoBehaviour {
    public bool isdown = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
        {
            isdown = true;
        }
    }
}
