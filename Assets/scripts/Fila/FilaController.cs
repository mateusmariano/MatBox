using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class FilaController : MonoBehaviour {

	public List<GameObject> fila =  new List<GameObject>();
	public GameObject[] auxfilainstancia;
	public int[] filavalues;
	public GameObject elemento; 
	public int limite, inicio, fim;
	public Text pushvalue,limitevalue,tam_f;
	public DebugController debug;
	float actual_x;
	public HistoricoOperacoesController historico;

	/// <summary>
	/// ajeitar dimensionamento do valor da fila e da pilha
	/// colocar indicador de memoria (1º, 2º...)
	/// </summary>

	void Start () {
		SetInitialData ();
	}
	
	void Update () {
		tam_f.text = (fim + 1).ToString(); // atualiza o texto do tamanho da fila
	}

	public void SetInitialData(){
		inicio = 0;
		fim = -1;
		actual_x = -7f;
	}

	#region limite
	public void Limite(){ 
		// caso o texto do limite não seja vazio, continua a alteração do limite
		if (limitevalue.text != "") {
			// caso o valor digitado seja maior que o tamanho da pilha atual, continua a alteração
			if (System.Convert.ToInt32 (limitevalue.text) >= System.Convert.ToInt32 (tam_f.text)) { 
				limite = System.Convert.ToInt32 (limitevalue.text);
				System.Array.Resize (ref auxfilainstancia, auxfilainstancia.Length
					+ (limite - auxfilainstancia.Length));
				System.Array.Resize (ref filavalues, filavalues.Length
					+ (limite - filavalues.Length));
				historico.EscreverOperacao (limitevalue.text, "Limite");
			}
			// caso o valor digitado seja menor que o tamanho da pilha atual, entra em exceção
			else {
				debug.ShowDebug ("Não é possível mudar o  \n " +
					"limite para um valor inferior  \n " +
					"ao tamanho da fila.");
			}
		}
		// caso o valor digitado seja vazio
		else {
			debug.ShowDebug ("Insira um valor para ser  \n " +
				"adicionado como limite.");
		}
	}
	#endregion

	#region pop_push
	public void Push(){ //void push, ela tambem instancia os Gameobjects da fila na tela.
		if(!TemErros ()){
			fim++;
			actual_x += 1f; // incremento da posição em Y dos objetos da fila
			fila.Add(elemento); // adiciona elemento na fila;
			auxfilainstancia[fim] = Instantiate(fila[fim], 
									new Vector2(actual_x,-2.17f), 
									Quaternion.Euler (0,0,90)) as GameObject;
			//atualiza o texto do elemento inserido
			auxfilainstancia[fim].GetComponentInChildren<Text>().text = pushvalue.text;
			filavalues[fim] = System.Convert.ToInt32(pushvalue.text);
			//atualiza o historico de operacoes
			historico.EscreverOperacao (filavalues[fim].ToString (), "Push");
		}

	}
	public void Pop(){ //void pop, remove o elemento no fim da fila, e destroi o gameobject instanciado;
		if(fim < 0){
			debug.ShowDebug("Não é possível \n realizar a operação, \n a fila esta vazia");
		}
		else{
			//atualiza o historico de operacoes
			historico.EscreverOperacao (filavalues[inicio].ToString (), "Pop");
			Destroy(auxfilainstancia[inicio]); //destroi o objeto no topo
			fila.Remove(fila[inicio]); //remove da lista da fila
			fim--;
			actual_x -= 1f; // decremento da posição em Y dos objetos da fila
			TrataFila();
		}
	}
	#endregion
	#region unaria
	public void Dec(){
		if(inicio < 0){
			debug.ShowDebug("Não é possível \n realizar a operação, \n a pilha esta vazia");
		}
		else{
			historico.EscreverOperacao ("1", "Dec");
			filavalues[inicio] = filavalues[inicio] - 1;
			auxfilainstancia[inicio].GetComponentInChildren<Text>().text = filavalues[inicio].ToString();

		}
	}
	#endregion
	#region TrataFila
	public void TrataFila(){
		for(int atual = 1; atual < auxfilainstancia.Length; atual ++){
			auxfilainstancia [atual - 1] = auxfilainstancia [atual];
			filavalues [atual - 1] = filavalues [atual];
			if(auxfilainstancia[atual] != null){
				auxfilainstancia [atual].transform.position = new Vector3(
										auxfilainstancia[atual].transform.position.x -1,
										auxfilainstancia[atual].transform.position.y,
										auxfilainstancia[atual].transform.position.z);
			}

		}
	}
	public bool TemErros(){
		if(pushvalue.text == ""){ 
			debug.ShowDebug ("Insira um valor para ser  \n adicionado à fila.");
			return true;	
		}
		else if(!Regex.IsMatch (pushvalue.text, "^-?[0-9]+$")){
			debug.ShowDebug ("Insira um valor numérico  \n adicionado à fila.");
			return true;	
		}
		if (fim + 1 == limite) {
			debug.ShowDebug ("Não é possível adicionar \n mais  objetos,o limite da  \n fila foi atingido.");
			return true;	
		} else if (fim < limite - 1) {
			return false;
		}

		return false;
	}
	#endregion

}
