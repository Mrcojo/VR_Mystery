using UnityEngine;
using System.Collections;

public class PlayerWayPointController : MonoBehaviour {
	public float horizontalBorder;
	public float speed = 0.0f;
	public float rotationSpeed = 100.0f;
	public float maxWalkSpeed = 10.0f;
	public bool moving;

	public Interaction interaction;
	public Material interactiveMaterial;

	public AudioClip footstepSound;
	public AudioClip interactionSound;

	private Transform facingWayPoint;
	private bool wayPointReached;
	private Quaternion lastRotation;
	private Rigidbody rigidBody;
	private float rangeToStartChecking = 1f;
	private float dist = 0f;
	private float lastDist = Mathf.Infinity;
	
	// Use this for initialization
	void Start () {
		moving = false;
		wayPointReached = false;
		rigidBody = transform.GetComponent<Rigidbody>();
		lastRotation = transform.rotation;
		horizontalBorder = Screen.width / 4f;
	}
	
	// Update is called once per frame
	float fr = 0f;
	float footsFrequency = 0.7f;
	void Update ()
	{
		if( rigidbody.velocity.magnitude > 0f)
		{
			fr += Time.deltaTime;

			while( fr >= footsFrequency )
			{
				fr = 0f;
				
				audio.PlayOneShot( footstepSound );
			}
		}

		if (interaction.hit.transform.tag == "Interactive") {
			if ((interaction.hit.transform.position - transform.position ).sqrMagnitude < 5) {
				interaction.hit.transform.renderer.material = interactiveMaterial;
			}
		}
	}

	void FixedUpdate () {
		if (Input.GetMouseButton(0))
		{
			transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
		} 
		
		if (Input.GetMouseButton(1))
		{
			transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
		} 
		
		if (Input.GetMouseButtonUp(2))
		{
			if (interaction.hit.transform.tag == "WayPoint" && !moving/* && !interaction.hit.transform.name.Contains("trigger")*/) {
				moving = true;
				wayPointReached = false;
				facingWayPoint = interaction.hit.transform;
				Vector3 relativePos = facingWayPoint.position - transform.position;
				speed = maxWalkSpeed;
				rigidBody.AddForce(speed * relativePos);
			}
			else if (interaction.hit.transform.tag == "Interactive") {
				if ((interaction.hit.transform.position - transform.position ).sqrMagnitude < 5) {
					audio.PlayOneShot( interactionSound);
					GameObject.Destroy(interaction.hit.transform.gameObject);
				}
			}
		}				

		if (moving) {
			dist = (facingWayPoint.position - transform.position ).sqrMagnitude;
		}
		
		if (dist < rangeToStartChecking)
		{
			if ( dist > lastDist )
			{
				moving = false;
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
		
	}
}