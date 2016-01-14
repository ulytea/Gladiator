using UnityEngine;
using System.Text;

public class ObjectPool
    : MonoBehaviour
{
    private GameObject[] _pool;

    public int maxSize { get; private set; }

    public void init(GameObject userObject, int maxSize, Transform parent)
    {
        _pool = new GameObject[maxSize];
        this.maxSize = maxSize;

        for (int i = 0; i < maxSize; i++)
        {
            _pool[i] = Instantiate(userObject) as GameObject;
            _pool[i].transform.parent = parent;

            StringBuilder sb = new StringBuilder();
            sb.Append(userObject.name);
            sb.Append(" [Pool : ");
            sb.Append(i.ToString());
            sb.Append("]");
            _pool[i].name = sb.ToString();

            _pool[i].SetActive(false);
        }
    }

    public void init(GameObject userObject, int maxSize)
    {
        _pool = new GameObject[maxSize];
        this.maxSize = maxSize;

        for (int i = 0; i < maxSize; i++)
        {
            _pool[i] = Instantiate(userObject) as GameObject;

            StringBuilder sb = new StringBuilder();
            sb.Append(userObject.name);
            sb.Append(" [Pool : ");
            sb.Append(i.ToString());
            sb.Append("]");
            _pool[i].name = sb.ToString();

            _pool[i].SetActive(false);
        }
    }

    public GameObject getUsableObject()
    {
        for (int i = 0; i < maxSize; i++)
        {
            if (_pool[i].activeSelf.Equals(false))
            {
                _pool[i].SetActive(true);
                return _pool[i];
            }
        }
        return null;
    }
}