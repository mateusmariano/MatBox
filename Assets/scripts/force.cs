using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class force : MonoBehaviour {

	public List<Nodes> nodes;
	public Material matlines,matpoints;
	public GameObject nodeobject;
	public int[,] matrix = new int[3,2];
	public int[] vec;
	public Transform target;
	// Use this for initialization
	void Start () {
		nodes =  new List<Nodes>();
		for(int i = 0; i < 10; i ++){
			nodes.Add(new Nodes() {childrens = nodes.Where(node => Random.value > 0.5f).ToList(),pos = Random.insideUnitSphere * 8,vel = Vector3.zero});
		}
		foreach(var node in nodes){
			Points(node.pos);
		}
	}
	// Update is called once per frame
	void Update () {
		transform.LookAt(target.position);
		transform.Translate(Vector3.right * 1 * Time.deltaTime);

	}
	void OnPostRender(){
		Lines();
	}
	void Lines(){
		GL.Begin(GL.LINES);
		matlines.SetPass(0);
		foreach(var node in nodes){
			foreach(var connect in node.childrens){
				GL.Color(new Color(255,0,0,0.2f));
				GL.Vertex(node.pos);
				GL.Color(new Color(0,255,0,0.2f));
				GL.Vertex(connect.pos);
			}
		}

		GL.End();
	}
	void Points(Vector3 points){
		Instantiate(nodeobject,points, Quaternion.identity);
	}
}


