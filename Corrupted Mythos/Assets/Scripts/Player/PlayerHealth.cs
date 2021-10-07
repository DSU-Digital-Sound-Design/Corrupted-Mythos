using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health, check=0, maxHealth;
    public Transform player;
    public Transform spawn;
    public GameObject death;
    public Slider hpBar;

    [HideInInspector]
    public CorruptedNode node;

    float timer;

    private void Start()
    {
        health = 100;

        hpBar.maxValue = health;
        hpBar.value = health;

        maxHealth = health;
    }

    
    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void minusHealth(int damage)
    {
        if (timer <= 0)
        {
            health -= damage;
            //update UI
            hpBar.value = health;
            timer = 0.25f;

            StartCoroutine(FlashObject(this.GetComponent<SpriteRenderer>(), Color.white, Color.red, 1f, 0.5f));
        }
        // respawn script
        if (health <= 0)
        {
            RespawnPlayer();
        }
        
    }
    public void addHealth(int gain)
    {
        if (health + gain <= maxHealth)
        {
        health += gain;
        }

        Debug.Log(health);
        //update UI
    }

    public void RespawnPlayer()
    {
        player.position = spawn.position;
        health = 100;
        hpBar.value = health;
        this.GetComponent<CharacterController2D>().m_FacingRight = true;
        this.GetComponent<SpriteRenderer>().color = Color.white;
        this.transform.localScale = new Vector2(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
        StopAllCoroutines();

        if(node != null)
        {
            node.ResetNodeActivity();
            node.active = false;
            node = null;
        }
    }
    IEnumerator FlashObject(SpriteRenderer toFlash, Color originalColor, Color flashColor, float flashTime, float flashSpeed)
    {
        float flashingFor = 0;
        Color newColor = flashColor;
        while (flashingFor < flashTime)
        {
            toFlash.color = newColor;
            flashingFor += Time.deltaTime;
            yield return new WaitForSeconds(flashSpeed);
            flashingFor += flashSpeed;
            if (newColor == flashColor)
            {
                newColor = originalColor;
            }
            else
            {
                newColor = flashColor;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("checkpoint"))
        {
            spawn.position = other.transform.position;
            check += 1;
            //Destroy(other);
        }
    }
}
