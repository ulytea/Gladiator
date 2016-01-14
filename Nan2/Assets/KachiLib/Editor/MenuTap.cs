#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public static class MenuTap
{
    [MenuItem("KachiLib/Create/Singleton Creater %&m")]
    public static void KachiLib_Create_SingletonCreater()
    {
        SingletonCreater.ShowWindow();
    }

    [MenuItem("KachiLib/Create/Scene Creater %&n")]
    public static void KachiLib_Create_SceneCreater()
    {
        SceneCreater.ShowWindow();
    }

    [MenuItem("KachiLib/GameObject/2D Kit")]
    public static void KachiLib_GameObject_2DKit()
    {
        GameObject Kit = ObjectAdder.createObject("2D Kit");
        Kit.AddComponent<EventSystem>();
        Kit.AddComponent<StandaloneInputModule>();
        Kit.AddComponent<TouchInputModule>();
        Kit.transform.localPosition = Vector3.zero;
        Kit.transform.localScale = Vector3.one;
        #region 카메라
        GameObject camera = ObjectAdder.createObject("2D Camera", new Camera(), new GUILayer());
        camera.transform.SetParent(Kit.transform);
        camera.transform.localScale = Vector3.one;
        camera.transform.localPosition = new Vector3(0.0f, 0.0f, -100.0f);

        camera.AddComponent<FlareLayer>();
        camera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        camera.GetComponent<Camera>().backgroundColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        camera.GetComponent<Camera>().orthographic = true;
        camera.GetComponent<Camera>().orthographicSize = 5;
        camera.GetComponent<Camera>().nearClipPlane = 0.3f;
        camera.GetComponent<Camera>().farClipPlane = 100;
        camera.GetComponent<Camera>().depth = 0;
        camera.GetComponent<Camera>().useOcclusionCulling = true;

        #endregion
        #region 켄버스
        GameObject canvas = ObjectAdder.createObject("Canvas", new RectTransform(), new Canvas());
        canvas.transform.SetParent(Kit.transform);
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        canvas.GetComponent<Canvas>().pixelPerfect = true;
        canvas.GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();
        canvas.GetComponent<Canvas>().planeDistance = 100;
        canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1280.0f, 800.0f);
        canvas.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

        // EventSystem이 랙을 줄이기 위해 First Selected에 canvas를 미리 지정해줌
        Kit.GetComponent<EventSystem>().firstSelectedGameObject = canvas;
        #endregion
        #region 판넬
        GameObject panel = ObjectAdder.createObject("Panel", new RectTransform(), new CanvasRenderer());
        panel.transform.SetParent(canvas.transform);
        panel.AddComponent<ObjectDepth>();
        panel.GetComponent<RectTransform>().anchorMax = Vector2.one;
        panel.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        panel.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        panel.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        panel.transform.localPosition = Vector3.zero;
        panel.transform.localScale = Vector3.one;
        #endregion
    }

    [MenuItem("KachiLib/GameObject/3D Kit")]
    public static void KachiLib_GameObject_3DKit()
    {
        MessageBox.Show("준비중", "제작중인 기능입네다.");
    }

    [MenuItem("KachiLib/GameObject/Image %&i", false, 6)]
    public static void KachiLib_GameObject_Image()
    {
        if (Selection.activeObject != null)
        {
            GameObject[] selectObjects = Selection.gameObjects;

            for (int i = 0; i < selectObjects.Length; ++i)
            {
                GameObject image = ObjectAdder.createObject("Image", new RectTransform(), new CanvasRenderer());
                image.transform.SetParent(selectObjects[i].transform);
                image.AddComponent<Image>();
                image.AddComponent<ObjectDepth>();
                image.transform.localPosition = Vector3.zero;
                image.transform.localScale = Vector3.one;
            }
        }
        else
        {
            GameObject image = ObjectAdder.createObject("Image", new RectTransform(), new CanvasRenderer());
            image.AddComponent<Image>();
            image.AddComponent<ObjectDepth>();
            image.transform.localPosition = Vector3.zero;
            image.transform.localScale = Vector3.one;
        }
    }

    [MenuItem("KachiLib/GameObject/Text %&t")]
    public static void KachiLib_GameObject_Text()
    {
        if (Selection.activeObject != null)
        {
            GameObject[] selectObjects = Selection.gameObjects;

            for (int i = 0; i < selectObjects.Length; ++i)
            {
                GameObject text = ObjectAdder.createObject("Text", new RectTransform(), new CanvasRenderer());
                text.transform.SetParent(selectObjects[i].transform);
                text.AddComponent<Text>();
                text.AddComponent<ObjectDepth>();
                text.transform.localPosition = Vector3.zero;
                text.transform.localScale = Vector3.one;
            }
        }
        else
        {
            GameObject text = ObjectAdder.createObject("Text", new RectTransform(), new CanvasRenderer());
            text.AddComponent<Text>();
            text.AddComponent<ObjectDepth>();
            text.transform.localPosition = Vector3.zero;
            text.transform.localScale = Vector3.one;
        }
    }

    [MenuItem("KachiLib/GameObject/Panel %&p")]
    public static void KachiLib_GameObject_Panel()
    {
        if (Selection.activeObject != null)
        {
            GameObject[] selectObjects = Selection.gameObjects;

            for (int i = 0; i < selectObjects.Length; ++i)
            {
                GameObject panel = ObjectAdder.createObject("Panel", new RectTransform(), new CanvasRenderer());
                panel.transform.SetParent(selectObjects[i].transform);
                panel.AddComponent<ObjectDepth>();
                panel.GetComponent<RectTransform>().anchorMax = Vector2.one;
                panel.GetComponent<RectTransform>().anchorMin = Vector2.zero;
                panel.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                panel.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                panel.transform.localPosition = Vector3.zero;
                panel.transform.localScale = Vector3.one;
            }
        }
        else
        {
            GameObject panel = ObjectAdder.createObject("Panel", new RectTransform(), new CanvasRenderer());
            panel.AddComponent<ObjectDepth>();
            panel.GetComponent<RectTransform>().anchorMax = Vector2.one;
            panel.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            panel.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            panel.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            panel.transform.localPosition = Vector3.zero;
            panel.transform.localScale = Vector3.one;
        }
    }

    [MenuItem("KachiLib/GameObject/Button %&b")]
    public static void KachiLib_GameObject_Button()
    {
        if (Selection.activeObject != null)
        {
            GameObject[] selectObjects = Selection.gameObjects;

            for (int i = 0; i < selectObjects.Length; ++i)
            {
                GameObject button = ObjectAdder.createObject("KachiButton", new RectTransform(), new CanvasRenderer());
                button.transform.SetParent(selectObjects[i].transform);
                button.AddComponent<ObjectDepth>();
                button.AddComponent<KachiButton>();
                button.AddComponent<Image>();
                button.transform.localPosition = Vector3.zero;
                button.transform.localScale = Vector3.one;
            }
        }
        else
        {
            GameObject button = ObjectAdder.createObject("KachiButton", new RectTransform(), new CanvasRenderer());
            button.AddComponent<ObjectDepth>();
            button.AddComponent<KachiButton>();
            button.AddComponent<Image>();
            button.transform.localPosition = Vector3.zero;
            button.transform.localScale = Vector3.one;
        }
    }

    [MenuItem("KachiLib/Basic Folder/2D")]
    public static void KachiLib_BasicFolder_2D()
    {
        string[] folderList =
        {
            "Scenes",
            "Resources/Global",
            "Resources/Global/Scripts",
            "Resources/Global/Images",
            "Resources/Global/Shaders",
            "Resources/Global/Animations",
            "Resources/Global/Sounds",
            "Resources/Global/Fonts",
            "Resources/Global/Prefabs",
        };

        for (int i = 0; i < folderList.Length; i++)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.dataPath + "/" + folderList[i]);

            if (!di.Exists)
            {
                di.Create();
            }
        }

        EditorApplication.update();
        AssetDatabase.Refresh();
    }

    [MenuItem("KachiLib/Basic Folder/3D")]
    public static void KachiLib_BasicFolder_3D()
    {
        string[] folderList =
        {
            "Scenes",
            "Resources/Global",
            "Resources/Global/Scripts",
            "Resources/Global/Images",
            "Resources/Global/Models",
            "Resources/Global/Shaders",
            "Resources/Global/Animations",
            "Resources/Global/Sounds",
            "Resources/Global/Fonts",
            "Resources/Global/Materials",
            "Resources/Global/Prefabs",
        };

        for (int i = 0; i < folderList.Length; i++)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.dataPath + "/" + folderList[i]);

            if (!di.Exists)
            {
                di.Create();
            }
        }

        EditorApplication.update();
        AssetDatabase.Refresh();
    }

    [MenuItem("KachiLib/Help")]
    public static void KachiLib_Help()
    {
        MessageBox.Show("KachiLib", "KachiLib v0.4.2 (http://blog.naver.com/mkachi)");
    }
}
#endif