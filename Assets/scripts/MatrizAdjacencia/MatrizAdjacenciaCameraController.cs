using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrizAdjacenciaCameraController : MonoBehaviour {

	public GameObject camera;
	public Toggle camLivreToggle, camLockToggle;
	public bool camLock, camLivre;
	public Transform cameraCenter;
	float x, y, vertical;
	public Rigidbody rb;

	void Start () {
		camera = gameObject;
		camLivre  = false;
		rb = GetComponent<Rigidbody>();
		camLock = true;
	}
	
	void Update () {
		camLivre = camLivreToggle.isOn;
		camLock = camLockToggle.isOn;
		// a region abaixo faz a movimentacao da camera. caso !freecam, a camera fica girando ao redor do centro do grafo, caso freecam ela tem zoom e visao livre com o mouse 
		#region camerarot
		if(!camLivre){
			if(!camLock){
				camera.transform.LookAt(cameraCenter.position);
				camera.transform.Translate(Vector3.right * 1 * Time.deltaTime);
			}
		}else{
			if(!camLock){
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
}
