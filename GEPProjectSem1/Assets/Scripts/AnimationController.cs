using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator m_CharacterAnimator;
    PlayerState m_currentAnimation;

    private void Start()
    {
        m_CharacterAnimator = GetComponent<Animator>();

    }

    public void ChangeAnimationState(PlayerState newState)
    {
        //stop the same animation from interrupting itself --Guard
        if(m_currentAnimation == newState) return;

        //Play animation
        m_CharacterAnimator.Play(newState.ToString());

        //reassingn the current state
        m_currentAnimation = newState;
    }

}
