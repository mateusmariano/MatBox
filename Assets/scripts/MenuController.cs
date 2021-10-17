using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	 
	public void matriz (){
		SceneManager.LoadScene ("MatrizAdjacencia");
	}
	public void coord (){
		Application.LoadLevel("CoordHomo");
		
	}
	public void pilha (){
		Application.LoadLevel("Pilha");
		
	}
	public void fila (){
		Application.LoadLevel("Fila");

	}
}
