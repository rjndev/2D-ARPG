using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

	private Rigidbody2D _rb2D;
	[SerializeField]
	private float _speed = 2f;
	public float Speed { get { return _speed; } set { _speed = value; }  }

	private bool _lookRight;
	private Vector2 _moveDir;
	private bool _leap;
	private bool _hitStagger;
	private Vector2 _hitDir;
	private Vector2 _leapDistance;
	private float _walkSpeed = 2f;
	private float _runSpeed = 5f;

	private bool _toggleRun;
	public bool RunToggle { get { return _toggleRun; } }
	
	private bool _leapPast;
	[SerializeField]
	private float _leapPastSpeed = 120f;

	[SerializeField]
	private float _leapSpeed = 10f;

	private Vector2 _normalDir = new Vector2(1, 0);

	private BoxCollider2D _collisionCollider;

	// Start is called before the first frame update
	void Awake()
	{
		_toggleRun = true;
		_lookRight = true;
		_moveDir = new Vector2(0, 0);
		_hitDir = new Vector2(0, 0);
		_rb2D = GetComponent<Rigidbody2D>();
		_collisionCollider = GetComponent<BoxCollider2D>();
		_leap = false;
		_hitStagger = false;
		_leapPast = false;

		_leapDistance = new Vector2(5, 0);
	}

	void Start()
	{
	}

	void OnEnable()
	{

	}

	void OnDisable()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		if (_moveDir != Vector2.zero)
		{
			FlipDirection();


			_rb2D.MovePosition(_rb2D.position + _moveDir * _speed * Time.deltaTime);

		}
		else if (_leap)
		{
			_leap = false;
			_rb2D.MovePosition((_rb2D.position + (_normalDir * transform.localScale.x) * _leapSpeed * Time.deltaTime));
		}

		else if (_leapPast)
		{
			_leapPast = false;
			_collisionCollider.enabled = false;
			//_rb2D.MovePosition((_rb2D.position + (_normalDir * transform.localScale.x) * _leapPastSpeed * Time.deltaTime));
			StopAllCoroutines();
			StartCoroutine(LeapSmooth());
		}

		if (_hitStagger)
		{
			Debug.Log("HIT STAGGER /n lookright" + _lookRight + "/n hitDir " + _hitDir.x);
			_hitStagger = false;

			if (_lookRight && _hitDir.x > 0)
			{
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				_lookRight = false;
			}
			else if (!_lookRight && _hitDir.x < 0)
			{
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				_lookRight = true;
			}

			_rb2D.MovePosition((_rb2D.position + (_hitDir) * _leapSpeed * Time.deltaTime));

		}
	}

	public void ToggleRun(bool toggle)
    {
		_toggleRun = toggle;
		if (_toggleRun)
			_speed = _runSpeed;
		else
			_speed = _walkSpeed;
    }
	private IEnumerator LeapSmooth()
	{
		float originalX = transform.position.x;
		float targetX = originalX + (_leapDistance.x * transform.localScale.x);

		Debug.Log("ORIGX TARG X " + originalX + " " + targetX);
		float currentX = originalX;
		if (transform.localScale.x > 0)
		{
			while (currentX < targetX)
			{
				_rb2D.MovePosition((_rb2D.position + (_normalDir * transform.localScale.x) * _leapPastSpeed * Time.deltaTime));
				currentX = _rb2D.position.x;
				yield return null;
			}
		}
		else
		{
			while (currentX > targetX)
			{
				_rb2D.MovePosition((_rb2D.position + (_normalDir * transform.localScale.x) * _leapPastSpeed * Time.deltaTime));
				currentX = _rb2D.position.x;
				yield return null;
			}
		}



	}

	private void FlipDirection()
	{
		if (_lookRight && _moveDir.x < 0)
		{
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			_lookRight = false;
		}

		else if (!_lookRight && _moveDir.x > 0)
		{
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			_lookRight = true;
		}
	}

	public void Move(Vector2 direction)
	{
		_moveDir = direction;
	}

	public void Leap()
	{
		_leap = true;
	}

	public void LeapPast()
	{
		//For moves that require leap to go past the enemy
		_leapPast = true;
	}

	public void HitStagger(Vector2 hitDir)
	{
		Debug.Log("HIT STAGGER1");
		_hitDir = hitDir;
		_hitStagger = true;
	}
}
