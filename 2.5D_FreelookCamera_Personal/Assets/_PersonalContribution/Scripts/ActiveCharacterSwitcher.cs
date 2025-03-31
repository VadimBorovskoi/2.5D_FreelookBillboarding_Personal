using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

public class ActiveCharacterSwitcher : MonoBehaviour
{
    public static ActiveCharacterSwitcher Instance { get; private set; }

    [SerializeField] private FreelookRotation virtualCamera;
    public CharacterController[] characters;
    public float followDistance = 2f; // Distance between characters when following
    [HideInInspector] public int activeCharacterIndex = 0;
    private List<Vector3> activeCharacterPositions = new List<Vector3>(); // Store positions of the active character over time
    public int maxPositionsToStore = 100; // Number of positions to store
    public float rotationSpeed = 10f; // Rotation speed for characters
    public GroupManager groupManager;

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
    void Start()
    {
        SwitchCharacter(4);
    }

    void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Idle)
        {
            ManageInput();
            FollowActiveCharacter();
        }
        else
        {
            //for (int i = 0; i < characters.Length; i++)
            //{
            //    if (i != activeCharacterIndex)
            //    {
            //        characters[i].gameObject.SetActive(false);
            //    }
            //}
        }
    }

    private void ManageInput()
    {
        // Switch character based on input keys (1-5)
        for (int i = 0; i < characters.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchCharacter(i);
                break;
            }
        }
    }
    public void SwitchCharacter(int index)
    {
        characters[activeCharacterIndex].SetStop();

        activeCharacterIndex = index;

        // Enable control for the new character
        characters[activeCharacterIndex].SetControl();

        // Clear stored positions when switching characters
        activeCharacterPositions.Clear();

        UpdateCameraFollow(characters[activeCharacterIndex].cameraFollowObject);
    }

    public void SwitchCharacterByIndex(int index)
    {
        if (index >= 0 && index < characters.Length)
        {
            SwitchCharacter(index);
        }
    }

    private void FollowActiveCharacter()
    {
       
        UpdateStoredPositions();

        List<int> activeGroup = null;
        int activeCharacterNumber = int.Parse(characters[activeCharacterIndex].name); // assuming the character names are the numbers
        foreach (List<int> group in groupManager.GetGroups())
        {
            if (group.Contains(activeCharacterNumber))
            {
                activeGroup = group;
                break;
            }
        }

        for (int i = 0; i < characters.Length; i++)
        {
            if (i != activeCharacterIndex)
            {
                int characterNumber = int.Parse(characters[i].name); // assuming the character names are the numbers
                if (activeGroup != null && activeGroup.Contains(characterNumber))
                {
                    characters[i].SetFollow(CalculateTargetPosition(i));
                }
            }
        }
    }
    private void UpdateStoredPositions()
    {
        // Store position of the active character
        activeCharacterPositions.Insert(0, characters[activeCharacterIndex].transform.position);
        // Limit the number of stored positions
        if (activeCharacterPositions.Count > maxPositionsToStore)
        {
            activeCharacterPositions.RemoveAt(activeCharacterPositions.Count - 1);
        }
    }
    private Vector3 CalculateTargetPosition(int characterIndex)
    {
        // Determine the target index for the character in the chain
        int targetIndex = characterIndex - 1;
        if (targetIndex < 0)
            targetIndex += characters.Length; // Wrap around to the last character

        // Retrieve the corresponding position from the stored positions of the active character
        int storedIndex = Mathf.Min(targetIndex, activeCharacterPositions.Count - 1);
        Vector3 targetPosition = activeCharacterPositions[storedIndex];

        Vector3 offset = (characters[characterIndex].transform.position - characters[activeCharacterIndex].transform.position).normalized * followDistance;
        targetPosition += offset;

        return targetPosition;
    }

    public void UpdateCameraFollow(Transform obj)
    {
        virtualCamera.UpdateFollowTarget(obj);
    }
}
