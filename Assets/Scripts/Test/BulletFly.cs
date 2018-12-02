using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour {
    public float speed;
    public float damage;
    public GameObject blood;
	void Update () {
        transform.Translate(-speed, 0, 0);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "maincharacter")
        {
            GameObject.FindWithTag("blood").transform.localScale -= new Vector3(damage, 0);
            Destroy(this.gameObject);            
        }
    }
}
