using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
    Idle,
    Combat,
    Paused
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerMove playerMove;
    public EnemyMove enemyMove;
    public EnemyCollisions enemyCollisions;
    public GridInitialiser grid;

    public bool TurnBasedCombatInitiated => CurrentGameState == GameState.Combat;
    private GameState currentGameState = GameState.Idle;
    public GameState CurrentGameState { 
        get 
        { 
            return currentGameState;
        }
        set
        {
            currentGameState = value;
            UpdateGridAndMovementState();
        }
    }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        CurrentGameState = GameState.Idle;
    }

    private void UpdateGridAndMovementState()
    {
        enemyMove.enabled = TurnBasedCombatInitiated;
        playerMove.enabled = TurnBasedCombatInitiated;
        grid.gameObject.SetActive(TurnBasedCombatInitiated);
    }
    public void SetGridPosition(Vector3 newPos)
    {
        grid.CurrentPosition = newPos;
    }
    
    public void React()
    {
        print("REACT");
    }

    public void SetCombatPlayer(GameObject assumedPlayer)
    {
        if(assumedPlayer.TryGetComponent(out CharacterController playerController))
        {
            playerController.SetCombat();
            CurrentGameState = GameState.Combat;
        }
    }
}
