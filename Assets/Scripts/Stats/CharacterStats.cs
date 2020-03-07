using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    private int maxHealth = 100;
    private int currentHealth;

    public Stat damage;
    public Stat armor;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // for test
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        //armor modify
        damage -= armor.GetValue();
        //clamp so damage cant be negative
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        Debug.Log(transform.name+" take damage: "+damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
    }


}
