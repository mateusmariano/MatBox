using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PilhaController :MonoBehaviour {
	
	public List<GameObject> pilha =  new List<GameObject>();
	public GameObject[] auxpilhainstancia;
	public int[] pilhavalues;
	public GameObject elemento; 
	public int limite, topo;
	public Text pushvalue,limitevalue,tam_p;
	float actual_y;

	void Start(){
		SetInitialData ();
	}
	void Update(){ 
		tam_p.text = (topo+1).ToString();
	}
	public void Limite(){ 
		int curlimite = limite;
		if(System.Convert.ToInt32(limitevalue.text) >= curlimite){
			limite = System.Convert.ToInt32(limitevalue.text);
			System.Array.Resize(ref auxpilhainstancia,auxpilhainstancia.Length + (limite-auxpilhainstancia.Length));
			System.Array.Resize(ref pilhavalues,pilhavalues.Length + (limite-pilhavalues.Length));
		}
		else{
			Debug.Log("Erro");
		}
	}
	void SetInitialData(){
		topo = -1;
		actual_y = -5.5f;
	}
	#region pop_push
	public void Push(){ //void push, ela tambem instancia os Gameobjects da pilha na tela.
		if(topo == limite){
			Debug.Log("limite da pilha atingido");
		}
		else if(topo < limite-1){
			topo++;
			actual_y += 0.9f; // controle do Y do Gameobject;
			pilha.Add(elemento); // adiciona elemento na pilha;
			auxpilhainstancia[topo] = Instantiate(pilha[topo],new Vector2(-6.3f,actual_y),Quaternion.identity) as GameObject;
			auxpilhainstancia[topo].GetComponentInChildren<Text>().text = pushvalue.text;
			pilhavalues[topo] = System.Convert.ToInt32(pushvalue.text);
		}
	}
	public void Pop(){ //void pop, remove o elemento no topo da pilha, e destroi o gameobject instanciado;
		if(topo < 0){
			Debug.Log("a pilha esta vazia");
		}
		else{
			Destroy(auxpilhainstancia[topo]);
			pilha.Remove(pilha[topo]);
			pilhavalues[topo] = -1;
			topo--;
			actual_y -= 0.9f;
		}
	}
	#endregion
	#region unaria 
	// esta region faz o calculo da DEC
	public void Dec(){
		if(topo < 0){
			Debug.Log("a pilha esta vazia");
		}
		else{
			pilhavalues[topo] = pilhavalues[topo] -1;
			auxpilhainstancia[topo].GetComponentInChildren<Text>().text = pilhavalues[topo].ToString();
		}
	}
	#endregion
	#region binaria
	// esta region faz o calculo da ADD,SUB,MPY & DIV
	private double Add(){
		return pilhavalues[topo-1] + pilhavalues[topo];
	}
	private double Sub(){
		return pilhavalues[topo-1] - pilhavalues[topo];
	}
	private double Mpy(){
		return pilhavalues[topo-1] * pilhavalues[topo];
	}
	private double Div(){
		return pilhavalues[topo-1] / pilhavalues[topo];
	}
	#endregion

	#region controleOperacoes
	public void ControleOperacoes(int operacao){
		double valor = 0;

		if(topo < 0 ){
			Debug.Log("pilha vazia");
		}
		else if(topo < 2 && topo == 0){
			Debug.Log(" a pilha precisa ter no minimo 2 elementos para esta operacao");
		}
		else{
			switch (operacao) {
				case 1:
					valor = Add ();
					break;
				case 2: 
					valor = Sub ();
					break;
				case 3:
					valor = Mpy ();
					break;
				case 4:
					valor = Div ();
					break;
			}
			TrataPilha (valor);
		}
	}
	#endregion

	#region trataPilha
	public void TrataPilha(double valor){
		for(int i = topo; i > topo -2; i --){
			pilhavalues[i] = -1;
			pilha.Remove(pilha[i]);
			Destroy(auxpilhainstancia[i]);
		}
		topo--;
		actual_y -= 0.9f;
		pilha.Add(elemento);
		auxpilhainstancia[topo] = Instantiate(pilha[topo],new Vector2(-6.3f,actual_y),Quaternion.identity) as GameObject;
		pilhavalues[topo] = (int)valor;
		auxpilhainstancia[topo].GetComponentInChildren<Text>().text = valor.ToString();
	}
	#endregion

	public void Menu(){
		Application.LoadLevel (0);
	}
}



	