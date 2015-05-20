using UnityEngine;
using System.Collections;

public class ItemLibrary {

	private Hashtable items;

	public ItemLibrary(){
		this.items = new Hashtable ();

		GrenadeLauncher gl = new GrenadeLauncher ();
		gl.Buy ();

		this.items.Add ("LazerCannon", new LazerCannon ());
		this.items.Add ("GrenadeLauncher", gl);
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
