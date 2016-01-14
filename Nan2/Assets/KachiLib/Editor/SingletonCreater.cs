#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class SingletonCreater
    : EditorWindow
{
    private string singletonName = "";
    private bool isDontDestroyObject = false;

    static public void ShowWindow()
    {
        EditorWindow.GetWindow<SingletonCreater>();
        EditorWindow.GetWindow<SingletonCreater>().titleContent = new GUIContent("Singleton Creater");
    }

    void OnGUI()
    {
        singletonName = EditorGUI.TextField(new Rect(10, 10, 300, 17), "Singleton name", singletonName);
        isDontDestroyObject = EditorGUI.Toggle(new Rect(10, 35, 300, 17), "DontDestroyObject", isDontDestroyObject);

        if (GUI.Button(new Rect(10, 60, 300, 17), "Create"))
        {
            if (singletonName.Equals(""))
            {
                MessageBox.Show("Error", "Singleton의 이름을 입력해주세요!");
                Debug.LogError("[KachiLib] Singleton의 이름을 입력해주세요!");
                return;
            }

            FileStream saveStream = new FileStream(Application.dataPath + "/" + singletonName + ".cs", FileMode.Create);
            StreamWriter sw = new StreamWriter(saveStream, Encoding.UTF8);

            string[] scripts =
            {
                "using UnityEngine;",
                "using System.Collections.Generic;",
                "",
                "public class " + singletonName,
                "    :Singleton<" + singletonName + ">",
                "{",
                "    protected override void init()",
                "    {",
                "",
                "    }",
                "}",
            };

            string[] scriptsDontDistroy =
            {
                "using UnityEngine;",
                "using System.Collections.Generic;",
                "",
                "public class " + singletonName,
                "    :Singleton<" + singletonName + ">",
                "{",
                "    protected override void init()",
                "    {",
                "        DontDestroyOnLoad(this.gameObject);",
                "    }",
                "}",
            };

            if (isDontDestroyObject == true)
            {
                for (int i = 0; i < scriptsDontDistroy.Length; i++)
                {
                    sw.WriteLine(scriptsDontDistroy[i]);
                }
            }
            else
            {
                for (int i = 0; i < scripts.Length; i++)
                {
                    sw.WriteLine(scripts[i]);
                }
            }

            sw.Close();
            saveStream.Close();

            Debug.Log("[KachiLib] Singleton " + "\"" + singletonName + "\" 생성되었습니다.");
        }

        EditorApplication.update();
        AssetDatabase.Refresh();
    }
}
#endif