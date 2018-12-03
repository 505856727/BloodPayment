using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireAppear : MonoBehaviour {
    public GameObject vampire;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            vampire.SetActive(true);
        }
    }
}
