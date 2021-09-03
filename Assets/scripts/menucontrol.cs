using UnityEngine;
using System.Collections;

public class menucontrol : MonoBehaviour {
	 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void matriz (){
		Application.LoadLevel(1);
	}
	public void coord (){
		Application.LoadLevel(2);
		
	}
	public void pilha (){
		Application.LoadLevel(3);
		
	}
}
