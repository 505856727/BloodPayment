using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
    public float speed;
    public float limitleft;
    public float limitright;
    public float jumpspeed;
    public float rotatespeed;
    public GameObject sword;
    public bool isjump = false;
    public bool isattack = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveCharacter();
        Attack();
	}

    void MoveCharacter()
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x > limitleft)
        { 
            transform.Translate(-speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < limitright)
        {
            transform.Translate(speed, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.W) && isjump == false)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpspeed));
            isjump = true;
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && isattack == false)
        {
            isattack = true;
            StartCoroutine(SwordAttack());
        }
    }

    IEnumerator SwordAttack()
    {
        while (sword.transform.eulerAngles.z > 240)
        {
            sword.transform.Rotate(0, 0, -rotatespeed);
            yield return null;
        }
        sword.transform.eulerAngles = new Vector3(0, 0, -30);
        isattack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            isjump = false;
        }
    }
}
