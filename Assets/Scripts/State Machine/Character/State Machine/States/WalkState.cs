using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
	private PlayerController _playerController;
	public WalkState(BaseStateMachine _ctx) : base(_ctx)
	{
		stateName = "Walk State";
		_playerController = _context.PlayerController;
	}
	public override void EnterState()
	{
		Debug.Log("Entering " + stateName);
		//_context.Animator.SetFloat("Speed", 5f);
		_context.Animator.CrossFade("Walk", 0.2f, 0);
		_context.PlayerController.Move(_context.Direction);
	}

	public override void ExitState()
	{
	}

	public override void UpdateState()
	{
		if (_playerController.RunToggle)
			_context.SwitchState(_context.Factory.Run());
	}

	public override void CheckOverrideSwitch()
	{

	}
}
