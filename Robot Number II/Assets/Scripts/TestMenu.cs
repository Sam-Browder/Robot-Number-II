using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour {

	public string testMessage;
	public string[] availableWeapons;
	public string[] weaponData;
	private int[] buttonTracker;
	public double[] armorData;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		testMessage = "Try and find me";
		availableWeapons = new string[] {"Projectile", "Lazer","JetPack","Rush"};
		weaponData = new string[] {availableWeapons [0], availableWeapons [1], availableWeapons [2], availableWeapons [3]};
		buttonTracker = new int[] {0, 0, 0, 0};
		armorData = new double[] {1.0, 1.0, 1.0, 1.0};
	}
	
	public void startGame() {
		Application.LoadLevel ("StartingLevel");
	}

	public void pressedWeaponButton(Button button) {
		int buttonID = int.Parse (button.name);
		buttonTracker [int.Parse (button.name)]++;
		string newText = availableWeapons [buttonTracker [buttonID] % availableWeapons.Length];
		weaponData [buttonID] = newText;
		button.GetComponentInChildren<Text> ().text = newText;
	}

	public void pressedUpArmor(Text armorText) {
		double armorValue = double.Parse (armorText.text);
		armorValue += 0.1;
		armorText.text = armorValue.ToString ();
	}

	public void pressedDownArmor(Text armorText) {
		double armorValue = double.Parse (armorText.text);
		int armorID = int.Parse (armorText.name) % 4;
		armorValue -= 0.1;
		armorText.text = armorValue.ToString ();
		armorData [armorID] = armorValue;
	}
}