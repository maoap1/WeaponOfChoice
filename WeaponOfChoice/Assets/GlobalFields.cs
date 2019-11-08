using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFields
{
	static System.Random random;
	public static WeaponsTypes First { private get; set; }
	static bool firstTaken = false;
	static bool somethingTaken = false;
	public static WeaponsTypes Second { private get; set; }
	public static WeaponsTypes GetWeapon()
	{
		if (somethingTaken)
		{
			somethingTaken = false;
			if (firstTaken)
			{
				firstTaken = false;
				return Second;
			}
			else
				return First;
		}
		else
		{
			somethingTaken = true;
			if (random.Next(1) == 0)
			{
				firstTaken = true;
				return First;
			}
			else
				return Second;
		}
	}
}
