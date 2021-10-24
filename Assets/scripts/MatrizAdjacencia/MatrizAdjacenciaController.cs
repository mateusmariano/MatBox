using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class MatrizAdjacenciaController : MonoBehaviour {

	private ClasseGrafo<Vector3,int> grafo;
	public int[,] matriz = new int[4,4];
	public int[] matrizvalues;
	public Text[] valuestxt;
	public InputField[] matrizInputFields;
	public Transform[] posicoesNos;
	public GameObject node;
	public GameObject[] nodes;
	public GameObject laco;
	public GameObject[] lacos;
	public GameObject[] lacosAux;
	public Material arestaMaterial;
	public Text matriztext;
	public bool mostrar;
	public Transform center;
	public Toggle autofill;
	public bool autoPreenchimento;
	public CanvasGroup painel;
	public Text btn_esconderText;
	public DebugController debug;

	void Start () {
		SetInitialData ();
	}

	void SetInitialData(){
		//o for abaixo pega os InputFields para o autopreenchimento
		for(int a = 0 ; a < 16; a ++){
			matrizInputFields[a] = valuestxt[a].GetComponentInParent<InputField>();
		}
	}

	void Update(){
		autoPreenchimento = autofill.isOn;
	}
	// a void abaixo monta o grafo e faz algumas verificaçoes
	public void CriarGrafo(){

		if(TemErros()){
			return;
		}
		grafo = new ClasseGrafo<Vector3,int> (); // instancia da classe grafo
		#region default
		if(grafo.edges.Count > 0){  //caso ja tenha alguma aresta, ele tira da lista;
			for(int x = 0; x < grafo.edges.Count; x ++){
				grafo.edges.RemoveAt(x);
			}
		}
		//caso tenha algum GameObject (node) ele o Destroy da cena. 
		//Utilizado para nao ficar com varias esferas sobrepostas na cena;
		for(int t = 0; t < 4; t ++){ 
			if(nodes[t] != null){
				Destroy(nodes[t]);
			}
		}
			for(int i = 0; i < 4; i ++){
				grafo.nodes.Add( new Node<Vector3>(){valor = posicoesNos[i].position});
				// chama a funcao para instanciar as esferas(nodes) na tela;
				Points(grafo.nodes[i].valor,i + 1,i); 
			}
		for(int k = 0 ; k < 4; k ++){ // caso tenha algum laco(GameObject) na cena, este e destruido;
			if(lacosAux[k] != null){
				Destroy(lacosAux[k]);
			}
		}

		#endregion
		#region manual
		if(!autoPreenchimento){  	//preenchimento manual da matriz de adjacencia
			for(int k = 0; k < valuestxt.Length; k ++){
				matrizvalues[k] = System.Convert.ToInt32(valuestxt[k].text);
				matriz[k/4,k%4] = matrizvalues[k];
			}
		}
		#endregion
		#region auto
		if(autoPreenchimento){//preenchimento automatico da matriz de adjacencia
			int rowaux = 1; 
			// este for preenche primeiro os valores da diagonal principal e acima dela
			for(int i = 0; i <4; i ++){
				for(int j = 0; j < rowaux; j++){
					// o Random.Range trabalha com : min inclusivo e max exclusivo
					matriz[i,j] = Random.Range(0,2);
				}
				rowaux ++;
			}
			rowaux = 1;
			// este for apenas "reflete" os valores para abaixo da diagonal principal
			for(int l = 0; l < 4; l++){
				for(int k = rowaux; k < 4; k ++){
					matriz[l,k] = matriz[k,l];
				}
				rowaux ++;
			}
			//preenche os valores ods inputs com os valores da matriz
			for(int x = 0; x < 16; x ++){
				matrizInputFields[x].text = matriz[x/4,x%4].ToString();
			}
		}
		#endregion
		// este for adiciona arestas na lista de 'grafos.edges'
		for(int i = 0; i < 4; i ++){ 
			for(int j = 0; j < 4; j ++){
				if(matriz[i,j] == 1){
					grafo.edges.Add(new Edge<int, Vector3>(){
									valor = 1, From = grafo.nodes[i],to = grafo.nodes[j]});
				}
			}
		}
		//este for instancia os lacos, caso existam, na posicao do node e em uma rotacao randomica em Z 
		for(int i = 0; i < 4; i ++){
			for(int j = 0; j < 4; j ++){
				if(matriz[i,j] == 1 && i == j){
					if(lacos.Length <= 4){
						lacos[i] = Instantiate(laco,posicoesNos[i].position,
								Quaternion.Euler(0,0,Random.Range(0,361))) as GameObject;
						lacosAux[i] = lacos[i];
					}
				}
			}
		}
		// variavel de controle para a GL. 
		//Apos todo o procedimento acima e liberado para a GL desenhar as arestas
		mostrar = true; 
	}

	//Chamada apos uma camera ser renderizada na cena
	void OnPostRender(){ 
		Lines();
	}
	void Lines(){ // void que desenha as arestas coloridas.
		GL.Begin(GL.LINES);
		arestaMaterial.SetPass(0);
		if(mostrar){
			for(int j = 0; j < grafo.edges.Count; j ++){
					GL.Color(new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f)));
					GL.Vertex(grafo.edges[j].From.valor);
					GL.Color(new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f)));
					GL.Vertex(grafo.edges[j].to.valor);
			}
		}
		GL.End();
	}
	void Points(Vector3 point, int value,int index){ // void que instancia os ndes
		nodes[index] = Instantiate(node,point,Quaternion.identity) as GameObject;
		nodes[index].GetComponentInChildren<Text>().text = value.ToString();
	}

	public void EscondePanel(){
		painel.alpha = painel.alpha == 0 ? 1 : 0;
		btn_esconderText.text = btn_esconderText.text.Contains ("ESCONDER") ? "MOSTRAR PAINEL" : "ESCONDER PAINEL";
	}

	bool TemErros (){
		if (autoPreenchimento) {
			return false;
		}
		for(int i = 0 ; i < valuestxt.Length; i ++){
			if(valuestxt[i].text == "" || !Regex.IsMatch (valuestxt[i].text , "^?[0-9]+$")){
				debug.ShowDebug ("Os valores da matriz \n não foram inseridos \n " +
								"corretamente. Insira \napenas números positivos");
				return true;
			}
		}
		return false;
	}
}
