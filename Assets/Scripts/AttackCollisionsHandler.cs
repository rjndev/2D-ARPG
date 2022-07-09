using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AttackCollisionsHandler : MonoBehaviour
{
	// Start is called before the first frame update
	private List<GameObject> _collidedChars;
	public List<GameObject> CollidedChars { get { return _collidedChars; } }

	private Vector2 _attackDir;
	private BoxCollider2D _attackCollider;

	void Awake()
	{
		_collidedChars = new List<GameObject>();
	}
	void Start()
	{
		_attackDir = new Vector2(1, 0);
		_attackCollider = GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if ((collider.tag == "Enemy" && transform.parent.tag == "Character") || (collider.tag == "Character" && transform.parent.tag == "Enemy"))
		{
			//collider.gameObject.GetComponent<BaseStateMachine>().HandleGetDamaged(_attackDir * transform.parent.localScale.x);
			_collidedChars.Add(collider.gameObject);
			Debug.Log("Added! " + collider.gameObject.name);
		}

	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if ((collider.tag == "Enemy" && transform.parent.tag == "Character") || (collider.tag == "Character" && transform.parent.tag == "Enemy"))
		{
			_collidedChars.Remove(collider.gameObject);
			Debug.Log("Removed! " + collider.gameObject.name);
		}
	}
}
