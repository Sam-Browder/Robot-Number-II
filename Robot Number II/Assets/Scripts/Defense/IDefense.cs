using UnityEngine;
using System.Collections;

public interface IDefense{
	
	string GetDefense();
	void ExecuteDefense(IDefenseBehavior defense);
	
}
