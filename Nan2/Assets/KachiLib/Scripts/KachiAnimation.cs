using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class KachiAnimation
    : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();
    public float Delay = 0.1f;
    public bool Run = true;
    public bool NativeSize = false;
    public bool Loop = true;

    [HideInInspector]
    public int curIndex = 0;
    private Image image;
    private float DelayCount = 0.0f;

    void OnEnable()
    {
        curIndex = 0;
        DelayCount = 0.0f;
        image.SetNativeSize();
    }

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Run.Equals(true))
        {
            DelayCount += Time.smoothDeltaTime;

            if (curIndex.Equals(0))
            {
                image.sprite = Sprites[curIndex];
                image.SetNativeSize();
            }

            if (DelayCount >= Delay)
            {
                ++curIndex;
                if (curIndex > Sprites.Count - 1)
                {
                    if (Loop.Equals(true))
                    {
                        curIndex = 0;
                    }
                    else
                    {
                        Run = false;
                        return;
                    }
                }
                image.sprite = Sprites[curIndex];

                if (NativeSize.Equals(true))
                {
                    image.SetNativeSize();
                }

                DelayCount = 0.0f;
            }
        }
    }

    public void pause()
    {
        Run = false;
    }

    public void resume()
    {
        Run = true;
    }
}