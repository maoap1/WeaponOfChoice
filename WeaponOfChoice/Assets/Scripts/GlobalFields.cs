using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFields
{

	public static float WeaponChooseCounterTime = 3f;

	#region weapons selection
    public static WeaponsTypes WeaponPlayer0 { private get; set; } = WeaponsTypes.Fork;
	public static WeaponsTypes WeaponPlayer1 { private get; set; } = WeaponsTypes.Palcat;
	public static WeaponsTypes GetWeapon(int playerNum)
	{
		if (playerNum == 0)
			return WeaponPlayer0;
		else if (playerNum == 1)
			return WeaponPlayer1;
		else
			throw new NotImplementedException();
	}

	public static bool swap = false;

	public static void SetWeapon(WeaponsTypes wt, int playerNum)
	{
		if (swap)
			playerNum = playerNum * (-1) + 1;
		if (playerNum == 0)
			WeaponPlayer0 = wt;
		else if (playerNum == 1)
			WeaponPlayer1 = wt;
		else
			throw new NotImplementedException();
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
