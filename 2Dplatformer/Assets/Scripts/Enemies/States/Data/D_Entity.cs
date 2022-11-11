using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newEnemyData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float maxHealth = 30,
        damageHopSpeed = 3,
        wallCheckDistance = .2f, ledgeCheckDistance = .4f, groundCheckRadius = .3f,
        minAgroDistance = 3, maxAgroDistance = 4,
        closeRangeActionDistance = 1,
        stunResistance = 3,
        stunRecoveryTime = 2;

    public GameObject hitParticle;

    public LayerMask whatIsGround, whatIsPlayer;
}
