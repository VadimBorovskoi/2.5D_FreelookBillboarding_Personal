using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Xml.Linq;
using Unity.VisualScripting;

public class FreelookRotation : MonoBehaviour
{
    private CinemachineFreeLook vCam;

    [SerializeField] private bool shouldUseMouse = true;
    [SerializeField] private float rotateAmount = 45;
    [SerializeField] private float transitionTime = .3f;

    private float initialPosition = 0;
    private float targetPosition = 0;
    private float t = 0f;
    private float lastActiveMouseInput = 0;

    private int numOfAngles;
    private float[] fixedAngles;

    private bool canMove = true;
    private KeyCode currentKey;


  
       
    [SerializeField] private Follower FollowObject;
    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineFreeLook>();
        numOfAngles = (int) (360 / rotateAmount);
        fixedAngles = new float[numOfAngles];
        for (int i = 0; i < numOfAngles; i++)
        {
            if (rotateAmount * i > 180f)
            {
                fixedAngles[i] = -360 + rotateAmount * i;
            }
            else
            {
                fixedAngles[i] = rotateAmount * i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //MoveTowardsTarget();
        ManageInput();
    }
    private void FixedUpdate()
    {
        if (initialPosition == targetPosition)
            return;


        if (t > 1)
        {
            return;
        }
        t += Time.deltaTime / transitionTime;
        float newPosition = Mathf.SmoothStep(initialPosition, targetPosition, t);
        vCam.m_XAxis.Value = newPosition;
        if (targetPosition == vCam.m_XAxis.Value)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }

    }
    private void ManageInput()
    {
        if ((vCam.m_XAxis.m_InputAxisValue < .05 && vCam.m_XAxis.m_InputAxisValue > -0.05) == false)
        {
            lastActiveMouseInput = vCam.m_XAxis.m_InputAxisValue;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(currentKey == KeyCode.Q)
            {
                if (!canMove)
                {
                    return;
                }
            } 
            currentKey = KeyCode.Q;
                RotateCameraRight();
                return;

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentKey == KeyCode.E)
            {
                if (!canMove)
                {
                    return;
                }
            }
            currentKey = KeyCode.E; ;
          
                RotateCameraLeft();
                return;
                
        }
        if(shouldUseMouse == false)
        {
            vCam.m_XAxis.m_InputAxisName = "";
            return;
        } 
        else
        {
            vCam.m_XAxis.m_InputAxisName = "Gamepad X";
        }
        if (lastActiveMouseInput < 0)
        {
            RotateCameraRight();
            return;
        }
        if(lastActiveMouseInput > 0)
        {
            RotateCameraLeft();
        }
       
    }
    private void RotateCameraRight()
    {
        initialPosition = vCam.m_XAxis.Value;
        targetPosition = FindClosestFixedAngle(vCam.m_XAxis.m_InvertInput);
        lastActiveMouseInput = 0;
        t = 0;
    }

    private void RotateCameraLeft()
    {
        initialPosition = vCam.m_XAxis.Value;
        targetPosition = FindClosestFixedAngle(!vCam.m_XAxis.m_InvertInput);
        lastActiveMouseInput = 0;
        t = 0;
    }

    private float FindClosestFixedAngle(bool isPositive)
    {
        float remainder = initialPosition % rotateAmount;
       
        float differenceToNext = rotateAmount -  Mathf.Abs(remainder);

        if (isPositive)
        {
            if(initialPosition < -0.01f && !remainder.Equals(0))
            {
                return initialPosition + Mathf.Abs(remainder);
            }
            return initialPosition + differenceToNext;
        }
        else
        {
            if(initialPosition > 0.01f && !remainder.Equals(0))
            {
                return initialPosition - Mathf.Abs(remainder);
            }
            return initialPosition - differenceToNext;
        }
    }
    private float FindClosestElement(float value)
    {
        float closestElement = fixedAngles[0];
        float minDifference = Mathf.Abs(value - fixedAngles[0]);

        foreach (float element in fixedAngles)
        {
            float difference = Mathf.Abs(value - element);
            if (difference < minDifference)
            {
                minDifference = difference;
                closestElement = element;
            }
        }

        return closestElement;
    }
 
    public void UpdateFollowTarget(Transform newFollower)
    {
        FollowObject.target = newFollower;
    }
}