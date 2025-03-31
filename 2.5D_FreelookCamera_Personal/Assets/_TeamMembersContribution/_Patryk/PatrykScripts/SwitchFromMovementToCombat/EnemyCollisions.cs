using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SetCombatPlayer(other.gameObject);
            GameManager.Instance.SetGridPosition(other.transform.position);
        }
    }
    
}
