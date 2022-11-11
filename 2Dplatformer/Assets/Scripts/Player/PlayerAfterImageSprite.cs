using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f,
        timeActivated,
        alpha;
    [SerializeField]
    private float alphaSet = 0.8f,
        alphaDecay = 0.85f;

    private Transform player; // reference to player object to get its position and rotation

    private SpriteRenderer SR,  // reference to sprite renderer on this afterImage game object
        playerSR;               // reference to player game object sprite renderer
    private Color color;        // for decreasing the alpha of the sprite over time

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha -= alphaDecay * Time.deltaTime;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;

        if(Time.time >= (timeActivated + activeTime))
        {
            // Add back to pool
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
