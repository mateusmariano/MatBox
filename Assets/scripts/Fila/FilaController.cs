using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilaController : MonoBehaviour {

	public List<GameObject> fila =  new List<GameObject>();
	public GameObject[] auxfilainstancia;
	public int[] filavalues;
	public GameObject elemento; 
	public int limite, topo;
	public Text pushvalue,limitevalue,tam_f;
	public DebugController debug;
	float actual_y;

	void Start () {
		//chamar o SetInitialData para setar os dados iniciais da cena olhar pilha
	}
	
	void Update () {
		// fazer o tam_f receber o valor do tamanho da fila olhar pilha
	}

	public void SetInitialData(){
		//olhar pilha
	}

	#region limite
	public void Limite(){ 
		//olhar pilha
	}
	#endregion

	#region pop_push
	public void Push(){ //void push, ela tambem instancia os Gameobjects da fila na tela.
		//olhar pilha e aplicar a logica de fila

	}
	public void Pop(){ //void pop, remove o elemento no fim da fila, e destroi o gameobject instanciado;
		//olhar pilha e aplicar a logica de fila
	}
	#endregion

	public void Menu(){
		Application.LoadLevel (0);
	}
}
