﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseOfWeapons : MonoBehaviour
{
	public List<GameObject> WeaponsPrefabs;

	public GameObject GetWeaponThat(Predicate<Weapon> condition)
	{
		foreach (GameObject prefab in WeaponsPrefabs)
		{
			Weapon w = prefab.GetComponent<Weapon>();
			if (condition(w))
				return prefab;
		}
		return null;
	}
	public GameObject GetWeaponThatIs(WeaponsTypes type)
	{
		switch(type){
			case WeaponsTypes.Fork:
				return GetWeaponThat(w => w is Fork);
			case WeaponsTypes.Toaster:
				return GetWeaponThat(w => w is Toaster);
			default:
				throw new NotImplementedException();
		}
	}
	public WeaponsTypes GetWeaponTypeOf(Weapon weapon)
	{
		switch (weapon)
		{
			case Fork f:
				return WeaponsTypes.Fork;
			case Toaster t:
				return WeaponsTypes.Toaster;
			default:
				throw new NotImplementedException();
		}
	}
}
