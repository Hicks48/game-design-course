using UnityEngine;
using System.Collections;

public class EventAlarm : MonoBehaviour {

	public AudioClip alarmSound;
	public float startScale = 10f;
	public float endScale = 0.01f;
	public float speed = 2f;

	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		AudioSource.PlayClipAtPoint(alarmSound, new Vector3());
		Destroy(gameObject, speed);
	}
	
	// Update is called once per frame
	void Update () {
		float delta = (Time.time - startTime) / speed;
		float lerp = Mathf.Lerp(startScale, endScale, delta);
		transform.localScale = new Vector3(lerp, lerp, 1f);
	}
}
