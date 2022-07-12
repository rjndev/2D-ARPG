using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem
{

	//TODO Handle Leap Forward Basic Attack

	//This class handles the combo system of the character
	//Please Instantiate on Start()
	//this represents current combo 1-3
	private int _currentComboIndex;
	private AnimationEvents _animationEvents;
	private BaseStateMachine _stateMachine;
	private Animator _animator;
	private Rigidbody2D _rb2d;
	private PlayerController _playerController;
	private AttackCollisionsHandler _attackCollisionsHandler;
	private BoxCollider2D _attackTrigger;
	private BoxCollider2D _collisionCollider;


	private string[] _combos;
	private const int MAX_COMBOS = 3;
	private bool _canCombo;
	private bool _firstAttack;
	private bool _attackEnd;
	public bool AttackEnd { get { return _attackEnd; } }


	public ComboSystem(AnimationEvents e, BaseStateMachine s)
	{
		_combos = new string[MAX_COMBOS];
		_canCombo = false;
		_attackEnd = true;
		_firstAttack = true;
		_currentComboIndex = 0;
		_animationEvents = e;
		_stateMachine = s;
		_animator = _stateMachine.Animator;
		_rb2d = _stateMachine.Rb2d;
		_playerController = _stateMachine.PlayerController;

		_attackCollisionsHandler = _stateMachine.GetComponentInChildren<AttackCollisionsHandler>();
		_attackTrigger = _attackCollisionsHandler.gameObject.GetComponent<BoxCollider2D>();
		_collisionCollider = _stateMachine.GetComponent<BoxCollider2D>();

		//TODO transfer some of these events to other classes
		_animationEvents.BasicAttackLeapEvent += HandleBAttackLeap;
		_animationEvents.AnimationEndEvent += HandleClipEnd;
		_animationEvents.ActivateAttackCollidersEvent += HandleActivateAttackColliders;
		_animationEvents.AttackEndEvent += HandleAttackEnd;
		_animationEvents.HitStaggerEvent += HandleHitStagger;
		_animationEvents.AttackLeapPastEvent += HandleAttackLeapPast;
		_animationEvents.ActivateAttackCollidersEvent += HandleActivateCollisionColliders;
		_animationEvents.TriggerAttackColliderTransferEvent += HandleTransferAttackColliders;
		_animationEvents.TriggerCanComboEvent += HandleTriggerCanCombo;
		_animationEvents.AttackDamageEvent += HandleAttackDamage;
	}


	//When user clicks left click or right click attack
	public void Attack()
	{

		if (_firstAttack)
		{
			
			TriggerAttack();
			_firstAttack = false;
			_canCombo = false;
		}
		else
		{
			if (_canCombo)
			{				
				TriggerAttack();
				_canCombo = false;
			}
			else
			{
			}
		}
	}


	private void TriggerAttack()
	{
		//AttackEnd is used for AttackCollisionsHandler to check if current 
		//targets are damaged for the attack and can be removed from the Target List
		//through OnTriggerExit2D
		_attackEnd = false;

		_currentComboIndex += 1;

		if (_currentComboIndex > MAX_COMBOS)
		{
			_currentComboIndex = 1;
		}
		
		//_animator.SetTrigger("Attack" + (_currentComboIndex));
		_animator.CrossFade("BasicAttack" + _currentComboIndex, 0, 0 );


		// if (_currentComboIndex >= MAX_COMBOS)
		// 	_currentComboIndex = 0;
		// else
		// 	_currentComboIndex += 1;
	}

	public void HandleAttackDamage()
	{
		_attackCollisionsHandler.CollidedChars.ForEach(entity =>
		{
			entity.GetComponent<BaseStateMachine>().HandleGetDamaged(new Vector2(1, 0) * _stateMachine.transform.localScale.x);
		});
	}

	public void HandleTriggerCanCombo()
	{
		_stateMachine.AllowAttack(true);
		_canCombo = true;
	}

	public void HandleTransferAttackColliders(bool transferBehind)
	{
		if (!transferBehind)
		{
			_attackTrigger.offset = new Vector2(1.4f, -0.36f);
			_attackTrigger.size = new Vector2(1.27f, 1f);
		}
		else
		{
			_attackTrigger.offset = new Vector2(-1.67f, -0.36f);
			_attackTrigger.size = new Vector2(4.5f, 1f);
		}

	}
	public void HandleAttackLeapPast()
	{
		_playerController.LeapPast();
	}

	public void HandleActivateCollisionColliders()
	{
		_collisionCollider.enabled = true;
	}

	public void HandleHitStagger()
	{
		_playerController.HitStagger(_stateMachine.DamageDir);
	}


	public void HandleClipEnd()
	{
		//if it reached end of an attack animation reset back to Idle
		//and reset counter to 0
		_currentComboIndex = 0;
		_firstAttack = true;
		_canCombo = false;
		_attackEnd = true;
		_stateMachine.SwitchState(_stateMachine.Factory.Idle());
	}

	public void HandleBAttackLeap()
	{
		//Attack Leap Event Handler for basic attack
		//Maybe move this to other class later on
		_playerController.Leap();
	}

	public void HandleActivateAttackColliders()
	{
		//Activate Attack Colliders
		_attackTrigger.enabled = true;
	}

	public void HandleAttackEnd()
	{
		_currentComboIndex = 0;
		_firstAttack = true;
		_canCombo = false;
		_stateMachine.AllowAttack(true);
	}
}
