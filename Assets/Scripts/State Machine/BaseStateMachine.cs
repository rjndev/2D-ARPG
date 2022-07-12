using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BaseStateMachine : MonoBehaviour
{
	protected State _currentState;
	protected StateFactory _factory;
	public StateFactory Factory { get { return _factory; } }
	[SerializeField]
	protected Animator _animator;
	public Animator Animator { get { return _animator; } }

	protected Character _character;
	public Character CharacterGetter { get { return _character; } }

	protected PlayerController _playerController;
	public PlayerController PlayerController { get { return _playerController; } }

	protected Vector2 _direction;
	public Vector2 Direction { get { return _direction; } }

	protected Rigidbody2D _rb2d;
	public Rigidbody2D Rb2d { get { return _rb2d; } }

	protected ComboSystem _comboSystem;
	public ComboSystem ComboSys { get { return _comboSystem; } }

	protected Vector2 _damageDir;
	public Vector2 DamageDir { get { return _damageDir; } }

	[SerializeField]
	protected ParticleSystem _hit;
	public ParticleSystem Hit { get { return _hit; } }




	//DEBUG PURPOSES 
	[SerializeField]
	protected Text _debugText;
	public Text DebugText { get { return _debugText; } }

    void Start()
    {
        
    }

    public virtual void SwitchState(State newState)
	{
		_currentState.ExitState();
		_currentState = newState;
		_currentState.EnterState();
	}
	protected void HandleWalk(InputAction.CallbackContext context)
	{
		_direction = context.ReadValue<Vector2>();

		if (_playerController.RunToggle)
			SwitchState(_factory.Run());
		else
			SwitchState(_factory.Walk());

	}

	public void HandleGetDamaged(Vector2 attackDir)
	{
		_damageDir = attackDir;
		SwitchState(_factory.Damaged());
	}

	protected void HandleAttack(InputAction.CallbackContext context)
	{
		SwitchState(_factory.Attack());
	}

	protected void HandleIdle(InputAction.CallbackContext context)
	{
		_direction = new Vector2(0, 0);
		SwitchState(_factory.Idle());
	}

	protected void HandleRunToggle(InputAction.CallbackContext context)
    {
		_playerController.ToggleRun(!_playerController.RunToggle);
    }

	protected void UpdateState()
	{
		CheckOverrideSwitch();
		_currentState?.UpdateState();
	}

	void Update()
	{
		UpdateState();
	}

	private void CheckOverrideSwitch()
	{

	}

	public virtual void AllowWalk(bool allow) { }

	public virtual void AllowAttack(bool allow) { }




}
