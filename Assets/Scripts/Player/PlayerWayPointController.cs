using UnityEngine;
using System.Collections;

/*public enum Rotation {
	Left,
	Right,
	None
} */

public class PlayerWayPointController : MonoBehaviour {
	public float horizontalBorder;
	public float speed = 0.0f;
	public float rotationSpeed = 100.0f;
	public float maxWalkSpeed = 10.0f;
	public bool moving;
	public Transform[] wayPoints;

	private int currentWayPoint;
	private bool wayPointReached;
	private Rotation playerRotation;
	private Quaternion lastRotation;
	private Rigidbody rigidBody;
	private float rangeToStartChecking = 1f;
	private float dist = 0f;
	private float lastDist = Mathf.Infinity;

	// Use this for initialization
	void Start () {
		moving = false;
		currentWayPoint = 0;
		wayPointReached = false;
		rigidBody = transform.GetComponent<Rigidbody>();
		playerRotation = Rotation.None;
		lastRotation = transform.rotation;
		horizontalBorder = Screen.width / 4f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
				Debug.Log("Go for " + (currentWayPoint + 1) + " @ " + maxWalkSpeed);
				if (currentWayPoint < wayPoints.Length - 1) {
					wayPointReached = false;
					Vector3 relativePos = wayPoints[currentWayPoint + 1].position - transform.position;
					speed = maxWalkSpeed;
					rigidBody.AddForce(speed * relativePos);
				}
			} 
		}

		if (currentWayPoint < wayPoints.Length - 1) {
			dist = ( wayPoints[currentWayPoint + 1].position - transform.position ).sqrMagnitude;
		}

		if (dist < rangeToStartChecking)
		{
			if ( dist > lastDist )
			{
				currentWayPoint++;
				Debug.Log(currentWayPoint + " has been reached");
				wayPointReached = true;
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
				lastDist = Mathf.Infinity;
			}
			else
			{
				lastDist = dist;
			}
		}
		else
		{
			lastDist = Mathf.Infinity;
		}

		switch (playerRotation) {
			case Rotation.None: 
				break;
			case Rotation.Left: 
				if (Vector3.Distance(transform.eulerAngles, (lastRotation * Quaternion.Euler(0, -90, 0)).eulerAngles) > 1f) {
					transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
				}
				else  {
					lastRotation = transform.rotation;
					playerRotation = Rotation.None;
				}
				
				break;
			case Rotation.Right:
				if (Vector3.Distance(transform.eulerAngles, (lastRotation * Quaternion.Euler(0, 90, 0)).eulerAngles) > 1f) {
					transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
				}
				else  {
					lastRotation = transform.rotation;
					playerRotation = Rotation.None;
				}
				break;
		} 
	}
}