using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeaponTextDisplayer : MonoBehaviour
{
	public GameObject opponentText;
	public GameObject youText;

	public float displayDelay = 0.2f;

	bool used = true;
	float usedTime = 0;

	public void Proceed()
	{
		used = false;
		usedTime = Time.timeSinceLevelLoad;
	}

	void Update()
    {
        if(Time.timeSinceLevelLoad >= usedTime + displayDelay && !used)
		{
			used = true;
			GameObject text;
			if (GlobalFields.swap)
				text = opponentText;
			else
				text = youText;
			for (int i = -1; i < 2; i += 2)
			{
				GameObject newText = Instantiate(text, transform);
				newText.transform.localPosition += new Vector3(i * 4.55f, 0);
			}
		}
    }
}
