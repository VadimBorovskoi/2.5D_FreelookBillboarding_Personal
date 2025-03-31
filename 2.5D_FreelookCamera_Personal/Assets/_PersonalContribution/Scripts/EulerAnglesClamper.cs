using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulerAnglesClamper
{
    private static EulerAnglesClamper _instance;
    public static EulerAnglesClamper Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new EulerAnglesClamper();
            }
            return _instance;
        }
    }
    public void ClampObjectAngle(Transform transform)
    {
        Vector3 clampedEulerAngles = transform.eulerAngles;
        ClampAngleValue(ref clampedEulerAngles.x);
        ClampAngleValue(ref clampedEulerAngles.y);
        ClampAngleValue(ref clampedEulerAngles.z);
        //clampedEulerAngles.x = Mathf.Repeat(clampedEulerAngles.x, 360f);
        //clampedEulerAngles.y = Mathf.Repeat(clampedEulerAngles.y, 360f);
        //clampedEulerAngles.z = Mathf.Repeat(clampedEulerAngles.z, 360f);
        transform.eulerAngles = clampedEulerAngles;
    }
    public void ClampAngleValue(ref float angle)
    {
        angle = Mathf.Repeat(angle, 360f);
    }
}
