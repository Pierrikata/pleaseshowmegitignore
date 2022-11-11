using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField] private float maxHealth, knockBackSpeedX, knockBackSpeedY, knockBackDuration, knockBackDeathSpeedX, knockBackDeathSpeedY, deathTorque;
    [SerializeField] private bool applyKnockBack;

    [SerializeField]  private GameObject hitParticle;

    private float currentHealth, knockBackStart;
    private int playerFacingDirection;
    private bool playerOnLeft, knockBack;
    private PlayerController pc;
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;

    private void Start()
    {
        currentHealth = maxHealth;
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("Broken Top").gameObject;
        brokenBotGO = transform.Find("Broken Bottom").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }
    private void Update()
    {
        CheckKnockBack();
    }
    private void Damage(AttackDetails details)
    {
        currentHealth -= details.damageAmount;

        if (details.position.x < aliveGO.transform.position.x)
            playerFacingDirection = 1;
        else
            playerFacingDirection = -1;

        Instantiate(hitParticle, aliveGO.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (playerFacingDirection == 1)
            playerOnLeft = true;
        else
            playerOnLeft = false;

        aliveAnim.SetBool("PlayerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("Damage");

        if(applyKnockBack && currentHealth > 0.0f)
        {
            KnockBack();
        }

        if(currentHealth < 0.0f)
        {
            Die();
        }
    }
    private void KnockBack()
    {
        knockBack = true;
        knockBackStart = Time.time;
        rbAlive.velocity = new Vector2(knockBackSpeedX*playerFacingDirection, knockBackSpeedY);
    }
    private void CheckKnockBack()
    {
        if(Time.time >= knockBackStart + knockBackDuration && knockBack)
        {
            knockBack = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }
    private void Die()
    {
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(true);

        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBotGO.transform.rotation = aliveGO.transform.rotation;

        rbBrokenBot.velocity = new Vector2(knockBackSpeedX * playerFacingDirection, knockBackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockBackDeathSpeedX * playerFacingDirection, knockBackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}
