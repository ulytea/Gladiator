#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ObjectDepth), true)]
public class ObjectDepthEditor
    : Editor
{
    private ObjectDepth _objectDepth;
    private SerializedProperty _depth;

    void OnEnable()
    {
        _objectDepth = target as ObjectDepth;
        _depth = serializedObject.FindProperty("depth");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.BeginVertical();
        GUILayout.Label(new GUIContent("depth가 같을 경우 Hierarchy창의 순서로 depth가 결정됩니다."));

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(new GUIContent("Depth"),GUILayout.Width(60.0f));
            if (_depth.intValue.Equals(0)) 
            {
                GUI.color = new Color32(0, 0, 0, 152);
            }
            bool depthDown = GUILayout.Button("◀", GUILayout.Width(40.0f));
            if (depthDown)  { _depth.intValue -= 1; _objectDepth.transform.SetSiblingIndex(_depth.intValue); }

            GUI.color = new Color32(255, 255, 255, 255);
            EditorGUILayout.PropertyField(_depth, new GUIContent(), GUILayout.Width(200.0f));
            bool depthUp = GUILayout.Button("▶", GUILayout.Width(40.0f));
            if (depthUp) { _depth.intValue += 1; _objectDepth.transform.SetSiblingIndex(_depth.intValue); }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif