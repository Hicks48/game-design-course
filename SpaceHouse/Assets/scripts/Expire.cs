using UnityEngine;
using System.Collections;

public class Expire : MonoBehaviour {

	public float time = 10f;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, time);
	}
}
