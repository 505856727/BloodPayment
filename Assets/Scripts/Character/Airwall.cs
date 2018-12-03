using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airwall : MonoBehaviour {
    public GameObject Camera;
    public Vector3 offset;
    private void Start()
    {
        offset = transform.position - Camera.transform.position; 
    }
    private void Update()
    {
        transform.position = Camera.transform.position + offset;
    }
}
