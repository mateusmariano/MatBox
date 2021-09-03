using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class controller : MonoBehaviour {

	private ClasseGrafo<Vector3,int> grafo;
	public int numberofnodes;
	public int[,] matriz = new int[4,4];
	public int[] matrizvalues;
	public Text[] valuestxt;
	public InputField[] valuestxtinputs;
	public Transform[] nodespositions;
	public GameObject node;
	public GameObject[] nodesgam;
	public GameObject tie;
	public GameObject[] ties;
	public GameObject[] tiesaux;
	public Material mat;
	public Text matriztext;
	int counter;
	EventSystem system;
	public bool can;
	public GameObject cam;
	private int nodecountcontrol;
	bool rotnow;
	public Transform center;
	bool freecam;
	public Toggle _freecamtg,autofill;
	float x, y,vertical;
	public Rigidbody rb;
	public bool auto;

	void Start () {
		system =  EventSystem.current;
		nodecountcontrol = 0;
		freecam  = false;
		for(int a = 0 ; a < 16; a ++){
			valuestxtinputs[a] = valuestxt[a].GetComponentInParent<InputField>();
		}
	}
	void Update(){
		//o if abaixo e usado para passar os labels usando o TAB
		if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)){
			Selectable prox = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
			if(prox != null){
				InputField inputfield = prox.GetComponent<InputField>();
				if (inputfield != null)
					inputfield.OnPointerClick(new PointerEventData(system));
				
				system.SetSelectedGameObject(prox.gameObject, new BaseEventData(system));

			}
		}
		freecam = _freecamtg.isOn;
		auto = autofill.isOn;
		rb = GetComponent<Rigidbody>();

		// a region abaixo faz a movimentacao da camera. caso !freecam, a camera fica girando ao redor do centro do grafo, caso freecam ela tem zoom e visao livre com o mouse 
		#region camerarot
		if(!freecam){
			if(rotnow){
				cam.transform.LookAt(center.position);
				cam.transform.Translate(Vector3.right * 1 * Time.deltaTime);
			}
		}else{
			if(rotnow){
				y += 2 * Input.GetAxis("Mouse X");
				x -= 2 * Input.GetAxis("Mouse Y");
				transform.eulerAngles =  new Vector3(x,y,0);
				vertical = Input.GetAxis("Vertical");
				Vector3 mov = new Vector3(transform.forward.y * vertical,0,vertical);
				rb.velocity = mov * 20;
			}
		}
		#endregion
	}
	// a void abaixo monta o grafo e faz algumas verificaçoes
	public void Make(){
		grafo = new ClasseGrafo<Vector3,int> (); // instancia da classe grafo
		#region default
		if(grafo.edges.Count > 0){  //caso ja tenha alguma aresta, ele tira da lista;
			for(int x = 0; x < grafo.edges.Count; x ++){
				grafo.edges.RemoveAt(x);
			}
		}
		for(int t = 0; t < 4; t ++){ //caso tenha algum GameObject (node) ele Destroy da cena. Utilizado para nao ficar com varias esferas sobrepostas na cena;
			if(nodesgam[t] != null){
				Destroy(nodesgam[t]);
			}
		}
		if(nodecountcontrol <= 0){ // adiciona nodes na lista de 'grafos'.
			for(int i = 0; i < 4; i ++){
				grafo.nodes.Add( new Node<Vector3>(){valor = nodespositions[i].position});
				Points(grafo.nodes[i].valor,i + 1,i); // chama a funcao para instanciar as esferas(nodes) na tela;
			}
		}
		for(int k = 0 ; k < 4; k ++){ // caso tenha algum laco(GameObject) na cena, este e destruido;
			if(tiesaux[k] != null){
				Destroy(tiesaux[k]);
			}
		}

		#endregion
		#region manual
		if(!auto){  	//preenchimento manual da matriz de adjacencia
			for(int k = 0; k < valuestxt.Length; k ++){
				matrizvalues[k] = System.Convert.ToInt32(valuestxt[k].text);
				matriz[k/4,k%4] = matrizvalues[k];
			}
		}
		#endregion
		#region auto
		if(auto){		//preenchimento automatico da matriz de adjacencia
			int rowaux = 1; 
			for(int i = 0; i <4; i ++){ // este for preenche primeiro os valores da diagonal principal e acima dela
				for(int j = 0; j < rowaux; j++){
					matriz[i,j] = Random.Range(0,2); // o Random.Range trabalha com : min inclusivo e max exclusivo
				}
				rowaux ++;
			}
			rowaux = 1;
			for(int l = 0; l < 4; l++){ // este for apenas "reflete" os valores para abaixo da diagonal principal
				for(int k = rowaux; k < 4; k ++){
					matriz[l,k] = matriz[k,l];
				}
				rowaux ++;
			}
			for(int x = 0; x < 16; x ++){
				valuestxtinputs[x].text = matriz[x/4,x%4].ToString(); //preenche os valores ods inputs com os valores da matriz
			}
		}
		#endregion
		for(int i = 0; i < 4; i ++){ // este for adiciona arestas na lista de 'grafos.edges'
			for(int j = 0; j < 4; j ++){
				if(matriz[i,j] == 1){
					grafo.edges.Add(new Edge<int, Vector3>(){valor = 1, From = grafo.nodes[i],to = grafo.nodes[j]});
				}
			}
		}
		for(int i = 0; i < 4; i ++){	//este for instancia os lacos, caso existam, na posicao do node e em uma rotacao randomica em Z 
			for(int j = 0; j < 4; j ++){
				if(matriz[i,j] == 1 && i == j){
					if(ties.Length <= 4){
						ties[i] = Instantiate(tie,nodespositions[i].position,Quaternion.Euler(0,0,Random.Range(0,361))) as GameObject;
						tiesaux[i] = ties[i];
					}
				}
			}
		}
		can = true; // variavel de controle para a GL. Apos todo o procedimento acima e liberado para a GL desenhar as arestas
		rotnow = true; // variavel de controle para a camera começar sua movementacao
	}

	void OnPostRender(){ // void auxliar utilizada pela biblioteca GL para chamada da funcao que ira desenhar as arestas. Chamada apos renderizacao de todos os objetos em cena
		Lines();
	}
	void Lines(){ // void que desenha as arestas coloridas.
		GL.Begin(GL.LINES);
		mat.SetPass(0);
		if(can){
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
		nodesgam[index] = Instantiate(node,point,Quaternion.identity) as GameObject;
		nodesgam[index].GetComponentInChildren<Text>().text = value.ToString();
	}

	public void reset(){ // void que reinicia o programa;
		int a =Application.loadedLevel;
		Application.LoadLevel(a);
	}
}
