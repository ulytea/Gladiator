using UnityEngine;
using System.Collections;

public class AsynSceneLoad 
    : MonoBehaviour
{
    public string sceneName;
    public float DelayTime = 0.0f;

    public float LoadProgress { get; private set; }

    void Awake()
    {
        StartCoroutine("SceneLoad");
    }

    IEnumerator SceneLoad()
    {
        AsyncOperation asyncOperation = Application.LoadLevelAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        bool isIn = false;
        while (true)
        {
            LoadProgress = asyncOperation.progress;

            if (asyncOperation.isDone)
            {
                yield return null;
            }

            if (!asyncOperation.isDone)
            {
                if (!isIn)
                {
                    isIn = true;
                }
                else
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return new WaitForSeconds(DelayTime);
            }
        }
    }
}