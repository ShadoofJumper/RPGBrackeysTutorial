using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public float enemySeeRange = 10.0f;

    // for enemy movement we nead have nav agent to controll enemy movement
    private NavMeshAgent agent;
    // and we nead link to target we nead to chase
    private Transform target;
    private CharacterCombat enemyCombat;

    // Start is called before the first frame update
    void Start()
    {
        // get enemy nav agent
        agent = GetComponent<NavMeshAgent>();
        // get player game object, using singlton acces
        target = PlayerManager.instance.player.transform;
        enemyCombat = GetComponent<CharacterCombat>();
    }



    // Update is called once per frame
    void Update()
    {
        // calculate distance to taget and if near, then attack
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= enemySeeRange)
        {
            agent.SetDestination(target.position);
            // face target if we near to attack
            if (distance <= agent.stoppingDistance)
            {
                //face target
                FaceTarget();
                //attack taget
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                enemyCombat.Attack(targetStats);
            }
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (transform.position - target.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemySeeRange);
    }
}
