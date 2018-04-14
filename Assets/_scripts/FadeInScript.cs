using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour {
    public UnityEngine.UI.Text text;
    public float delay, delayTime;
    bool done = false;
	// Use this for initialization
	void Start () {
        delayTime = Time.time + delay;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time>delayTime && !done)
        {
            StartCoroutine(FadeTextToFullAlpha(3f, text));
            done = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("MainMenu");
	}
    
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r + (Time.deltaTime / t), i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
