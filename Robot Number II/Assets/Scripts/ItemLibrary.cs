using UnityEngine;
using System.Collections;

public class ItemLibrary {

	private Hashtable items;

	public ItemLibrary(){
		this.items = new Hashtable ();

		this.items.Add ("LazerCannon", new LazerCannon ());
		this.items.Add ("GrenadeLauncher", new GrenadeLauncher ());
		this.items.Add ("ResistanceEnhancement", new ResistanceEnhancement ());
		this.items.Add ("MedicalKit", new MedicalKit ());
		this.items.Add ("JumpPack", new JumpPack ());
		this.items.Add ("RushBoots", new RushBoots ());
		this.items.Add ("SpeedBoots", new SpeedBoots ());
	}

	public IItem GetItem(string item){
		return (IItem)this.items [item];
	}
}
