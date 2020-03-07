using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        Debug.Log("You die!");

        Destroy(gameObject);
    }

}
