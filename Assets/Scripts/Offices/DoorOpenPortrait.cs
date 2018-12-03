using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenPortrait : MonoBehaviour {
    [Header("机关浮板")]
    public GameObject office;
    [Header("没按时图片")]
    public Sprite buttomup;
    [Header("按下时图片")]
    public Sprite buttomdown;
    [Header("初始位置")]
    public Vector3 officeup;
    [Header("终止位置")]
    public Vector3 officedown;
    [Header("是否是触发式")]
    public bool istrigger;
    [Header("是否一次性")]
    public bool islast;
    [Header("浮板速度")]
    public float speed;
    public enum direction { up,down };
    [Header("往上还是往下")]
    public direction real;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (real == direction.down && (collision.gameObject.layer == 10 || collision.gameObject.layer == 8))
        {
            //office.transform.position = Vector3.MoveTowards(officepos, officeori, speed * Time.deltaTime);
            if (!istrigger)
                GetComponent<SpriteRenderer>().sprite = buttomdown;
            StopCoroutine("OfficeUp");
            StartCoroutine("OfficeDown");
        }
        else if(real == direction.up && (collision.gameObject.layer == 10 || collision.gameObject.layer == 8))
        {
            if (!istrigger)
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
            GetComponent<SpriteRenderer>().sprite = buttomup;
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
