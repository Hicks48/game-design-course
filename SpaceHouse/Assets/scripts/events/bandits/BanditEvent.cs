using UnityEngine;
using System.Collections;

public class BanditEvent : MonoBehaviour {

	public GameObject banditPrefab;
	public int levels = 1;

	private float levelDistance = 5f;
	private float baseDistance = 15f;
	private int banditBaseCount = 15;
	private int banditIncrement = 5;

	// Use this for initialization
	void Start () {
		SpawnBanditWave(levels);
	}

	private void SpawnBanditWave(int levels) {
		int bandits = banditBaseCount;
		float banditDistance = baseDistance;
		for(int level = 0; level < levels; level++) {
			for(int bandit = 0; bandit < bandits; bandit++) {
				GameObject banditInstance = (GameObject) Instantiate(banditPrefab);
				BanditController controller = banditInstance.GetComponent<BanditController>();
				controller.banditsInCurrentWave = bandits;
				controller.index = bandit;
				controller.targetDistance = banditDistance;
			}
			bandits += banditIncrement;
			banditDistance += levelDistance;
		}

	}
}
