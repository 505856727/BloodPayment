using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodShow : MonoBehaviour {

    public GameObject vampire;
    public float hpfull;
	// Use this for initialization
	void Start () {
        hpfull = vampire.GetComponent<VampireControl>().hp;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(vampire.GetComponent<VampireControl>().hp / hpfull, 1, 1);
	}
}
