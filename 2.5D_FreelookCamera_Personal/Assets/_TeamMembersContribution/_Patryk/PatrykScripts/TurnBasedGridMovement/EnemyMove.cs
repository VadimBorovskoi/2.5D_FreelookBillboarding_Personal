using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : TacticsMove
{

    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
         Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }

        if (!moving)
        {
        FindNearestTarget();
        CalculatePath();
        FindSelectableTiles();
        if(actualTargetTile) actualTargetTile.target = true;
        }
        else
        {
            Move();
        }
    }

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);

    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        //Place for enemy AI
        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);
            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }
}
