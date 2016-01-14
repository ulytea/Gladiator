#if UNITY_EDITOR
using UnityEngine;

public class ObjectAdder
{
    public static GameObject createObject(string objectName, params Component[] components)
    {
        int failCount = 0;

        string _objectname = objectName;

        while (GameObject.Find(_objectname) != null)
        {
            failCount++;
            _objectname = objectName + "_" + failCount.ToString();
        }

        GameObject gameObject = new GameObject();
        nameSet(ref gameObject, _objectname);
        addComponent(gameObject, components);

        gameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        gameObject.transform.localScale = Vector3.one;

        return gameObject;
    }

    static public void addComponent(GameObject gameObject, Component[] components)
    {
        for (int i = 0; i < components.Length; ++i)
        {
            if (gameObject.GetComponent(components[i].GetType()).Equals(null))
            {
                gameObject.AddComponent(components[i].GetType());
            }
        }
    }

    static public void nameSet(ref GameObject gameObject, string name)
    {
        if (name.Equals(""))
        {
            return;
        }
        gameObject.name = name;
    }
}
#endif