using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] dragImages;
    RectTransform rectTransform;
    Canvas canvas;
    Vector2 originalPosition;
    bool isDragging = false;
    bool isHovering = false;
    Image[] images;
    List<Image> highlightedImages = new List<Image>();
    List<Image> groupedImages = new List<Image>();

    public static List<int> latestGroup = new List<int>();
    public static GameObject currentlyDraggedImage = null;
    public static Dictionary<int, Vector2> originalPositions = new Dictionary<int, Vector2>();

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
        FindImages();
    }

    void FindImages() => images = canvas.GetComponentsInChildren<Image>();

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        originalPosition = rectTransform.anchoredPosition; // Store the original position
        ImageDragHandler.currentlyDraggedImage = gameObject; // Set the currently dragged image
        Image draggedImage = gameObject.GetComponent<Image>();
        if (draggedImage != null && !highlightedImages.Contains(draggedImage))
            highlightedImages.Add(draggedImage);
        HighlightImage();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            HighlightImage();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        ImageDragHandler.currentlyDraggedImage = null; // Clear the currently dragged image
        GroupImages();
        UnhighlightImage();
        highlightedImages.Clear();
        groupedImages.Clear();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging)
        {
            isHovering = true;
            HighlightImage();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging)
        {
            isHovering = false;
            HighlightImage();
        }
    }

    void HighlightImage()
    {
        foreach (GameObject dragImage in dragImages) // Only consider the drag images
        {
            Image image = dragImage.GetComponent<Image>();
            RectTransform otherRectTransform = image.GetComponent<RectTransform>();
            if (isHovering && otherRectTransform != null && Vector2.Distance(rectTransform.anchoredPosition, otherRectTransform.anchoredPosition) < 50f)
            {
                if (!highlightedImages.Contains(image))
                    highlightedImages.Add(image);
            }
            else if (image == gameObject.GetComponent<Image>())
            {
                image.color = Color.green;
                if (!highlightedImages.Contains(image))
                    highlightedImages.Add(image);
            }
            else if (highlightedImages.Contains(image))
                image.color = Color.green;
            else
                image.color = Color.white;
        }
    }


    void UnhighlightImage()
    {
        foreach (GameObject dragImage in dragImages) // Only consider the drag images
        {
            Image image = dragImage.GetComponent<Image>();
            image.color = Color.white;
        }
    }

    void GroupImages()
    {
        latestGroup.Clear();
        foreach (Image image in highlightedImages)
        {
            latestGroup.Add(int.Parse(image.name)); // assuming the image names are the numbers
        }

        List<string> groupedImageNames = new List<string>();
        foreach (Image image in highlightedImages)
        {
            if (!groupedImages.Contains(image))
                groupedImages.Add(image);
        }
        foreach (Image image in groupedImages)
            groupedImageNames.Add(image.name);
        if (groupedImageNames.Count > 0)
            Debug.Log("Grouped Images: " + string.Join(", ", groupedImageNames.ToArray()));

        RearrangeImages();
    }

    void RearrangeImages()
    {
        float initialY = images[0].GetComponent<RectTransform>().anchoredPosition.y;
        float gap = 50f; // Set this to the gap you want between images
        for (int i = 0; i < groupedImages.Count; i++)
        {
            RectTransform rectTransform = groupedImages[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, initialY - i * gap);
        }
    }
}
