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

    IEnumerator Gotobegin()
    {
        yield return new WaitForSeconds(resttime);
        SceneManager.LoadScene("Begin");
    }
}
