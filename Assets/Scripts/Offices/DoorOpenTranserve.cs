using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTranserve : MonoBehaviour {

    public GameObject office;
    public Vector3 officeori;
    public Vector3 officepos;
    public Sprite buttomup;
    public Sprite buttomdown;
    public bool islast;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
        {
            //office.transform.position = Vector3.MoveTowards(officepos, officeori, speed * Time.deltaTime);
            StopCoroutine("OfficeLeft");
            StartCoroutine("OfficeRight");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!islast && (collision.gameObject.layer == 10 || collision.gameObject.layer == 8))
        {
            //office.transform.position = Vector3.MoveTowards(officeori, officepos, speed * Time.deltaTime);
            StopCoroutine("OfficeRight");
            StartCoroutine("OfficeLeft");
        }
    }

    IEnumerator OfficeRight()
    {
        while (office.transform.position.x < officepos.x)
        {
            office.transform.Translate(speed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }

    IEnumerator OfficeLeft()
    {
        while (office.transform.position.x > officeori.x)
        {
            office.transform.Translate(-speed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }
}
