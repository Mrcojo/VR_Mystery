using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

	public RaycastHit hit;
	private GameObject gameObjectHit;
	private GameObject lastWaypointHit;
	// Use this for initialization
	void Start () {
		hit = new RaycastHit();
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 500, 9)) {
			gameObjectHit = hit.transform.gameObject;
			if (gameObjectHit.tag == "WayPoint") {
				lastWaypointHit = gameObjectHit;
				lastWaypointHit.transform.GetChild(0).gameObject.SetActive(true);
			}
			else {
				if (lastWaypointHit != null && lastWaypointHit.tag == "WayPoint")  {
					lastWaypointHit.transform.GetChild(0).gameObject.SetActive(false);
				}
			}
		}
		else {
			lastWaypointHit.transform.GetChild(0).gameObject.SetActive(false);
		}
	}
}
