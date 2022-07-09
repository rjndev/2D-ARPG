using UnityEngine;
public abstract class State
{
	protected BaseStateMachine _context;
	protected Animator _animator;
	protected string stateName = "Default State";
	public string StateName { get { return stateName; } }

	public State(BaseStateMachine _ctx)
	{
		_context = _ctx;
		_animator = _context.Animator;
	}
	public abstract void EnterState();
	public abstract void ExitState();
	public abstract void UpdateState();
	//this checks if there are State switching forced by outside forces
	//e.g. getting hit, falling etc.
	public abstract void CheckOverrideSwitch();
}
