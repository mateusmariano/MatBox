using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	 
	public void matriz (){
		SceneManager.LoadScene ("MatrizAdjacencia");
	}
	public void coord (){
		SceneManager.LoadScene("CoordHomo");
		
	}
	public void pilha (){
		SceneManager.LoadScene("Pilha");
		
	}
	public void fila (){
		SceneManager.LoadScene("Fila");
	}
	public void sair (){
		Application.Quit ();
	}
}
