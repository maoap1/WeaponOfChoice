using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class WeaponsSelector : MonoBehaviour
{
	public GameObject[] keysPictures = new GameObject[3];
	GameObject UpKey { get => keysPictures[0]; set => keysPictures[0] = value; }
	GameObject RightKey { get => keysPictures[1]; set => keysPictures[1] = value; }
	GameObject LeftKey { get => keysPictures[2]; set => keysPictures[2] = value; }
	public GameObject weaponsBase;
	public float WaitingTime;
	public float weaponChoosingTime = 1;

    const int maximalNumberOfWeapons = 3;

	BaseOfWeapons weapons => weaponsBase.GetComponent<BaseOfWeapons>();

	float startTime;
	float usedTime;
	bool used = false;
	bool usedSecondTime = false;

	InputResults Input => GetComponent<InputManager>().CurrInput;

	void Start()
	{
		startTime = Time.timeSinceLevelLoad;
		for (int i = 0; i < keysPictures.Length; i++)
		{
			keysPictures[i] = Instantiate(keysPictures[i], transform);
		}
	}

	void Update()
	{
		if (Time.timeSinceLevelLoad > startTime + WaitingTime && !used)
		{
			used = true;
			usedTime = Time.timeSinceLevelLoad;
			int count = Mathf.Min(maximalNumberOfWeapons, weapons.WeaponsPrefabs.Count);
			List<int> usedIndexes = new List<int>();
			int currIndex;
			for (int i = 0; i < count; i++)
			{
				while (usedIndexes.Contains(currIndex = Random.Range(0, weapons.WeaponsPrefabs.Count)))
				{ }
				usedIndexes.Add(currIndex);
			}
			for (int i = 0; i < keysPictures.Length; i++)
				InstantiateWithPosOf(weapons.WeaponsPrefabs[usedIndexes[
					Mathf.Min(i, usedIndexes.Count - 1)]], transform, i);
		}
		if (used && !usedSecondTime && (Input.StartJumping || Input.EndJumping || (Input.Horizontal != 0)))
		{
			int index = 0;
			if (Input.StartJumping || Input.EndJumping)
				index = 0;
			else if (Input.Horizontal > 0)
				index = 1;
			else if (Input.Horizontal < 0)
				index = 2;
			else
				throw new System.Exception("That doesn't make sense! :O");
			usedSecondTime = true;
			GlobalFields.SetWeapon(weapons.GetWeaponTypeOf(
					keysPictures[index].GetComponent<Weapon>()));
			//Debug.Log("done - index = " + index.ToString());
		}
		else if (usedSecondTime && usedTime + weaponChoosingTime < Time.timeSinceLevelLoad)
		{
			GlobalFields.SetWeapon(weapons.GetWeaponTypeOf(
					keysPictures[Random.Range(0, maximalNumberOfWeapons)].GetComponent<Weapon>()));
		}
	}
	private void InstantiateWithPosOf(GameObject weaponToInstantiate, Transform parent, int positionerIndex)
	{
		GameObject newObj = Instantiate(weaponToInstantiate, parent.transform);
		newObj.transform.localPosition = keysPictures[positionerIndex].transform.localPosition * 2;
		keysPictures[positionerIndex] = newObj;
		newObj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		newObj.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}
}
