using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour ,IDamageable<float>
{
    public float _Hp=100f;
    public float _Move_Speed = 40f;
    public float _AttackDamage = 10f;


    public void Damage(float damageTaken)
    {
        if (_Hp - damageTaken > 0)
            _Hp -= damageTaken;
        else
            _Hp = 0;

        Debug.LogWarning("HP=" + _Hp);
    }

    // Start is called before the first frame update
    void Start()
    {
        _Hp = 100f;
        Debug.Log("HP=" +_Hp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
