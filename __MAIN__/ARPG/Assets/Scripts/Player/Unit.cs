using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour 
{
	protected CharacterController controller;
	protected Vector3 move = Vector3.zero;
	protected Collider _interactable = null;
	protected Vector3 _destination = Vector3.zero;

	protected bool _attacking = false;
	protected bool _looting = false;

	public const float MIN_DIST = 0.5f;

	private bool _stopSeeking = false;

	public float moveSpeed = 1.0f;

	// Use this for initialization
	public virtual void Start () {
		controller = this.GetComponent<CharacterController> ();
		if (!controller) {
			Debug.LogError ("Unit.Start() " + name + " has no CharacterController!");
			enabled = false;
		}
		_destination = transform.position;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
		//attempt to travel to interactable or destination
		Vector3 targetPos = transform.position;
		if (_interactable) {
			targetPos = _interactable.transform.position;
		} 
		else {
			targetPos = _destination;
		}

		float dist = (new Vector3(targetPos.x, 0, targetPos.z) - new Vector3(transform.position.x, 0, transform.position.z)).magnitude;
		if (dist > MIN_DIST && !_attacking && !_stopSeeking) {
			//rotate
			Vector3 direction = targetPos - transform.position;
			direction.y = 0;
			Quaternion toRotation = Quaternion.LookRotation (direction);
			transform.rotation = toRotation;

			//move
			move = targetPos - transform.position;
			move.y = 0;
			move.Normalize ();
			controller.SimpleMove (move * moveSpeed);
		} 
		else if (_attacking) {
		} 
		// Made it to the collider, now let's try to do some stuff
		else {
			if(_interactable && _interactable.gameObject.tag == "Lootable")
			{
				_looting = true;
				_stopSeeking = false;
				_interactable = null;
			}
			_destination = transform.position;
			move = Vector3.zero;
		} 
	}

	public void OnTriggerEnter(Collider col)
	{
		if (col.gameObject == _interactable.gameObject) {
			_stopSeeking = true;
		}
	}
}
