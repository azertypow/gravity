using UnityEngine;
using System.Collections;

public class persoController : MonoBehaviour {

	public float force;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		if( Input.GetKeyDown ("left") ){
			Vector3 addMoovment = new Vector3 (0, 0, -force);
			rb.AddForce(addMoovment);
		}

		if( Input.GetKeyDown ("right") ){
			Vector3 addMoovment = new Vector3 (0, 0, force);
			rb.AddForce(addMoovment);
		}

		if( Input.GetKeyDown ("up") ){
			Vector3 addMoovment = new Vector3 (-force, 0, 0);
			rb.AddForce(addMoovment);
		}

		if( Input.GetKeyDown ("down") ){
			Vector3 addMoovment = new Vector3 (force, 0, 0);
			rb.AddForce(addMoovment);
		}
	}
}
