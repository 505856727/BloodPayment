using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomDown : MonoBehaviour {
    public bool isdown = false;
    public Sprite buttomup;
    public Sprite buttomdown;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
        {
            isdown = true;
            GetComponent<SpriteRenderer>().sprite = buttomdown;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
        {
            isdown = false;
            GetComponent<SpriteRenderer>().sprite = buttomup;
        }
    }
}
