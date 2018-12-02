using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {
    public GameObject blood;
    public float damage;
    void Update()
    {
        if (blood.transform.localScale.x <= 0)
        {
            print("over");
            Time.timeScale = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.parent.tag == "Monster" && collision.gameObject.tag == "maincharacter")
        {
            blood.transform.localScale -= new Vector3(damage,0);
        }
        if(transform.parent.tag == "maincharacter" && collision.gameObject.tag == "Monster")
        {
            blood.transform.localScale -= new Vector3(damage, 0);
        }
    }
}
