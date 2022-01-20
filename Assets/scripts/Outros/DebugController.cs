using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour {

	public CanvasGroup canvasGroup;
	public Text debugText;
	public bool ativado;
	public Animator anim;

	public void ShowDebug(string msg){
		anim.SetInteger ("fade", 1);
		debugText.text = msg;
		Time.timeScale = 0.2f; //diminuindo a velocidade da aplicação
		canvasGroup.blocksRaycasts = true; 
		if(!ativado){
			Invoke ("DisableDebug", 4f);
			ativado = true;
		}
	}

	public void DisableDebug(){
		anim.SetInteger ("fade", 0);
		debugText.text = "";
		ativado = false;
		Time.timeScale = 1;
		canvasGroup.blocksRaycasts = false; 
	}
}
