using UnityEngine;
using System.Collections;

public class GravityDirection : MonoBehaviour {

	public int force;
	public int diviseur;
	private ConstantForce cf;

	// Use this for initialization
	void Start () {
		force = 0;
		diviseur = 1;
		cf = this.GetComponent<ConstantForce>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		cf.force = new Vector3( 0 ,  0, 0);
	}
	public void ReverseGravity()
	{
	}
}
