using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour {


	public int nextLevel;
	public string[] availableWeapons;
	public string[] weaponData;
	private int[] buttonTracker;
	public double[] armorData;
	public int money;
	public Text budget;
	public IItem activeWeapon;
	public IItem activeDefense;
	public IItem activeUtility;
	public ItemLibrary library;
	public float health;

	// Use this for initialization
	void Start () {
		this.nextLevel = 3;
		money = 0;
		GameObject oldData = GameObject.Find("oldPersistentData");
		if (oldData != null) {
			this.money += oldData.GetComponent<TestMenu>().money;
			Destroy (oldData);
		}
		GameObject returnData = GameObject.Find("returnPersistentData");
		if (returnData != null) {
			this.money += oldData.GetComponent<TestMenu>().money;
			this.nextLevel = oldData.GetComponent<TestMenu>().nextLevel;
			Destroy (returnData);
		}

		DontDestroyOnLoad(transform.gameObject);
		availableWeapons = new string[] {"Projectile", "Lazer","BurstJump","Rush", "SpeedBurst", "GrenadeToss", "DeathLazer"};
		weaponData = new string[] {availableWeapons [0], availableWeapons [0], availableWeapons [0], availableWeapons [0]};
		buttonTracker = new int[] {0, 0, 0, 0};
		armorData = new double[] {1.0, 1.0, 1.0, 1.0};
		this.activeWeapon = new NonItem ();
		this.activeDefense = new NonItem ();
		this.activeUtility = new NonItem ();
		this.library = new ItemLibrary ();
		this.activeWeapon = this.library.GetItem ("GrenadeLauncher");
		this.health = 100f;
		}
	
	public void startGame() {
		Application.LoadLevel(this.nextLevel);
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

	public void ActiveWeapon(Button button){
		if (this.library.GetItem (button.name).IsOwned())
			this.activeWeapon = this.library.GetItem (button.name);
	}

	public void ActiveDefense(Button button){
		if (this.library.GetItem (button.name).IsOwned())
			this.activeDefense = this.library.GetItem (button.name);
	}

	public void ActiveUtility(Button button){
		if (this.library.GetItem (button.name).IsOwned())
			this.activeUtility = this.library.GetItem (button.name);
	}

	public void BuyItem(Button button){
		if (!this.library.GetItem (button.name).IsOwned () && (this.money - this.library.GetItem (button.name).GetPrice () >= 0)) {
			this.library.GetItem (button.name).Buy ();
			this.money -= this.library.GetItem (button.name).GetPrice ();
		}

		if (this.library.GetItem (button.name).IsOwned ()) {
			Debug.Log("Owned");
		}
	}

	public void BuyArmor(){
		if (this.money >= 50) {
			this.armorData [0] += 0.1f;
			this.armorData [1] += 0.1f;
			this.armorData [2] += 0.1f;
			this.armorData [3] += 0.1f;
			this.money -= 50;
		}
	}

	public void BuyHealth(){
		if (this.money >= 40) {
			this.health += 20;
			this.money -= 40;
		}
	}

	public void quitGame() {
		Application.Quit ();
	}
}