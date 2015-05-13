using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public string[] availableWeapons;
	public string[] weaponData;
	private int[] buttonTracker;
	public double[] armorData;
	public int money;

	// Use this for initialization
	void Start () {
		GameObject oldData = GameObject.Find("oldPersistentData");
		if (oldData != null) {
			this.money = oldData.GetComponent<TestMenu>().money / 4;
			Destroy (oldData);
		}
		DontDestroyOnLoad(transform.gameObject);
		money = 50;
		availableWeapons = new string[] {"Projectile", "Lazer","BurstJump","Rush", "SpeedBurst", "GrenadeToss", "DeathLazer"};
		weaponData = new string[] {availableWeapons [0], availableWeapons [0], availableWeapons [0], availableWeapons [0]};
		buttonTracker = new int[] {0, 0, 0, 0};
		armorData = new double[] {1.0, 1.0, 1.0, 1.0};
	}
	
	public void startGame() {
		Application.LoadLevel(1);
	}
}
