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
	public IItem activeWeapon;
	public IItem activeDefense;
	public IItem activeUtility;
	public ItemLibrary library;

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
		this.activeWeapon = new NonItem ();
		this.activeDefense = new NonItem ();
		this.activeUtility = new NonItem ();
		this.library = new ItemLibrary ();
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

	public void ActiveWeapon(Button button){
		if (this.library.GetItem (button.name).IsOwned())
			this.activeWeapon = this.library.GetItem (button.name);
		/*switch (button.name) {
		case "LazerCannon":
			this.activeWeapon = new LazerCannon ();
			break;
		case "GrenadeLauncher":
			this.activeWeapon = new GrenadeLauncher ();
			break;
		default:
			this.activeWeapon = new LazerCannon ();
			break;
		}*/
	}

	public void ActiveDefense(Button button){
		if (this.library.GetItem (button.name).IsOwned())
			this.activeDefense = this.library.GetItem (button.name);
		/*switch (button.name) {
		case "ResistanceEnhancement":
			this.activeDefense = new ResistanceEnhancement ();
			break;
		case "MedicalKit":
			this.activeDefense = new MedicalKit ();
			break;
		default:
			this.activeDefense = new ResistanceEnhancement ();
			break;
		}*/
	}

	public void ActiveUtility(Button button){
		if (this.library.GetItem (button.name).IsOwned())
			this.activeUtility = this.library.GetItem (button.name);
		/*switch (button.name) {
		case "JumpPack":
			this.activeUtility = new JumpPack ();
			break;
		case "SpeedBoots": 
			this.activeUtility = new SpeedBoots ();
			break;
		case "RushBoots":
			this.activeUtility = new RushBoots ();
			break;
		default:
			this.activeUtility = new JumpPack ();
			break;
		}*/
	}

	public void BuyItem(Button button){
		if (!this.library.GetItem (button.name).IsOwned () && (this.money - this.library.GetItem (button.name).GetPrice () >= 0)) {
			this.library.GetItem (button.name).Buy ();
			this.money -= this.library.GetItem (button.name).GetPrice ();
		}

	}
}