using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

	static float fadingTime = 0.25f;

	public GameObject fadingPic;
	bool startFading = true;
	bool endFading = true;
	bool end = false;
	bool start = true;
	bool FadeAtEnd => end && endFading;
	bool FadeAtStart => start && startFading;

	float startTime;
	float endTime;

	string newSceneName;

	public void LoadScene(string sceneName)
	{
		if (!endFading)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
		}
		else
		{
			GameObject pic = Instantiate(fadingPic);
			SpriteRenderer i = pic.GetComponent<SpriteRenderer>();
			fader = i.FadeIn(fadingTime);
			end = true;
			newSceneName = sceneName;
			endTime = Time.timeSinceLevelLoad;
		}
	}

	IEnumerator fader;

	private void Start()
	{
		if(FadeAtStart)
		{
			startTime = Time.timeSinceLevelLoad;
			SpriteRenderer sr = Instantiate(fadingPic).GetComponent<SpriteRenderer>();
			fader = sr.FadeOut(fadingTime);
		}
	}

	void Update()
    {
		if (FadeAtStart)
		{
			if (Time.timeSinceLevelLoad > startTime + fadingTime)
				start = false;
			else
				fader.MoveNext();
		}
		if (FadeAtEnd)
		{
			if (Time.timeSinceLevelLoad > endTime + fadingTime)
				UnityEngine.SceneManagement.SceneManager.LoadScene(newSceneName);
			else
				end = false;
		}
	}
}

static class SpriteRendererExtensions
{
	public static IEnumerator FadeIn(this SpriteRenderer renderer, float time)
	{
		float oldAlpha = renderer.color.a;
		renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
		while (renderer.color.a < oldAlpha)
		{
			renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b,
				Mathf.Min(1, renderer.color.a + Time.deltaTime / time));
			yield return null;
		}
	}

	public static IEnumerator FadeOut(this SpriteRenderer renderer, float time)
	{
		while (renderer.color.a > 0)
		{
			renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b,
				Mathf.Max(0, renderer.color.a - Time.deltaTime / time));
			yield return null;
		}
	}
}