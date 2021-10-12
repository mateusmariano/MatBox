using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoricoOperacoesController : MonoBehaviour {

	public Text operacoesText;
	public int contadorOperacoes = 0;

	public void LimparHistorico(){
		contadorOperacoes = 0;
		operacoesText.text = "";
	}

	public void EscreverOperacao(string valor, string operacao){
		contadorOperacoes++;
		operacoesText.text = FormataTexto (valor, operacao, contadorOperacoes.ToString ());
	}

	private string FormataTexto(string valor, string operacao, string contador){
		return 	operacoesText.text + "\n" + contadorOperacoes.ToString () + "º." + operacao + "(" + valor + ")";
	}
}
