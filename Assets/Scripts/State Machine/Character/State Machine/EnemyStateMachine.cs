using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : BaseStateMachine
{
	// Start is called before the first frame update

	void Awake()
	{
		_playerController = GetComponent<PlayerController>();
		_factory = new StateFactory(this);

		_currentState = _factory.Idle();
		_currentState.EnterState();
	}
	void Start()
	{
		_rb2d = GetComponent<Rigidbody2D>();
		_comboSystem = new ComboSystem(transform.gameObject.GetComponentInChildren<AnimationEvents>(), this);
		_character = GetComponent<Character>();
	}

	public override void AllowWalk(bool allow)
	{

	}

}
