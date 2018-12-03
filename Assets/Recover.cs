using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Blood"))
        {
            Human_PlayerController temp = collision.gameObject.GetComponent<Human_PlayerController>();
            if (temp)
            {
                temp.changeVitality(20.0f);
            }
            Destroy(gameObject);
        }

    }
}
