using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CenaUtils : MonoBehaviour {

	public void Reset(){
		int cenaAtual = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (cenaAtual);
	}
	public void Menu(){
		SceneManager.LoadScene (0);
	}
}
