using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public GameObject[] randomEvents;
	public float[] randomEventProbabilities; //probability of startin any given event is eventProbability / sum of all eventProbabilities
	public float randomEventFrequency = 10f;

	private float nextRandomEventTime;
	private float overallRandomEventProbability;

	// Use this for initialization
	void Start () {
		nextRandomEventTime = NewRandomEventTime();
		foreach(float probability in randomEventProbabilities) {
			overallRandomEventProbability += probability;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextRandomEventTime) {
			StartRandomEvent();
			nextRandomEventTime = NewRandomEventTime();
		}
	}

	private void StartRandomEvent() {
		if(randomEvents.Length == 0) return; 
		float random = Random.Range(0f, overallRandomEventProbability);
		int eventIndex = 0;
		float probability = randomEventProbabilities[0];
		while(random > probability) {
			eventIndex++;
			probability += randomEventProbabilities[eventIndex];
		}
		Instantiate (randomEvents[eventIndex]);
		Debug.Log ("random event: " + randomEvents[eventIndex].name);
	}

	private float NewRandomEventTime() {
		return Time.time + Random.Range(0.5f * randomEventFrequency, 1.5f * randomEventFrequency);
	}
}
