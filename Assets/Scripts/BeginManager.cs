using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginManager : MonoBehaviour {
    public GameObject start;
    public GameObject quit;
    public Vector3 startpos;
    public Vector3 quitpos;
    public GameObject choose;
    public AudioClip[] ClickClips;

    // Use this for initialization
    void Start () {
        startpos = new Vector3(0, 0, 0);
        quitpos = new Vector3(0, -1, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit= Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.down);
        if (hit.collider.gameObject == start)
        {
            choose.transform.position = startpos;
            if (Input.GetMouseButtonDown(0))
            {
                int i = Random.Range(0, ClickClips.Length);
                AudioSource.PlayClipAtPoint(ClickClips[i], transform.position);

                Invoke("EnterGame", 0.5f);
            }
        }
        else if (hit.collider.gameObject == quit)
        {
            choose.transform.position = quitpos;
            if (Input.GetMouseButtonDown(0))
            {
                int i = Random.Range(0, ClickClips.Length);
                AudioSource.PlayClipAtPoint(ClickClips[i], transform.position);

                Invoke("QuitGame", 0.5f);
            }
        }
        
    }

    public void EnterGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
