using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {
	public GameObject panel;
	bool show;


	public void Menu(){
		Application.LoadLevel(0);
	}
	public void Info(){
		if(!show){
			panel.SetActive(true);
			show = true;
		}else if(show){
			panel.SetActive(false);
			show = false;
		}
	}
	public void Quit(){
		Application.Quit();
	}
}
