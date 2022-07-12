using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
	private ComboSystem _comboSystem;
	public AttackState(BaseStateMachine ctx) : base(ctx)
	{
		stateName = "Attack State";
	}
	public override void EnterState()
	{
		Debug.Log("Entering " + stateName);
		//Disable Movement controls while attacking for now
		_context.AllowWalk(false);
		_context.AllowAttack(false);
		_comboSystem = _context.ComboSys;
		_comboSystem.Attack();
	}

	public override void ExitState()
	{
		//Enable back on when finished combo
		Debug.Log("EXITING ATTACK STATE");
		_context.AllowWalk(true);
		_context.AllowAttack(true);

	}

	public override void UpdateState()
	{
	}

	public override void CheckOverrideSwitch()
	{

	}
}
