using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleAni
        : MonoBehaviour
    , IPointerUpHandler
    , IPointerDownHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public float scaleSpeed = 3.0f;
    public Vector3 overScale = new Vector3(1.2f, 1.2f, 1.2f);
    public Vector3 clickScale = new Vector3(0.9f, 0.9f, 0.9f);

    private Vector3 _normalScale;
    private bool isPressed;
    private bool isOver;

    void Awake()
    {
        _normalScale = transform.localScale;
    }

    void Update()
    {
        if (isOver.Equals(true) && isPressed.Equals(false))
        {
            if (transform.localScale.x <= overScale.x)
            {
                transform.localScale += new Vector3(scaleSpeed, 0.0f, 0.0f) * Time.smoothDeltaTime;
                transform.localScale += new Vector3(0.0f, scaleSpeed, 0.0f) * Time.smoothDeltaTime;

                if (transform.localScale.x >= overScale.x)
                {
                    transform.localScale = overScale;
                }
            }
        }
        else if (isPressed.Equals(true))
        {
            if (transform.localScale.x >= clickScale.x)
            {
                transform.localScale -= new Vector3(scaleSpeed, 0.0f, 0.0f) * Time.smoothDeltaTime;
                transform.localScale -= new Vector3(0.0f, scaleSpeed, 0.0f) * Time.smoothDeltaTime;

                if (transform.localScale.x <= clickScale.x)
                {
                    transform.localScale = clickScale;
                }
            }
        }
        else
        {
            if (transform.localScale.x <= _normalScale.x)
            {
                transform.localScale += new Vector3(scaleSpeed, 0.0f, 0.0f) * Time.smoothDeltaTime;
                transform.localScale += new Vector3(0.0f, scaleSpeed, 0.0f) * Time.smoothDeltaTime;

                if (transform.localScale.x >= _normalScale.x)
                {
                    transform.localScale = _normalScale;
                }
            }

            if (transform.localScale.x >= _normalScale.x)
            {
                transform.localScale -= new Vector3(scaleSpeed, 0.0f, 0.0f) * Time.smoothDeltaTime;
                transform.localScale -= new Vector3(0.0f, scaleSpeed, 0.0f) * Time.smoothDeltaTime;

                if (transform.localScale.x <= _normalScale.x)
                {
                    transform.localScale = _normalScale;
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }
}