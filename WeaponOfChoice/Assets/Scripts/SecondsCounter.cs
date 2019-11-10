using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondsCounter : MonoBehaviour
{
	public List<GameObject> counterMarks = new List<GameObject>((int)GlobalFields.WeaponChooseCounterTime);

	GameObject instantiated;

    // Update is called once per frame
    void Update()
    {
		if (instantiated != null)
			Destroy(instantiated);
        if(Time.timeSinceLevelLoad  < GlobalFields.WeaponChooseCounterTime)
		{
			instantiated = Instantiate(counterMarks[Mathf.Max(0,
				(int)((Time.timeSinceLevelLoad +1)/ (GlobalFields.WeaponChooseCounterTime /counterMarks.Count)))], transform);
		}
    }
}
