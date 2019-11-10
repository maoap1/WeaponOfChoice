using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GlobalFields.swap = Random.Range(0, 2) == 1;
    }
}
