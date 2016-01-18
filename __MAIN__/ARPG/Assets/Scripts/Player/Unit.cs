using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    protected Animator animator;
    protected CharacterController controller;
	protected Vector3 move = Vector3.zero;
	protected Collider _interactable = null;
	protected Vector3 _destination = Vector3.zero;

	public const float MIN_DIST = 1.2f;

	public bool _stopSeeking = false;

	public float moveSpeed = 1.0f;

    public bool looting = false;
    public bool attacking = false;
    public bool combat = false;
    public float speed = 0.0f;

    // Use this for initialization
    public virtual void Start ()
    {
        animator = GetComponent<Animator>();
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
		if (dist > MIN_DIST && !_stopSeeking) {
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
            speed = moveSpeed;

        } 
		// Made it to the collider, now let's try to do some stuff
		else {
			if(_interactable && _interactable.gameObject.tag == "Lootable")
            {
                animator.SetTrigger("Looting");
                looting = true;
				_stopSeeking = false;
				_interactable = null;
			}
			_destination = transform.position;
			move = Vector3.zero;
            speed = 0;
		} 
	}

	public void OnTriggerEnter(Collider col)
	{
		if (col.gameObject == _interactable.gameObject) {
			_stopSeeking = true;
		}
	}
}
