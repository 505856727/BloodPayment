using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenPortrait : MonoBehaviour {
    public GameObject office;
    public Sprite buttomup;
    public Sprite buttomdown;
    public Vector3 officeup;
    public Vector3 officedown;
    public bool islast;
    public float speed;
    public enum direction { up,down,left,right};
    public direction real;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (real == direction.down && (collision.gameObject.layer == 10 || collision.gameObject.layer == 8))
        {
            //office.transform.position = Vector3.MoveTowards(officepos, officeori, speed * Time.deltaTime);
            GetComponent<SpriteRenderer>().sprite = buttomdown;
            StopCoroutine("OfficeUp");
            StartCoroutine("OfficeDown");
        }
        else if(real == direction.up && (collision.gameObject.layer == 10 || collision.gameObject.layer == 8))
        {
            GetComponent<SpriteRenderer>().sprite = buttomdown;
            StopCoroutine("OfficeDown");
            StartCoroutine("OfficeUp");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (real == direction.down && !islast && (collision.gameObject.layer == 10 || collision.gameObject.layer == 8))
        {
            //office.transform.position = Vector3.MoveTowards(officeori, officepos, speed * Time.deltaTime);
            GetComponent<SpriteRenderer>().sprite = buttomup;
            StopCoroutine("OfficeDown");
            StartCoroutine("OfficeUp");
        }
        else if (real == direction.up && !islast && (collision.gameObject.layer == 10 || collision.gameObject.layer == 8))
        {
            GetComponent<SpriteRenderer>().sprite = buttomdown;
            StopCoroutine("OfficeUp");
            StartCoroutine("OfficeDown");
        }
    }

    IEnumerator OfficeDown()
    {
        while (office.transform.position.y > officedown.y)
        {
            office.transform.Translate(0, -speed * Time.deltaTime, 0);
            yield return null;
        }
    }

    IEnumerator OfficeUp()
    {
        while (office.transform.position.y < officeup.y)
        {
            office.transform.Translate(0, speed * Time.deltaTime, 0);
            yield return null;
        }
    }
}
