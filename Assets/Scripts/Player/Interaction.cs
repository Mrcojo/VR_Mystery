using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

	public RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 500, 9)) {
			//Debug.Log (hit.transform.name);
		}
	}
}
