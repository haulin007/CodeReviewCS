using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyState : State
{
    [SerializeField]
    private State _idleState;
    [SerializeField]
    private State _attackState;
    [Space(10)]
    [SerializeField]
    private static float _patrolSpeed = 5.0f;
    [SerializeField]
    Transform _leftSide;
    [SerializeField]
    Transform _rightSide;
    [Space(20)]
    [SerializeField]
    bool _defaultDirectionLeft = true;
    [SerializeField]
    bool _randomFirstDirection = true;
    [SerializeField]
    bool _checkEveryTime = false;

    private static float _minTargetDistanse = 0.2f;
    private bool _inPatrol;
    private bool _endPatrol = false;
    private Vector3 targetPos;
    private Vector3 _leftpos;
    private Vector3 _rightpos;
    private float _checkDistanse;
    private bool _findAPlayer = false;

    private void Awake()
    {
        
        _leftpos = _leftSide.position;
        _rightpos = _rightSide.position;

        if (_randomFirstDirection)
            targetPos = Random.value > 0.5f ? _leftpos : _rightpos;
        else
        {
            if (_defaultDirectionLeft)
                targetPos = _leftpos;
            else
                targetPos = _rightpos;
        }
    }
    public override State ExitState()
    {
        return _findAPlayer == false ?_idleState : _attackState;
    }

    public override void InterState()
    {
        _endPatrol = false;
        _inPatrol = false;
        _findAPlayer = false;
    }

    public override bool UpdateState()
    {
        if (_inPatrol == false && _endPatrol == false)
            StartCoroutine("Patrol");

        return _endPatrol;
    }
    IEnumerator Patrol()
    {
        _inPatrol = true;

        while (Vector3.Distance(transform.position, targetPos) > _minTargetDistanse)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * _patrolSpeed);
            yield return null;

            if (_checkEveryTime == true)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3.0f);
                if (colliders.Length > 1)
                    foreach (Collider2D collider in colliders)
                        if (collider.CompareTag("Character"))
                        {
                            _endPatrol = true;
                            _findAPlayer = true;
                            StopAllCoroutines();
                        }
            }
        }

        //change target pos
        targetPos = targetPos == _leftpos ? _rightpos : _leftpos;
        //end patrol
        _endPatrol = true;
        _inPatrol = false;
    }
}
