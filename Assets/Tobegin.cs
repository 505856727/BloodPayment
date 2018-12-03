using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tobegin : MonoBehaviour {
    private void Start()
    {
        StartCoroutine(Gotobegin());
    }

    IEnumerator Gotobegin()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Begin");
    }
}
