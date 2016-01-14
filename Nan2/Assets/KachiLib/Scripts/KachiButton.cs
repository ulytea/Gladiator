using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class KachiButton
    : MonoBehaviour
    , IPointerClickHandler
    , IPointerUpHandler
    , IPointerDownHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public UnityEvent ButtonClick;
    public UnityEvent ButtonUp;
    public UnityEvent ButtonDown;
    public UnityEvent ButtonDownStay;
    public UnityEvent ButtonEnter;
    public UnityEvent ButtonExit;

    private bool _isPressed = false;

    void Update()
    {
        if (_isPressed)
        {
            ButtonDownStay.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ButtonClick.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        ButtonUp.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        ButtonDown.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonExit.Invoke();
    }
#if UNITY_EDITOR
    [ContextMenu("ScaleAni")]
    void AddScaleAni()
    {
        gameObject.AddComponent<ButtonScaleAni>();
    }

    [ContextMenu("ImageAni")]
    void AddShadowAni()
    {
        gameObject.AddComponent<ButtonImageAni>();
    }

    [ContextMenu("ShadowAni")]
    void AddImageAni()
    {
        gameObject.AddComponent<ButtonShadowAni>();
    }
#endif
}