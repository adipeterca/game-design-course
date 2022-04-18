﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator1 : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = .1f;
    NavMeshAgent agent;
    Animator animator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        float speedPercent1 = agent.velocity.magnitude/ agent.speed;
        animator.SetFloat("speedPercent1", speedPercent1, locomotionAnimationSmoothTime, Time.deltaTime);

    }
}