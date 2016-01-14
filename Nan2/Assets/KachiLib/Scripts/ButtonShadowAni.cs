using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonShadowAni 
    : MonoBehaviour
    , IPointerUpHandler
    , IPointerDownHandler
{
    public Color32 clickColor = new Color32(130, 130, 130, 255);

    private Image _image = null;
    private Color32 _normalColor;

    void Awake()
    {
        _image = GetComponent<Image>();
        _normalColor = _image.color;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.color = _normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.color = clickColor;
    }
}