using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO later make a base state machine and use this as a specific SM for character
public class PlayerStateMachine : BaseStateMachine
{


	private PlayerInput _playerInput;
	private InputAction _inputWalk;
	public InputAction InputWalk { get { return _inputWalk; } }
	private InputAction _inputBasicAttack;
	private InputAction _inputRunToggle;




	//Combosystem transfer to Combat class later on

	void Awake()
	{
		_playerController = GetComponent<PlayerController>();

		//Player Input Setup
		_playerInput = GetComponent<PlayerInput>();
		_inputWalk = _playerInput.actions["Walk"];
		_inputBasicAttack = _playerInput.actions["Basic_Attack"];
		_inputRunToggle = _playerInput.actions["RunToggle"];
		_factory = new StateFactory(this);

		//init states to idle default
		_currentState = _factory.Idle();
		_currentState.EnterState();
	}
	void Start()
	{
		_rb2d = GetComponent<Rigidbody2D>();
		_inputWalk.performed += HandleWalk;
		_inputBasicAttack.performed += HandleAttack;
		_inputWalk.canceled += HandleIdle;
		_inputRunToggle.performed += HandleRunToggle;


		_comboSystem = new ComboSystem(transform.gameObject.GetComponentInChildren<AnimationEvents>(), this);
	}

    // Update is called once per frame

    public override void SwitchState(State newState)
    {
        base.SwitchState(newState);
		_debugText.text = _currentState.StateName;
	}

    void OnEnable()
	{

	}

	void OnDisable()
	{
		_inputWalk.performed -= HandleWalk;
		_inputBasicAttack.performed -= HandleAttack;
	}

	public override void AllowWalk(bool allow)
	{
		if (allow)
			_inputWalk.Enable();
		else
			_inputWalk.Disable();
	}

	public override void AllowAttack(bool allow)
	{
		if (allow)
		{
			_inputBasicAttack.Enable();
		}
		else
		{
			_inputBasicAttack.Disable();
		}
	}


}
