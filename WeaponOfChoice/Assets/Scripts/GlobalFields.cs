using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFields
{
	static System.Random random = new System.Random();
	public static WeaponsTypes First { private get; set; } = WeaponsTypes.Toaster;//TODO
	static bool firstTaken = false;
	static bool somethingTaken = false;
	public static WeaponsTypes Second { private get; set; } = WeaponsTypes.Toaster;
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
			if (random.Next(2) == 0)
			{
				firstTaken = true;
				return First;
			}
			else
				return Second;
		}
	}
}
