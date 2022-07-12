using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health 
{
    private int _maxHealth;
    public int MaxHealth { get { return _maxHealth; } }
    private int _currentHealth;
    public int CurrentHealth { get { return _currentHealth; } }

    public event Action<int, int> HealthChangeEvent;

    

    public Health(int maxHealth = 100)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if(_currentHealth < 0 )
        {
            _currentHealth = 0;
        }

        HealthChangeEvent?.Invoke(_currentHealth, _maxHealth);
    }

    public void AddHealth(int add)
    {
        _currentHealth += add;

        if( _currentHealth > _maxHealth )
        {
            _currentHealth = MaxHealth;
        }

        HealthChangeEvent?.Invoke(_currentHealth, _maxHealth);
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;

        HealthChangeEvent?.Invoke(_currentHealth, _maxHealth);
    }
}
