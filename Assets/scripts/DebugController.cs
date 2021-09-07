using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour {

	public CanvasGroup canvasGroup;
	public Text debugText;


	public void ShowDebug(string msg){
		canvasGroup.alpha = 1;
		debugText.text = msg;
		Invoke ("DisableDebug", 4f);
	}

	void DisableDebug(){
		canvasGroup.alpha = 0;
		debugText.text = "";
	}
}
