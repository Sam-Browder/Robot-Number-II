using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour {

	public string testMessage;
	public string[] availableWeapons;
	public string[] weaponData;
	private int[] buttonTracker;
	public double[] armorData;
	public int money;
	public Text budget;

	// Use this for initialization
	void Start () {
		GameObject oldData = GameObject.Find("oldPersistentData");
		if (oldData != null) {
			this.money = oldData.GetComponent<TestMenu>().money / 4;
			Destroy (oldData);
		}
		DontDestroyOnLoad(transform.gameObject);
		money = 50;
		testMessage = "Try and find me";
		availableWeapons = new string[] {"Projectile", "Lazer","BurstJump","Rush", "SpeedBurst", "GrenadeToss", "DeathLazer"};
		weaponData = new string[] {availableWeapons [0], availableWeapons [0], availableWeapons [0], availableWeapons [0]};
		buttonTracker = new int[] {0, 0, 0, 0};
		armorData = new double[] {1.0, 1.0, 1.0, 1.0};
	}
	
	public void startGame() {
		Application.LoadLevel(1);
	}

	public void pressedWeaponButton(Button button) {
		int buttonID = int.Parse (button.name);
		buttonTracker [int.Parse (button.name)]++;
		string newText = availableWeapons [buttonTracker [buttonID] % availableWeapons.Length];
		weaponData [buttonID] = newText;
		button.GetComponentInChildren<Text> ().text = newText;
	}

	public void pressedUpArmor(Text armorText) {
		double budgetValue = double.Parse (this.budget.text) + 0.1;
		this.budget.text = budgetValue.ToString();
		double armorValue = double.Parse (armorText.text);
		armorValue += 0.1;
		int armorID = int.Parse (armorText.name) % 4;
		armorData [armorID] = armorValue;
		armorText.text = armorValue.ToString ();
	}

	public void pressedDownArmor(Text armorText) {
		double budgetValue = double.Parse (this.budget.text);
		double armorValue = double.Parse (armorText.text);
		if (budgetValue > 0 && armorValue > 0.2) {
			int armorID = int.Parse (armorText.name) % 4;
			armorValue -= 0.1;
			armorText.text = armorValue.ToString ();
			armorData [armorID] = armorValue;
			budgetValue -= 0.1;
			this.budget.text = budgetValue.ToString();
		}
	}
}