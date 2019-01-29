using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IPointerExitHandler
{
    public Sprite activeImage;
    public Sprite inactiveImage;
    public bool first = false;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        if (first)
            Select();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Deselect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Deselect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Select();
    }

    private void Select()
    {
        //Debug.Log("Selected" + gameObject.name);
        image.sprite = activeImage;
    }

    private void Deselect()
    {
        //Debug.Log("Deselected" + gameObject.name);
        image.sprite = inactiveImage;
    }
}
