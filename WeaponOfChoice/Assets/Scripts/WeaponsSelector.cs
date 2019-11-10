using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class WeaponsSelector : MonoBehaviour
{
	public GameObject[] keysPictures = new GameObject[3];
	GameObject[] weapons = new GameObject[3];
	GameObject UpKey { get => keysPictures[0]; set => keysPictures[0] = value; }
	GameObject RightKey { get => keysPictures[1]; set => keysPictures[1] = value; }
	GameObject LeftKey { get => keysPictures[2]; set => keysPictures[2] = value; }
	public GameObject Base;
	public float WaitingTime;
	private float weaponsSelectedTime = 0;
	public float weaponChoosingTime = 1;

	public GameObject sceneFader;

	static byte timesUsed = 0;

	const int maximalNumberOfWeapons = 3;

	BaseOfWeapons weaponsBase => Base.GetComponent<BaseOfWeapons>();

	float startTime;
	float usedTime;

	byte state = 0;

	InputResults Input => GetComponent<InputManager>().CurrInput;

	void Start()
	{
		timesUsed = 0;
		startTime = Time.timeSinceLevelLoad;
		for (int i = 0; i < keysPictures.Length; i++)
		{
			keysPictures[i] = Instantiate(keysPictures[i], transform);
		}
		state = 0;
	}

	void Update()
	{
		if (Time.timeSinceLevelLoad > startTime + WaitingTime && state == 0)
		{
			state++;
			usedTime = Time.timeSinceLevelLoad;
			int count = Mathf.Min(maximalNumberOfWeapons, weaponsBase.WeaponsPrefabs.Count);
			List<int> usedIndexes = new List<int>();
			int currIndex;
			for (int i = 0; i < count; i++)
			{
				while (usedIndexes.Contains(currIndex = Random.Range(0, weaponsBase.WeaponsPrefabs.Count)))
				{ }
				usedIndexes.Add(currIndex);
			}
			for (int i = 0; i < keysPictures.Length; i++)
				InstantiateWithPosOf(weaponsBase.WeaponsPrefabs[usedIndexes[
					Mathf.Min(i, usedIndexes.Count - 1)]], transform, i);
		}
		if ( state == 1 && (Input.StartJumping || Input.EndJumping || (Input.Horizontal != 0)))
		{
			state++;
			timesUsed++;
			int index = 0;
			if (Input.StartJumping || Input.EndJumping)
				index = 0;
			else if (Input.Horizontal > 0)
				index = 1;
			else if (Input.Horizontal < 0)
				index = 2;
			else
				throw new System.Exception("That doesn't make sense! :O");
			GlobalFields.SetWeapon(weaponsBase.GetWeaponTypeOf(
					weapons[index].GetComponent<Weapon>()));
			for(int i = 0; i < keysPictures.Length; i++)
				if(i != index)
				{
					keysPictures[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
					weapons[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
				}
		}
		else if (state == 1 && usedTime + weaponChoosingTime < Time.timeSinceLevelLoad)
		{
			state++;
			GlobalFields.SetWeapon(weaponsBase.GetWeaponTypeOf(
					weapons[Random.Range(0, maximalNumberOfWeapons)].GetComponent<Weapon>()));
			timesUsed++;
		}
		if (state == 2 && timesUsed > 1)
			state++;
		if (state == 3)
		{
			state++;
			sceneFader.GetComponent<SceneManager>().LoadScene("Fight");
		}
	}
	private void InstantiateWithPosOf(GameObject weaponToInstantiate, Transform parent, int positionerIndex)
	{
		GameObject newObj = Instantiate(weaponToInstantiate, parent.transform);
		newObj.transform.localPosition = keysPictures[positionerIndex].transform.localPosition * 2;
		weapons[positionerIndex] = newObj;
		newObj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		newObj.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}
}
