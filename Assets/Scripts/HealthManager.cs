using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public RocketController rocket;
    public float invincibilityLenght;
    private float invincibilityCounter;
    public HealthBar healthBar;

    public Renderer rocketRenderer;
    private float flashCounter;
    public float flashLenght = 0.1f;
    private bool isRespawining;
    private Vector3 respawnPoint;
    public float respawnLenght;


    void Start()
    {
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {

        respawnPoint = rocket.transform.position;
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                rocketRenderer.enabled = !rocketRenderer.enabled;
                flashCounter = flashLenght;
            }

            if (invincibilityCounter <= 0)
            {
                rocketRenderer.enabled = true;
            }
        }
    }


    public void HurtRocket(int dmg, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= dmg;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
               // rocket.KnockBack(direction);

                invincibilityCounter = invincibilityLenght;

                rocketRenderer.enabled = false;
                flashCounter = flashLenght;
            }


        }


    }

    public void Respawn()
    {
        //player.transform.position = respawnPoint;
        //currentHealth = maxHealth;

        if (!isRespawining)
        {
            StartCoroutine("RespawnCoroutine");
        }

    }

    public IEnumerator RespawnCoroutine()
    {
        isRespawining = true;
        rocket.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnLenght);
        isRespawining = false;

        rocket.gameObject.SetActive(true);

        RocketController charController = rocket.GetComponent<RocketController>();
        charController.enabled = false;
        rocket.transform.position = respawnPoint;
        charController.enabled = true;

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

        invincibilityCounter = invincibilityLenght;
        rocketRenderer.enabled = false;
        flashCounter = flashLenght;

    }
}
