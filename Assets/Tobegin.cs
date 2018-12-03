using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tobegin : MonoBehaviour {
    public float resttime;
    private void Start()
    {
        StartCoroutine(Gotobegin());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Begin");
        }
    }

    IEnumerator Gotobegin()
    {
        yield return new WaitForSeconds(resttime);
        SceneManager.LoadScene("Begin");
    }
}
