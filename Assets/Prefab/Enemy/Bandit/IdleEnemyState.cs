
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemyState : State
{
    [SerializeField]
    private State _patrolState;
    [SerializeField]
    private State _attackState;

    [SerializeField]
    private float _waitTime = 2.0f;

    private bool _inWaiting = true;
    private bool _findAPlayer = false;
    public override State ExitState()
    {
        return _findAPlayer ? _attackState : _patrolState;
    }

    public override void InterState()
    {
        StartCoroutine("Wait");
        _findAPlayer = false;
    }

    public override bool UpdateState()
    {
        if (_inWaiting == false)
        {

            print("state Idle Tick");
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3.0f);
            if (colliders.Length > 1)
            {
                foreach (Collider2D collider in colliders)
                    if (collider.CompareTag("Character"))
                    {
                        _findAPlayer = true;
                        StopAllCoroutines();
                        return true;
                    }
            }

            return true;
        }

        return false;
    }
    IEnumerator Wait()
    {
        _inWaiting = true;
        yield return new WaitForSeconds(_waitTime);
        _inWaiting = false;
    }
}
