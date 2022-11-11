using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    public float stunTime = 3, stunKnockbackTime = .2f, stunKnockbackSpeed = 20;
    public Vector2 stunKnockbackAngle;
}
