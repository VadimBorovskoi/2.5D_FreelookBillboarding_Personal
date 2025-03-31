using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject[] characters;
    public float followDistance = 2f;
    [HideInInspector] public int activeCharacterIndex = 0;
    private List<Vector3> activeCharacterPositions = new List<Vector3>();
    public int maxPositionsToStore = 100;
    public float rotationSpeed = 10f;
    public GroupManager groupManager;

    void Start()
    {
        for (int i = 1; i < characters.Length; i++)
        {
            characters[i].GetComponent<MovementScript>().enabled = false;
        }
    }

    void Update()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchCharacter(i);
                //print(getActiveCharacter());
                break;
            }
        }

        FollowActiveCharacter();
    }

    public void SwitchCharacter(int index)
    {
        characters[activeCharacterIndex].GetComponent<MovementScript>().enabled = false;

        activeCharacterIndex = index;
        characters[activeCharacterIndex].GetComponent<MovementScript>().enabled = true;

        activeCharacterPositions.Clear();
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
        activeCharacterPositions.Insert(0, characters[activeCharacterIndex].transform.position);
        if (activeCharacterPositions.Count > maxPositionsToStore)
        {
            activeCharacterPositions.RemoveAt(activeCharacterPositions.Count - 1);
        }

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
                    Vector3 targetPosition = CalculateTargetPosition(i);
                    characters[i].transform.position = Vector3.Lerp(characters[i].transform.position, targetPosition, Time.deltaTime * 5f);

                    if ((targetPosition - characters[i].transform.position).magnitude > 0.1f)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - characters[i].transform.position);
                        characters[i].transform.rotation = Quaternion.Lerp(characters[i].transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }

    private Vector3 CalculateTargetPosition(int characterIndex)
    {
        int targetIndex = characterIndex - 1;
        if (targetIndex < 0)
            targetIndex += characters.Length;

        int storedIndex = Mathf.Min(targetIndex, activeCharacterPositions.Count - 1);
        Vector3 targetPosition = activeCharacterPositions[storedIndex];

        Vector3 offset = (characters[characterIndex].transform.position - characters[activeCharacterIndex].transform.position).normalized * followDistance;
        targetPosition += offset;

        return targetPosition;
    }

    public GameObject getActiveCharacter()
    { 
      return characters[activeCharacterIndex];
    }
}
