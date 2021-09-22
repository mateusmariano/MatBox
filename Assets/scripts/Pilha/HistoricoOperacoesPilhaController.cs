using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoricoOperacoesPilhaController : MonoBehaviour {

	public Text operacoesText;


	void Start () {
		
	}
	
	void Update () {
		
	}

	public void EscreverOperacao(string valor, string operacao){
		operacoesText.text = operacoesText.text + "\n" + operacao + "(" + valor + ")";
	}
}
