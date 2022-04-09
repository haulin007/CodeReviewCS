using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyState : State
{
    [SerializeField]
    private State _idleState;
    [SerializeField]
    private float _reloadTime = 1.0f;

    bool inPew = false;
    bool goNextState = false;
    public override State ExitState()
    {
        return _idleState;
    }

    public override void InterState()
    {
        print("Inter into ATTACK state");
        goNextState = false;
    }

    public override bool UpdateState()
    {
        if (inPew == false)
        {
            Vector3 pos = new Vector3();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3.0f);
            if (colliders.Length > 1)
            {
                foreach (Collider2D collider in colliders)
                    if (collider.CompareTag("Character"))
                    {
                        pos = collider.gameObject.transform.position;
                        goNextState = false;
                        break;
                    }
                    else
                        goNextState = true;
            }
            if (goNextState == false)
                StartCoroutine(Pew(pos));
        }

        return goNextState;
    }
    IEnumerator Pew(Vector3 pos)
    {
        inPew = true;
        print("pew");
        gameObject.GetComponent<IShoot>().Shoot(pos);
        yield return new WaitForSeconds(_reloadTime);
        inPew = false;
    }
}
