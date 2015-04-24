using UnityEngine;
using System.Collections;

public interface IAttackBehavior{

	void doAttack();
	void swapAttack();
	void setPrimaryAttack(IAttack attack);
	void setSecondaryAttack(IAttack attack);

}
