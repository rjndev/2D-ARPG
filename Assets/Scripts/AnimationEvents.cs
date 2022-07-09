using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationEvents : MonoBehaviour
{
	// Start is called before the first frame update

	//Events to send out
	public event Action AnimationEndEvent;
	public event Action BasicAttackLeapEvent;
	public event Action ActivateAttackCollidersEvent;
	public event Action AttackEndEvent;
	public event Action HitStaggerEvent;
	public event Action AttackLeapPastEvent;

	public event Action ActivateCollisionCollidersEvent;

	public event Action<bool> TriggerAttackColliderTransferEvent;

	public event Action AttackDamageEvent;

	public event Action TriggerCanComboEvent;

	public void AttackDamage()
	{
		AttackDamageEvent?.Invoke();
	}

	public void TriggerCanCombo()
	{
		TriggerCanComboEvent?.Invoke();
	}

	public void TriggerAttColliderTransfer(int transferBehind)
	{
		if (transferBehind == 1)
			TriggerAttackColliderTransferEvent?.Invoke(true);
		else if (transferBehind == 0)
			TriggerAttackColliderTransferEvent?.Invoke(false);
	}

	public void TriggerAttackColliderTransfers(bool transferBehind)
	{
		TriggerAttackColliderTransferEvent?.Invoke(transferBehind);
	}

	public void ActivateCollisionColliders()
	{
		ActivateAttackCollidersEvent?.Invoke();
	}
	public void AttackLeapPast()
	{
		AttackLeapPastEvent?.Invoke();
	}

	public void HitStagger()
	{
		HitStaggerEvent?.Invoke();
	}
	public void AnimationEnd()
	{
		Debug.Log("ANIM END");
		AnimationEndEvent?.Invoke();
	}

	public void BasicAttackLeap()
	{
		//Leap Forward event when doing basic attack
		BasicAttackLeapEvent?.Invoke();
	}

	public void ActivateAttackColliders()
	{
		ActivateAttackCollidersEvent?.Invoke();
	}

	public void AttackEnd()
	{
		Debug.Log("ATTACK END");
		AttackEndEvent?.Invoke();
	}

}
