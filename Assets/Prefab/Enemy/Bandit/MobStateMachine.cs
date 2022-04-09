using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStateMachine : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Starting STATE for FSM")]
    private State _state;

    private void Start()
    {
        _state.InterState();
    }
    void Update()
    {
        if(_state.UpdateState() == true)
        {
            _state = _state.ExitState();
            _state.InterState();
        }
    }
}
