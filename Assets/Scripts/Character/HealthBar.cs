using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform _healthSprite;
    [SerializeField]
    private Character _character;
    void Start()
    {
        _character.HealthGetter.HealthChangeEvent += HandleHealthChange;
    }

    public void HandleHealthChange(int currHealth, int maxHealth)
    {
        _healthSprite.transform.localScale = new Vector3((float) currHealth / (float)maxHealth, 1f, 1f);
    }

    private void Update()
    {
        //To keep the health bar flipping everytime the character flips
        if(transform.parent.localScale.x == -1)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
