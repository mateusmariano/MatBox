using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour {

	public CanvasGroup canvasGroup;
	public Text debugText;
	public bool ativado;

	public void ShowDebug(string msg){
		canvasGroup.alpha = 1;
		debugText.text = msg;
		if(!ativado){
			Invoke ("DisableDebug", 4f);
			ativado = true;
		}
	}

	void DisableDebug(){
		canvasGroup.alpha = 0;
		debugText.text = "";
		ativado = false;
	}
}
