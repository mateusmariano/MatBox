using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

	private Transform target;
	public Toggle freecam;

	public void CameraLivre(){
		transform.LookAt(target.position);
		transform.Translate(Vector3.right * 1 * Time.deltaTime);
	}

	public void CameraFixa(){
		transform.position = transform.position;
	}
}
