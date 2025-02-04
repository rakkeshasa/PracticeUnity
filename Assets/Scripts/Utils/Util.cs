using System.ComponentModel;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();

        if(component == null)
            component = go.AddComponent<T>();

        return component;
    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }

    // where T : UnityEngine.Object��� ������ �޸� T�� Object�� ��ӹ��� Ÿ���̹Ƿ� Object�� �Լ��� ����� �� �ֽ��ϴ�.
    // where�� �����ϸ� T�� ���� Ÿ���̶�� ������ ������, Object �Լ��� �ڵ� ���ο��� ����� �� �˴ϴ�.
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                // ����Ƽ���� World�� ��ġ�ϴ� ������Ʈ�� ���� ��ġ�� �����ϴ��� ������ Transform Component�� ���� �ֽ��ϴ�. (������ �� GameObject������)
                // ���� � ������Ʈ�� �ǳ��� �� Transform�̳� GameObject ��� ���� �ǳ� �ִ��� Transform <->GameObject ������ ��ȯ�� �����ӽ��ϴ�.
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if(component != null)
                        return component;
                }
            }
            
        }
        else
        {
            // GetComponentsInChildren: ������ ������Ʈ�� ������ ���� ������Ʈ���� T Ÿ���� ��� ������Ʈ �� ���� ������Ʈ�� ��� �ڽĿ� ���� ������ �����ɴϴ�.
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                // component.name�� Button, Text �� T Ÿ���� Ŭ������ component�� ���� �ִ� gameObject�� ���մϴ�.
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
}
