using UnityEngine;
using System.Collections;

public class playerCamera : MonoBehaviour {
	public float DistanceFromPlayerX = 0.0f;
	public float DistanceFromPlayerY = 25.0f;
	public float DistanceFromPlayerZ = 25.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerLoc = GameObject.Find ("Player").transform.position;
		float posX = playerLoc.x - DistanceFromPlayerX;
		float posY = playerLoc.y + DistanceFromPlayerY;
		float posZ = playerLoc.z - DistanceFromPlayerZ;
		transform.position = new Vector3 (posX, posY, posZ);
		transform.LookAt (GameObject.Find ("Player").transform);
	}
}
