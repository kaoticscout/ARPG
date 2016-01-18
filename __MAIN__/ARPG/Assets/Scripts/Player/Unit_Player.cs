using UnityEngine;
using System.Collections;

public class Unit_Player : Unit {

    int lootHash = Animator.StringToHash("Base Layer.Loot");
    int attackHash = Animator.StringToHash("Base Layer.Attack2h");

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
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.fullPathHash == lootHash || stateInfo.fullPathHash == attackHash)
        {
            _stopSeeking = true;
        }
        else
        {
            animator.ResetTrigger("Attacking");
            animator.ResetTrigger("Looting");
            _stopSeeking = false;
        }

        if (Input.GetMouseButtonDown (0) && stateInfo.fullPathHash != attackHash) {
			ShootRay ();
		}

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.A) && stateInfo.fullPathHash != attackHash)
        {
            animator.SetTrigger("Attacking");
            _stopSeeking = true;
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            combat = !combat;
        }
        if(Input.GetKeyDown(KeyCode.L) && stateInfo.fullPathHash != lootHash)
        {
            animator.SetTrigger("Looting");
            _stopSeeking = true;
        }
        
        animator.SetBool("Combat", combat);
        animator.SetFloat("Speed", speed);

        base.Update ();
	}
}
