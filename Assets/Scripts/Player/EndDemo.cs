using UnityEngine;
using System.Collections;

public class EndDemo : MonoBehaviour {
	public GameObject lights;
	public GameObject[] cameras;
	public GameObject monsterPlane;
	public AudioClip piano;
	public AudioClip scream;

	private bool ending;
	// Use this for initialization
	void Start () {
		ending = false;
	}
	
	// Update is called once per frame
	void OnTriggerEnter () {
		if (!ending) {
			ending = true;
			StartCoroutine(FinishDemo());
		}
	}

	IEnumerator FinishDemo () {
		audio.PlayOneShot(piano);
		monsterPlane.SetActive(true);
		
		yield return new WaitForSeconds(0.5f);
		
		lights.SetActive(false);
		monsterPlane.SetActive(false);
		
		yield return new WaitForSeconds(1);
		
		cameras[0].SetActive(false);
		cameras[1].SetActive(false);
		audio.PlayOneShot(scream);
		
		yield return new WaitForSeconds(3);
		
		Application.Quit();
	}
}
