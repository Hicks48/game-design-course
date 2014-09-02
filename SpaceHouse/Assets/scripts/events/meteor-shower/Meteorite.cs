﻿using UnityEngine;
using System.Collections;

public class Meteorite : MonoBehaviour {

	public GameObject hitPrefab;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnRayMovementHit(RaycastHit2D hit) {
		if(hit.collider != null && hit.transform.tag == "Space Station") Instantiate(hitPrefab, hit.point, Quaternion.identity);
	}
}
