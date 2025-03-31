using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInitialiser : MonoBehaviour
{
    [SerializeField] private Transform[] roots;
    public Vector3 CurrentPosition { get => transform.position; set {
            SetPosition(roots[Random.Range(0, roots.Length - 1)], value);
        } }

    public void SetPosition(Transform chosenRoot, Vector3 newPosition)
    {
        var gridPosDiff = (newPosition - chosenRoot.position);
        gridPosDiff.y = 0;
        transform.position += gridPosDiff;

    }
}
