#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class SceneCreater
    : EditorWindow
{
    string sceneName = "";

    static public void ShowWindow()
    {
        EditorWindow.GetWindow<SceneCreater>();
        EditorWindow.GetWindow<SceneCreater>().titleContent = new GUIContent("Scene Creater");
    }

    void OnGUI()
    {
        sceneName = EditorGUI.TextField(new Rect(10, 10, 300, 17), "Scene name", sceneName);

        if (GUI.Button(new Rect(10, 35, 300, 17), "Create"))
        {
            if (sceneName.Equals(""))
            {
                MessageBox.Show("Error", "Scene의 이름을 입력해주세요!");
                return;
            }

            System.IO.DirectoryInfo di1 = new System.IO.DirectoryInfo(Application.dataPath + "/Resources");

            if (!di1.Exists)
            {
                di1.Create();
            }

            System.IO.DirectoryInfo di2 = new System.IO.DirectoryInfo(Application.dataPath + "/Scenes/");

            if (!di2.Exists)
            {
                di2.Create();
            }

            if (System.IO.File.Exists(Application.dataPath + "/Scenes/" + sceneName + ".unity"))
            {
                MessageBox.Show("Error", "이미 같은 이름을 가진 Scene이 존재합니다.");
            }
            else
            {
                System.IO.File.Create(Application.dataPath + "/Scenes/" + sceneName + ".unity");
            }

            string[] folderList =
            {
                "Images",
                "Models",
                "Shaders",
                "Animations",
                "Sounds",
                "Fonts",
                "Scripts",
                "Materials",
                "Prefabs",
            };

            for (int i = 0; i < folderList.Length; i++)
            {
                System.IO.DirectoryInfo di3 = new System.IO.DirectoryInfo(Application.dataPath + "/Resources/" + sceneName + "/" + folderList[i]);

                if (!di3.Exists)
                {
                    di3.Create();
                }
            }

            EditorApplication.update();
            AssetDatabase.Refresh();
        }
    }
}
#endif