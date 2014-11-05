using UnityEngine;
using System.Collections;

public enum Rotation {
	Left,
	Right,
	None
} 

public class PlayerController : MonoBehaviour {
	public float horizontalBorder;
	public float speed = 100.0f;
	public bool moving;

	private Rotation playerRotation;
	private Quaternion lastRotation;

	// Use this for initialization
	void Start () {
		moving = false;
		playerRotation = Rotation.None;
		lastRotation = transform.rotation;
		horizontalBorder = Screen.width / 4f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0))
		{

			if ( Input.mousePosition.x >= Screen.width - horizontalBorder )
			{
				playerRotation = Rotation.Right;
			}       
			else if ( Input.mousePosition.x <= horizontalBorder )
			{
				playerRotation = Rotation.Left;
			} 
			else
			{
				playerRotation = Rotation.None;
			} 
		}

		switch (playerRotation) {
			case Rotation.None: 
				break;
			case Rotation.Left: 
				if (Vector3.Distance(transform.eulerAngles, (lastRotation * Quaternion.Euler(0, -90, 0)).eulerAngles) > 1f) {
					transform.Rotate(Vector3.up, -speed * Time.deltaTime);
				}
				else  {
					lastRotation = transform.rotation;
					playerRotation = Rotation.None;
				}
				
				break;
			case Rotation.Right:
				if (Vector3.Distance(transform.eulerAngles, (lastRotation * Quaternion.Euler(0, 90, 0)).eulerAngles) > 1f) {
					transform.Rotate(Vector3.up, speed * Time.deltaTime);
				}
				else  {
					lastRotation = transform.rotation;
					playerRotation = Rotation.None;
				}
				break;
		} 
	}
}