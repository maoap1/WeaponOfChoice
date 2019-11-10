using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondsCounter : MonoBehaviour
{
	public List<GameObject> counterMarks;

	GameObject instantiated;

    // Update is called once per frame
    void Update()
    {
		if (instantiated != null)
			Destroy(instantiated);
        if(Time.timeSinceLevelLoad + 1 < counterMarks.Count)
		{
			instantiated = Instantiate(counterMarks[(int)Time.timeSinceLevelLoad]);
		}
    }
}
