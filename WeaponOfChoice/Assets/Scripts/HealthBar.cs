using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public Player Player;
	public bool stayOnRight;
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
		initSize = transform.localScale.x;
	}

	// Update is called once per frame
	void Update()
	{
		float curr = GetComponent<SpriteRenderer>().bounds.size.x;
		transform.localScale = new Vector3(initSize * Player.CurrHealth / Player.MAX_HEALTH, transform.localScale.y);
		transform.position += new Vector3( sign *(curr - GetComponent<SpriteRenderer>().bounds.size.x) / 2, 0);
	}
}
