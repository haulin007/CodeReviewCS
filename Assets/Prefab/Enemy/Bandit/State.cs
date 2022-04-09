using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void InterState();
    //return name of next State
    public abstract State ExitState();
    //return TRUE if need interense in new state
    public abstract bool UpdateState();
}
