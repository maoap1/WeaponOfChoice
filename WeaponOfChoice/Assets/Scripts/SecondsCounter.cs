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
        if(Time.timeSinceLevelLoad  < counterMarks.Count)
		{
			instantiated = Instantiate(counterMarks[Mathf.Max(0,(int)Time.timeSinceLevelLoad)], transform);
		}
    }
}
