using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilaController : MonoBehaviour {

	public List<GameObject> fila =  new List<GameObject>();
	public GameObject[] auxfilainstancia;
	public int[] filavalues;
	public GameObject elemento; 
	public int limite, inicio, fim;
	public Text pushvalue,limitevalue,tam_f;
	public DebugController debug;
	float actual_x;

	void Start () {
		SetInitialData ();
	}
	
	void Update () {
		// fazer o tam_f receber o valor do tamanho da fila olhar pilha
	}

	public void SetInitialData(){
		inicio = 0;
		fim = -1;
		actual_x = -7f;
	}

	#region limite
	public void Limite(){ 
		//olhar pilha
	}
	#endregion

	#region pop_push
	public void Push(){ //void push, ela tambem instancia os Gameobjects da fila na tela.
		if(!TemErros ()){
			fim++;
			actual_x += 1f; // incremento da posição em Y dos objetos da pilha
			fila.Add(elemento); // adiciona elemento na pilha;
			auxfilainstancia[fim] = Instantiate(fila[fim], 
									new Vector2(actual_x,-2.17f), 
									Quaternion.Euler (0,0,90)) as GameObject;
			//atualiza o texto do elemento inserido
			auxfilainstancia[fim].GetComponentInChildren<Text>().text = pushvalue.text;
			filavalues[fim] = System.Convert.ToInt32(pushvalue.text);
			//atualiza o historico de operacoes
			//*historico.EscreverOperacao (pilhavalues[fim].ToString (), "Push");
		}

	}
	public void Pop(){ //void pop, remove o elemento no fim da fila, e destroi o gameobject instanciado;
		//olhar pilha e aplicar a logica de fila
	}
	#endregion

	#region TrataFila
	public bool TemErros(){

		return false;
	}
	#endregion

	public void Menu(){
		Application.LoadLevel (0);
	}
}
