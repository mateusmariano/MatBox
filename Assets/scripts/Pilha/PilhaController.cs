using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class PilhaController :MonoBehaviour {
	
	public List<GameObject> pilha =  new List<GameObject>();
	public GameObject[] auxpilhainstancia;
	public int[] pilhavalues;
	public GameObject elemento; 
	public int limite, topo;
	public Text pushvalue,limitevalue,tam_p;
	public DebugController debug;
	float actual_y;
	public HistoricoOperacoesPilhaController historico;


	void Start(){
		SetInitialData ();
	}
	void Update(){ 
		tam_p.text = (topo+1).ToString();
	}
	void SetInitialData(){
		topo = -1;
		actual_y = -4.67f;
	}
	#region limite
	public void Limite(){ 
		int curlimite = limite;
		if (limitevalue.text != "") {
			if (System.Convert.ToInt32 (limitevalue.text) >= curlimite) {
				limite = System.Convert.ToInt32 (limitevalue.text);
				System.Array.Resize (ref auxpilhainstancia, auxpilhainstancia.Length + (limite - auxpilhainstancia.Length));
				System.Array.Resize (ref pilhavalues, pilhavalues.Length + (limite - pilhavalues.Length));
			} else {
				debug.ShowDebug ("Não é possível mudar o  \n limite para um valor inferior  \n ao tamanho da pilha.");
			}
		} else {
			debug.ShowDebug ("Insira um valor para ser  \n adicionado como limite.");
		}
	}
	#endregion
	#region pop_push
	public void Push(){ //void push, ela tambem instancia os Gameobjects da pilha na tela.
		if(!TemErros ()){
			topo++;
			actual_y += 0.9f; // controle do Y do Gameobject;
			pilha.Add(elemento); // adiciona elemento na pilha;
			auxpilhainstancia[topo] = Instantiate(pilha[topo],new Vector2(-6.4f,actual_y),Quaternion.identity) as GameObject;
			auxpilhainstancia[topo].GetComponentInChildren<Text>().text = pushvalue.text;
			pilhavalues[topo] = System.Convert.ToInt32(pushvalue.text);
			historico.EscreverOperacao (pilhavalues[topo].ToString (), "Push");
		}

	}
	public void Pop(){ //void pop, remove o elemento no topo da pilha, e destroi o gameobject instanciado;
		if(topo < 0){
			debug.ShowDebug("Não é possível \n realizar a operação, \n a pilha esta vazia");
		}
		else{
			historico.EscreverOperacao (pilhavalues[topo].ToString (), "Pop");
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
			debug.ShowDebug("Não é possível \n realizar a operação, \n a pilha esta vazia");
		}
		else{
			historico.EscreverOperacao ("1", "Dec");
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
			debug.ShowDebug("Não é possível  \n realizar a operação,  \n pois a pilha está vazia.");
		}
		else if(topo < 2 && topo == 0){
			debug.ShowDebug("A pilha precisa ter  \n no mínimo 2 elementos  \n para esta operação.");
		}
		else{
			switch (operacao) {
				case 1:
					valor = Add ();
					historico.EscreverOperacao (pilhavalues[topo].ToString (), "Add");
					break;
				case 2: 
					valor = Sub ();
					historico.EscreverOperacao (pilhavalues[topo].ToString (), "Sub");
					break;
				case 3:
					valor = Mpy ();
					historico.EscreverOperacao (pilhavalues[topo].ToString (), "Mpy");
					break;
				case 4:
					valor = Div ();
					historico.EscreverOperacao (pilhavalues[topo].ToString (), "Div");
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

	public bool TemErros(){
		if(pushvalue.text == ""){
			debug.ShowDebug ("Insira um valor para ser  \n adicionado à pilha.");
			return true;	
		} 
		else if(!Regex.IsMatch (pushvalue.text, "^[0-9]+$")){
			debug.ShowDebug ("Insira um valor numérico  \n adicionado à pilha.");
			return true;	
		}
		if (topo + 1 == limite) {
			debug.ShowDebug ("Não é possível adicionar \n mais  objetos,o limite da  \n pilha foi atingido.");
			return true;	
		} else if (topo < limite - 1) {
			return false;
		}

		return false;
	}
	#endregion

	public void Menu(){
		Application.LoadLevel (0);
	}
}



	