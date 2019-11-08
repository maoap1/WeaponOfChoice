using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

	static float fadingTime = 0.35f;

	public SceneManager(bool startFading = true, bool endFading = true)
	{
		this.startFading = startFading;
		this.endFading = endFading;
	}

	public GameObject fadingPic;
	bool startFading;
	bool endFading;
	bool end = false;
	bool start = true;
	bool FadeAtEnd => end && endFading;
	bool FadeAtStart => start && startFading;

	float startTime;
	float endTime;

	string newSceneName;

	public void LoadScene(string sceneName)
	{
		GameObject pic = Instantiate(fadingPic);
		Image i = pic.GetComponent<Image>();
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		i.CrossFadeAlpha(1, fadingTime, false);
		end = true;
		if (FadeAtEnd)
		{
			newSceneName = sceneName;
			endTime = Time.timeSinceLevelLoad;
		}
		else
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
		}
	}

	private void Start()
	{
		if(FadeAtStart)
		{
			startTime = Time.timeSinceLevelLoad;
			Image i = Instantiate(fadingPic).GetComponent<Image>();
			i.CrossFadeAlpha(0, fadingTime, false);
		}
	}

	void Update()
    {
		if (FadeAtStart && Time.timeSinceLevelLoad > startTime + fadingTime)
		{
			start = false;
		}
		if (FadeAtEnd && Time.timeSinceLevelLoad > endTime + fadingTime)
		{
			end = false;
			UnityEngine.SceneManagement.SceneManager.LoadScene(newSceneName);
		}
	}
}
