using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
	public IdleState(BaseStateMachine _ctx) : base(_ctx)
	{
		stateName = "Idle State";
	}
	public override void EnterState()
	{
		Debug.Log("Entering " + stateName);
		//_context.Animator.SetFloat("Speed", 0f);
		_context.AllowAttack(true);
		_context.Animator.CrossFade("Idle", 0f, 0);
		_context.PlayerController.Move(_context.Direction);
	}

	public override void ExitState()
	{
		_context.AllowAttack(true);
		_context.AllowWalk(true);
	}

	public override void UpdateState()
	{
		CheckOverrideSwitch();
	}

	public override void CheckOverrideSwitch()
	{
	}
}
