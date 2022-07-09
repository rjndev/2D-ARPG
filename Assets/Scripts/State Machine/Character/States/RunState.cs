using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    private PlayerController _playerController;
    // Start is called before the first frame update
    public RunState(BaseStateMachine _ctx) : base(_ctx)
    {
        stateName = "Run State";
        _playerController = _context.PlayerController;
    }

    public override void EnterState()
    {
        _context.Animator.CrossFade("Run", 0.2f, 0);
        _context.PlayerController.Move(_context.Direction);
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if (!_playerController.RunToggle)
            _context.SwitchState(_context.Factory.Walk());
    }
    public override void CheckOverrideSwitch()
    {
    }
}
