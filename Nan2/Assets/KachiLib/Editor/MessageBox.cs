#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class MessageBox
    : EditorWindow
{
    public string text;

    static public void Show(string title, string text)
    {
        GetWindow<MessageBox>();
        GetWindow<MessageBox>().titleContent = new GUIContent(title);
        GetWindow<MessageBox>().text = text;
    }

    void OnGUI()
    {
        EditorGUI.LabelField(new Rect(10, 10, 300, 17), text);
        if (GUI.Button(new Rect(10, 35, 300, 17), "OK"))
        {
            Close();
        }
    }
}
#endif