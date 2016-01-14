using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonImageAni
    : MonoBehaviour
    , IPointerUpHandler
    , IPointerDownHandler
{
    public Sprite clickImage;

    private Image _image = null;
    private Sprite _normalImage;

    void Awake()
    {
        _image = GetComponent<Image>();
        _normalImage = _image.sprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.sprite = _normalImage;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.sprite = clickImage;
    }
}