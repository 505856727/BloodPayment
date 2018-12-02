using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterToNextLevel : MonoBehaviour {

    public int nextLevel = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Level_"+ nextLevel);
    }
}
