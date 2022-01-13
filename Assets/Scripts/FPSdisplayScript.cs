using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSdisplayScript : MonoBehaviour
{
	string label = "";
	float count;

	IEnumerator Start()
	{
		GUI.depth = 2;
		while (true)
		{
			if (Time.timeScale == 1)
			{
				yield return new WaitForSeconds(0.05f);  //0.1
				count = (1 / Time.deltaTime);
				label = "FPS :" + (Mathf.Round(count));
			}
			else
			{
				label = "Pause";
			}
			yield return new WaitForSeconds(0.25f);   //0.5
		}
	}

	void OnGUI()
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = 50;
		style.normal.textColor = Color.blue;
		GUI.Label(new Rect(100, 100, 250, 250), label, style);
	}
}
