// Note: This file is for demonstration purposes only. This pack is built to provide you with models, not scripts.
// While you may feel free to use this file alongside these models, please understand that it is not what this package
// is all about. Thank you.

using UnityEngine;
using System.Collections;

public class ExplodeBarrel : MonoBehaviour 
{
	public float explosionForce = 10f;
	public float explosionRadius = 10f;
	public float upForceMin = 0.0f;
	public float upForceMax = 1f;

	public bool autoDestroy = true;
	public float lifeTime = 5.0f;

	void Start () 
	{
	    if (autoDestroy)
			Destroy(gameObject, lifeTime);
	}

	public void Explode()
	{
		Vector3 centerPos = transform.position;

		foreach (Transform child in transform) 
		{
			Rigidbody rb = child.gameObject.GetComponent<Rigidbody> ();
			rb.AddExplosionForce (explosionForce, centerPos, explosionRadius, Random.Range(upForceMin, upForceMax), ForceMode.Impulse);
		}
	}
}
