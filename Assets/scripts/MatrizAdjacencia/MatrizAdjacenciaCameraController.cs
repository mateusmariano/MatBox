using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrizAdjacenciaCameraController : MonoBehaviour {

	public GameObject camera;
	public Toggle camLockToggle;
	public bool camLock;
	public Transform cameraCenter;
	float x, y, vertical;
	public Rigidbody rb;

	void Start () {
		camera = gameObject;
		rb = GetComponent<Rigidbody>();
		camLock = true;
	}
	
	void Update () {
		camLock = camLockToggle.isOn;
		// a region abaixo faz a movimentacao da camera. caso !freecam, a camera fica girando ao redor do centro do grafo, caso freecam ela tem zoom e visao livre com o mouse 
		#region camerarot
		if(!camLock){
			camera.transform.LookAt(cameraCenter.position);
			camera.transform.Translate(Vector3.right * 5 * Time.deltaTime);
		}
		#endregion
	}
}
