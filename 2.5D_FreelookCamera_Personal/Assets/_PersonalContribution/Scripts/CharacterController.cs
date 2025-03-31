using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterState
{
    Idle,
    Walk
}
public class CharacterController : MonoBehaviour
{
    private CharacterState state = CharacterState.Idle;
    private BodyRotation rotationLogic;
    private SpriteManager spriteManager;
    private StrictMovement characterMovement;
    private NavMeshCharacter navMeshCharacter;
    private PlayerMove combatMove;
    public Transform cameraFollowObject;
    public BodyRotation RotationLogic => rotationLogic;

    private ICharacterMovement currentMovement;
    private void Awake()
    {
        rotationLogic = GetComponentInChildren<BodyRotation>();
        spriteManager = GetComponentInChildren<SpriteManager>();
        characterMovement = GetComponent<StrictMovement>();
        navMeshCharacter = GetComponent<NavMeshCharacter>();
        spriteManager.BodyLogic = rotationLogic.transform;
        characterMovement.enabled = false;
        navMeshCharacter.enabled = false;
        combatMove = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        if (currentMovement != null)
        {
            state = currentMovement.IsMoving ? CharacterState.Walk : CharacterState.Idle;
            spriteManager.CharacterState = state.ToString();
        }
    }
    public void SetFollow(Vector3 followTarget)
    {
        SetStop();
        navMeshCharacter.MoveTowards(followTarget);
    }
    public void SetControl()
    {
        if (currentMovement != characterMovement)
        {
            if (currentMovement) currentMovement.enabled = false;
            currentMovement = characterMovement;
            currentMovement.enabled = true;
        }
    }
    public void SetStop()
    {
        if (currentMovement != navMeshCharacter)
        {
            if (currentMovement) currentMovement.enabled = false;
            currentMovement = navMeshCharacter;
            currentMovement.enabled = true;
        }
    }
    //public void SetStop()
    //{
    //    if (currentMovement) currentMovement.enabled = false;
    //    navMeshCharacter.enabled = false;
    //    characterMovement.enabled = false;
    //}
    public void SetCombat()
    {
        SetStop();
        currentMovement.enabled = false;
        GameManager.Instance.playerMove = combatMove;
    }

}
