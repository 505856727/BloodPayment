using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour {
    public GameObject bullet;
    public GameObject character;
    public GameObject arch;
    public Vector3 bulletpos;
    public Vector3 archrightpos;
    public Vector3 archleftpos;
    public float archspeed;
    public float attackdistance;
    public bool isbullet = false;
    public bool isattack = false;

    private void Update()
    {
        Attack();
        Bullet();
    }

    void Attack()
    {
        if (transform.position.x - character.transform.position.x < attackdistance && isattack == false)
        {
            StartCoroutine(ArchAttack());
            isattack = true;
        }
    }

    IEnumerator ArchAttack()
    {
        while (arch.transform.localPosition.x > archleftpos.x)
        {
            arch.transform.Translate(-archspeed, 0, 0);
            yield return null;
        }
        arch.transform.localPosition = archrightpos;
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        isattack = false;
    }

    void Bullet()
    {
        if (transform.position.x - character.transform.position.x >= attackdistance && isbullet == false)
        {
            StartCoroutine(MakeBullet());
            isbullet = true;
        }
    }

    IEnumerator MakeBullet()
    {
        while(transform.position.x - character.transform.position.x >= attackdistance)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
            Instantiate(bullet, bulletpos, Quaternion.identity);            
        }
        isbullet = false;
    }

}
