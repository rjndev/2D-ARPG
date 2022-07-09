using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedState : State
{
	public DamagedState(BaseStateMachine ctx) : base(ctx)
	{
		stateName = "Damaged State";
	}
	public override void EnterState()
	{
		Debug.Log("Entered " + stateName);
		_context.AllowWalk(false);
		_animator.CrossFade("BasicDamaged", 0.3f, 0);
        
		//play hit particle effect
		_context.Hit.Play(true);
    }

    public override void UpdateState()
	{
	}

	public override void CheckOverrideSwitch()
	{
	}

	public override void ExitState()
	{
		//Can Walk when damaged anim finished
		_context.AllowWalk(true);
	}
}
