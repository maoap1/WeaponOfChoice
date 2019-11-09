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

	BaseOfWeapons weapons => weaponsBase.GetComponent<BaseOfWeapons>();

	System.Random random = new System.Random();
	float weaponChoosingTime;
	float startTime;
	float usedTime;
	bool used = false;

	InputResults Input => GetComponent<InputManager>().CurrInput;

	void Start()
    {
		float startTime = Time.timeSinceLevelLoad;
		for (int i = 0; i < keysPictures.Length; i++)
		{
			keysPictures[i] = Instantiate(keysPictures[i], transform);
		}
	}

    void FixedUpdate()
    {
        if(Time.timeSinceLevelLoad > startTime + WaitingTime && !used)
		{
			used = true;
			usedTime = Time.timeSinceLevelLoad;
			int count = Mathf.Min(3, weapons.WeaponsPrefabs.Count);
			List<int> usedIndexes = new List<int>();
			int currIndex;
			for (int i = 0; i < count; i++)
			{
				while(usedIndexes.Contains(currIndex = random.Next() % weapons.WeaponsPrefabs.Count))
				{ }
				usedIndexes.Add(currIndex);
			}
			for(int i = 0; i < keysPictures.Length; i++)
				InstantiateWithPosOf(weapons.WeaponsPrefabs[usedIndexes[
					Mathf.Min(i, usedIndexes.Count - 1)]], transform, i);
		}
		if(used)
		{
			if (Input.StartJumping || Input.EndJumping)
				GlobalFields.SetWeapon(weapons.GetWeaponTypeOf(
					keysPictures[0].GetComponent<Weapon>()));
			else if(Input.Horizontal > 0)
				GlobalFields.SetWeapon(weapons.GetWeaponTypeOf(
					keysPictures[1].GetComponent<Weapon>()));
			else if (Input.Horizontal > 0)
				GlobalFields.SetWeapon(weapons.GetWeaponTypeOf(
					keysPictures[2].GetComponent<Weapon>()));
		}
		else if(used && usedTime + weaponChoosingTime < Time.timeSinceLevelLoad)
		{
			GlobalFields.SetWeapon(weapons.GetWeaponTypeOf(
					keysPictures[random.Next(3)].GetComponent<Weapon>()));
		}
    }
	private void InstantiateWithPosOf(GameObject toInstantiate, Transform parent, int positionerIndex)
	{
		GameObject newObj = Instantiate(toInstantiate, parent.transform);
		newObj.transform.position = keysPictures[positionerIndex].transform.position;
		Destroy(keysPictures[positionerIndex]);
		keysPictures[positionerIndex] = newObj;
	}
}
