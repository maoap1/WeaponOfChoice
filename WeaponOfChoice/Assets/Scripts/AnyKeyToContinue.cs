using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyKeyToContinue : MonoBehaviour
{
	bool used = false;
    // Update is called once per frame
    void Update()
    {
		if (Input.anyKeyDown && !used)
		{
			GetComponent<SceneManager>().LoadScene("WeaponChoosing");
			used = true;
		}
    }
}
