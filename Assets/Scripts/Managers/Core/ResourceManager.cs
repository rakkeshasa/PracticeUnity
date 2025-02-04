using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf("/");
            if(index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if(go != null)
            {
                return go as T;
            }
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        // �޸� �󿡸� �ö�(�������� prefab)
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        // prefab�� ���纻�� ���� ���� ��ġ
        if (original.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }
        GameObject go = Object.Instantiate(original, parent);
        // Clone ���ڿ��� ã�ƴ޶�
        /*int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);*/
        go.name = original.name;

        return go;
    }

    public void Destroy(GameObject go)
    {
        if(go == null)
        {
            return;
        }

        // ���࿡ Ǯ���� �ʿ��� ���̶�� Ǯ�� �Ŵ������� �ñ��.
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}
