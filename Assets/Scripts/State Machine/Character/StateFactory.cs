using UnityEngine;

public class StateFactory
{
	private BaseStateMachine _context;
	public StateFactory(BaseStateMachine context)
	{
		this._context = context;
	}

	public State Idle()
	{
		return new IdleState(_context);
	}

	public State Walk()
	{
		return new WalkState(_context);
	}

	public State Attack()
	{
		return new AttackState(_context);
	}

	public State Damaged()
	{
		return new DamagedState(_context);
	}

	public State Run()
    {
		return new RunState(_context);
    }
}
