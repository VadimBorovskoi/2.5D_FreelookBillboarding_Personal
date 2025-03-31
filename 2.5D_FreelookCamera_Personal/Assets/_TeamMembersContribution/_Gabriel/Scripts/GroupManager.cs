using UnityEngine;
using System.Collections.Generic;

public class GroupManager : MonoBehaviour
{
    private List<List<int>> groups = new List<List<int>>();

    void Start()
    {
        // Initialize all images in one group
        List<int> initialGroup = new List<int>();
        for (int i = 1; i <= 5; i++) // assuming the image names are numbers from 1 to 5
        {
            initialGroup.Add(i);
        }
        groups.Add(initialGroup);
    }

    void Update()
    {
        if (ImageDragHandler.latestGroup.Count > 0)
        {
            // Remove the images in the latest group from their current groups
            foreach (List<int> group in groups)
            {
                group.RemoveAll(i => ImageDragHandler.latestGroup.Contains(i));
            }
            // Remove any empty groups
            groups.RemoveAll(group => group.Count == 0);
            // Add the latest group
            groups.Add(new List<int>(ImageDragHandler.latestGroup));
            // Clear the latest group
            ImageDragHandler.latestGroup.Clear();

            // Print the current groups
            PrintGroups();
        }
    }

    void PrintGroups()
    {
        for (int i = 0; i < groups.Count; i++)
        {
            Debug.Log("Group " + (i + 1) + ": " + string.Join(", ", groups[i]));
        }
    }

    public List<List<int>> GetGroups()
    {
        return groups;
    }
}
