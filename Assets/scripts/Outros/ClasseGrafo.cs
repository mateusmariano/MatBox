using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClasseGrafo <ntype,etype>{

	public ClasseGrafo(){
		nodes = new List<Node<ntype>>(); // lista dos nodes
		edges = new List<Edge<etype,ntype>>(); // lista das arestas
	}
	public List<Node<ntype>> nodes{get;private set;}
	public List<Edge<etype,ntype>> edges{get;private set;}
}
public class Node<gvalor>{ // classe NODE com variavel gvalor, que auxiliara na classe EDGE
	public gvalor valor{get;set;}
}
public class Edge<evalor,gvalor>{ //classe EDGE com variavel valor, from e to
	public evalor valor{get;set;}
	public Node<gvalor> From {get;set;}
	public Node<gvalor> to {get;set;}
	
}