using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFields
{
	#region weapons selection
    public static WeaponsTypes First { private get; set; } = WeaponsTypes.PenguinLouncher;
	static bool firstTaken = false;
	static bool somethingTaken = false;
	public static WeaponsTypes Second { private get; set; } = WeaponsTypes.PenguinLouncher;
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
			if (Random.Range(0,2) == 0)
			{
				firstTaken = true;
				return First;
			}
			else
				return Second;
		}
	}
	public static void SetWeapon(WeaponsTypes wt)
	{
		Debug.Log(wt.ToString());
		if (somethingTaken)
		{
			somethingTaken = false;
			if (firstTaken)
			{
				firstTaken = false;
				Second = wt;
			}
			else
				First = wt;
		}
		else
		{
			somethingTaken = true;
			if (Random.Range(0,2) == 0)
			{
				firstTaken = true;
				First = wt;
			}
			else
				Second = wt;
		}
	}
	#endregion

	public static int FirstPoints = 0;
	public static int SecondPoints = 0;

	public static void ILost(Transform me)
	{
		if (me.gameObject.layer == 9) //9 znamena Player0
			SecondPoints++;
		else
			FirstPoints++;
	}
}
