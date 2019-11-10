using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOverScript : MonoBehaviour
{
    public GameObject SceneManager;
	public GameObject greenWon;
	public GameObject redWon;
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalFields.player0Wins)
        {
            greenWon.GetComponent<SpriteRenderer>().enabled = true;
            redWon.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            greenWon.GetComponent<SpriteRenderer>().enabled = false;
            redWon.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.GetComponent<SceneManager>().LoadScene("WeaponChoosing");
        }
    }

}
