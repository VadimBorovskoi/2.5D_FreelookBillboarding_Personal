using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpriteManager : ICameraTransformDependant
{
    private Animator animator;
    private Transform bodyLogic;
    private float currentAngle = 0;
    private string characterState = "Idle";

    [SerializeField] private float[] anglesSequence = new float[8] { 0, 45, 90, 135, 180, 225, 270, 315 };
    [SerializeField] private string[] directionsSequence = new string[8] {"Up", "UpLeft", "Left", "DownRight",
                                                            "Down", "DownLeft", "Right", "UpRight"};

    [SerializeField] private bool changeSpritesHalfwayThrough = true;

    [SerializeField] private float angleRange = 22.5f; //If the value is bigger than 45, will have to Clamp it

    public Transform BodyLogic { get => bodyLogic; set => bodyLogic = value; }
    public string CharacterState { get => characterState; set { 
            characterState = value;
            if (animator)
            {
                if (canChangeState)
                {
                    PlayAnimation(true);
                    StartCoroutine(DelayDirection());
                }
                
            }
        } }
    [SerializeField] private float directionChangeRestriction = .5f;
    [SerializeField] private float stateChangeRestriction = .5f;
    private bool canChangeDirection = true;
    private bool canChangeState = true;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
  

    public override void Work(Transform cameraTransform)
    {
        if (animator)
        {
            AdjustSpriteDirection(cameraTransform, changeSpritesHalfwayThrough);
        }
    }
    private void AdjustSpriteDirection(Transform cameraTransform, bool changeSpritesHalfway)
    {
        if(!canChangeDirection)
        {
            return;
        }
        float angleDiff = Mathf.Round(cameraTransform.eulerAngles.y - bodyLogic.eulerAngles.y);
        EulerAnglesClamper.Instance.ClampAngleValue(ref angleDiff);

        if(changeSpritesHalfway)
        {
            angleDiff = Array.Find(anglesSequence, elem => CompareAngleRange(angleDiff, elem));
            if (currentAngle.Equals(angleDiff)) return;
            currentAngle = angleDiff;
            PlayAnimation(false);
        }
        else
        {
            if (currentAngle.Equals(angleDiff)) return;

            if (anglesSequence.Contains(angleDiff))
            {
                currentAngle = angleDiff;
                PlayAnimation(false);
            }
        }
        StartCoroutine(DelayDirection());

    }

    private bool CompareAngleRange(float angleToCompare, float potentialAngle)
    {
        float borderOne = potentialAngle + Mathf.Clamp(angleRange, 0, 22.5f);
        float borderTwo = potentialAngle - Mathf.Clamp(angleRange, 0, 22.5f);
        EulerAnglesClamper.Instance.ClampAngleValue(ref borderOne);
        EulerAnglesClamper.Instance.ClampAngleValue(ref borderTwo);
        return (angleToCompare < borderOne && angleToCompare > borderTwo);
    }
    private void PlayAnimation(bool shouldStartOver)
    {
        if (shouldStartOver)
        {
            animator.Play(characterState + directionsSequence[Array.IndexOf(anglesSequence, currentAngle)]);
        }
        else
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

            float currentNormalizedTime = currentState.normalizedTime;

            float nextClipLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            float nextClipStartTime = currentNormalizedTime * nextClipLength;

            animator.Play(characterState + directionsSequence[Array.IndexOf(anglesSequence, currentAngle)], 0, nextClipStartTime);
        }
    }
    private IEnumerator DelayDirection()
    {
        canChangeDirection = false;
        yield return new WaitForSeconds(directionChangeRestriction);
        canChangeDirection = true;
    }
    private IEnumerator DelayState()
    {
        canChangeState = false;
        yield return new WaitForSeconds(stateChangeRestriction);
        canChangeState = true;
    }
}
