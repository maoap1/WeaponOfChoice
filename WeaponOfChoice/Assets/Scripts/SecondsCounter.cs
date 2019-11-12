using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondsCounter : MonoBehaviour
{
	public List<GameObject> counterMarks = new List<GameObject>((int)GlobalFields.WeaponChooseCounterTime);
    private AudioSource audioSource;

    public AudioClip count;
    public AudioClip go;

	GameObject instantiated;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = count;
    }

    private int last = -1;
    private bool played = false;

    // Update is called once per frame
    void Update()
    {
		if (instantiated != null)
			Destroy(instantiated);
        if(Time.timeSinceLevelLoad < GlobalFields.WeaponChooseCounterTime)
		{
            int newOne = (int)((Time.timeSinceLevelLoad + 1) / (GlobalFields.WeaponChooseCounterTime / counterMarks.Count) - 1);
            if (last != newOne)
            {
                audioSource.Play();
                last = newOne;
            }
            instantiated = Instantiate(counterMarks[Mathf.Max(0, last)], transform);
		}
        if ((!played) && (Time.timeSinceLevelLoad >= GlobalFields.WeaponChooseCounterTime))
        {
            audioSource.clip = go;
            audioSource.Play();
            played = true;
        }
    }
}
