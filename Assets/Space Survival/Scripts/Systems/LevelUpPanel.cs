using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
	public void ResumeButtonClick()
	{
		Time.timeScale = 1;
		gameObject.GetComponentInParent<Transform>().gameObject.SetActive(false);
	}
}
