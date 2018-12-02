using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleOffice : MonoBehaviour {
    public GameObject handle;
    public float rotatespeed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(RotateHandle());
            }
        }
    }

    IEnumerator RotateHandle()
    {
        while (handle.transform.eulerAngles.z > 240)
        {
            handle.transform.Rotate(0, 0, -rotatespeed * Time.deltaTime);
            yield return null;
        }
    }
}
