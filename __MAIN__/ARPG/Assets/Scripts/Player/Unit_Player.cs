using UnityEngine;
using System.Collections;

public class Unit_Player : Unit {

	public void ShootRay()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit();

		//TODO: This is expensive, consider max length
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			if (hit.collider != null && hit.collider.gameObject.tag != "Floor" && hit.collider.gameObject.name != "Player") {
				_interactable = hit.collider;
			} 
			else {
				_destination = hit.point;
			}

		}
	}

	// Use this for initialization
	public override void Start () {
		base.Start ();
	}

	// Update is called once per frame
	public override void Update () {

		//mouse movement
		if (Input.GetMouseButtonDown (0)) {
			ShootRay ();
		}
		if (Input.GetMouseButtonDown (1) && _attacking == false) {
			Debug.Log ("Attack");
			GetComponent<Animator> ().Play ("Attack2h");
			_attacking = true;
		}

		//attack finished
		if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CombatReady")) {
			Debug.Log ("Attack Complete");
			_attacking = false;
		}

		base.Update ();

		if (_looting && GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Loot") == false) {
			GetComponent<Animator> ().Play ("Loot");
		}
		// end loot cycle
		if (_looting && GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Stand")) {
			_looting = false;
		}

		// return to idle
		if (!_attacking && !_looting) {
			if (move != Vector3.zero) {
				GetComponent<Animator> ().Play ("Walk");
			} else {
				GetComponent<Animator> ().Play ("Stand");	
			}
		}
	}
}
