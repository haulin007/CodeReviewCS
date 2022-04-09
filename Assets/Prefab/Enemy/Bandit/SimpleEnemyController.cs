using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour, IAttackible, IShoot
{
    [Header("main characteristics")]
    [SerializeField]
    private float _health = 100;
    [Space(10)]
    [Header("Gun option")]
    [SerializeField]
    Gun _gun;
    [SerializeField]
    private float _bulletSpeed = 15.0f;
    [SerializeField]
    private float _damage = 10.0f;
    [SerializeField]
    private float _reloadTime = 3.0f;
    [Space(10)]
    [Header("Patrol option")]
    [SerializeField]
    private bool _isPatrol = true;
    [SerializeField]
    private float _runSpeed = 5.0f;
    [SerializeField]
    private float _sideDelay = 2.0f;

    private bool gunIsLoad = true;

    public void GetDamage(int damage)
    {
        if (_health > damage)
            _health -= damage;
        else
            Death();
    }
    public void Shoot(Vector3 pos)
    {
         _gun.Shoot(pos);
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
