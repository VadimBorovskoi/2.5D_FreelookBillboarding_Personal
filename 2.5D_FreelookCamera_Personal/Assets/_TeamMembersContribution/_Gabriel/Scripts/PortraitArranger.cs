using UnityEngine;
using System.Collections.Generic;

public class PortraitArranger : MonoBehaviour
{
    public GameObject[] images; // The images to be arranged
    public GameObject[] dragImages; // The images to be dragged
    public GroupManager groupManager; // Reference to the GroupManager script
    public float gapBetweenGroups = 10f; // The gap between different groups
    public float gapWithinGroup = 0f; // The gap within the same group
    public float gapToDragImage = 50f; // The gap to the corresponding drag image

    void Update()
    {
        ArrangeImages();
    }

    void ArrangeImages()
    {
        float currentY = 450f; // The current y position for the next image
        List<List<int>> groups = groupManager.GetGroups();

        for (int i = 0; i < groups.Count; i++)
        {
            for (int j = 0; j < groups[i].Count; j++)
            {
                int imageNumber = groups[i][j];
                GameObject image = images[imageNumber - 1]; // assuming the image names are numbers from 1 to 5
                RectTransform rectTransform = image.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, currentY);

                // Update the position of the corresponding drag image
                GameObject dragImage = dragImages[imageNumber - 1];
                if (dragImage != ImageDragHandler.currentlyDraggedImage)
                {
                    RectTransform dragRectTransform = dragImage.GetComponent<RectTransform>();
                    dragRectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + rectTransform.rect.width + gapToDragImage, currentY);
                    ImageDragHandler.originalPositions[imageNumber] = dragRectTransform.anchoredPosition; // Update the original position
                }

                currentY -= rectTransform.rect.height + gapWithinGroup;
            }
            currentY -= gapBetweenGroups;
        }
    }
}
