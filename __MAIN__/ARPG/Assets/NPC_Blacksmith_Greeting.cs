using UnityEngine;
using System.Collections;

public class NPC_Blacksmith_Greeting : MonoBehaviour {

    private Animator animator = null;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Player")
        {
            if (animator != null)
            {
                animator.SetTrigger("Greetings");
            }
        }
    }
}
