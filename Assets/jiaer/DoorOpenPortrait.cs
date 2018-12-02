using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenPortrait : MonoBehaviour {
    public GameObject office;
    public Vector3 officeori;
    public Vector3 officepos;
    public bool islast;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
        {
            //office.transform.position = Vector3.MoveTowards(officepos, officeori, speed * Time.deltaTime);
            StopCoroutine("OfficeUp");
            StartCoroutine("OfficeDown");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!islast && (collision.gameObject.layer == 10 || collision.gameObject.layer == 8))
        {
            //office.transform.position = Vector3.MoveTowards(officeori, officepos, speed * Time.deltaTime);
            StopCoroutine("OfficeDown");
            StartCoroutine("OfficeUp");
        }
    }

    IEnumerator OfficeDown()
    {
        while (office.transform.position.y > officepos.y)
        {
            office.transform.Translate(0, -speed * Time.deltaTime, 0);
            yield return null;
        }
    }

    IEnumerator OfficeUp()
    {
        while (office.transform.position.y < officeori.y)
        {
            office.transform.Translate(0, speed * Time.deltaTime, 0);
            yield return null;
        }
    }
}
