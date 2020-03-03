using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    const float animationSmoothCons = .1f;
    private Animator animator;
    private NavMeshAgent agent;

    private float currentSpeedPercent;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //get preccent of move speed
        currentSpeedPercent = agent.velocity.magnitude / agent.speed;

        // update animation
        animator.SetFloat("speedPreccent", currentSpeedPercent, animationSmoothCons, Time.deltaTime);
    }
}
