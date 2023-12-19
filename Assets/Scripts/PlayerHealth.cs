using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMovement playerMovement;
    SpriteRenderer sR;

    public int maxHealth = 100;
    public int currentHealth;
    public bool dead = false;
    [Space]
    public float iFramesLength;
    public bool isInvincible = false;
    public Color32 iFrameColor;
    public Color32 normalColor;
    public Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        sR = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerMovement.bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && !isInvincible)
        {
            TakeDamage(34);

            if (currentHealth <= 0)
            {
                dead = true;
                playerMovement.Die();
                return;
            }
            
            StartCoroutine(IFrames());
        }
        if (playerMovement.bodyCollider.IsTouchingLayers(LayerMask.GetMask("Spike")))
        {
            playerMovement.Die();
            healthBar.fillAmount = 0;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        //Debug.Log("took damage; Current health = " + currentHealth);
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    IEnumerator IFrames()
    {
        isInvincible = true;
        sR.color = iFrameColor;

        yield return new WaitForSeconds(iFramesLength);

        sR.color = normalColor;
        isInvincible = false;
    }
}
