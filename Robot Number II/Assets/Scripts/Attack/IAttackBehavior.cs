using UnityEngine;
using System.Collections;

public interface IAttackBehavior{

	void DoAttack();
	void SwapAttack();
	void SetPrimaryAttack(IAttack attack);
	void SetSecondaryAttack(IAttack attack);

}
