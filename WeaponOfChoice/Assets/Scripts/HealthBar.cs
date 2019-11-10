using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public Player Player;
	public bool stayOnRight;
	public GameObject frame;
	public GameObject background;
	public GameObject fill;
	bool StayOnRight {
		get => stayOnRight;
		set {
			if (stayOnRight = value)
				sign = 1;
			else
				sign = -1;
		}
	}
	int sign;

	float initSize;

	// Start is called before the first frame update
	void Start()
	{
		StayOnRight = stayOnRight;
		initSize = background.transform.localScale.x;
	}

	// Update is called once per frame
	void Update()
	{
		float curr = background.GetComponent<SpriteRenderer>().bounds.size.x;
		background.transform.localScale = new Vector3(
			Mathf.Max(initSize * (1 - (Player.CurrHealth + 0f)/ Player.MAX_HEALTH), 0),
			background.transform.localScale.y);
		background.transform.position -= new Vector3(
			sign *(curr - background.GetComponent<SpriteRenderer>().bounds.size.x) / 2, 0);
	}
}
