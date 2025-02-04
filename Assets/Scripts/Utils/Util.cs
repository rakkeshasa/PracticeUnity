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

    // where T : UnityEngine.Object라고 조건을 달면 T가 Object를 상속받은 타입이므로 Object의 함수를 사용할 수 있습니다.
    // where를 제거하면 T가 무슨 타입이라는 조건이 없으니, Object 함수는 코드 내부에서 사용이 안 됩니다.
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                // 유니티에서 World에 배치하는 오브젝트는 설령 배치와 무관하더라도 무조건 Transform Component를 갖고 있습니다. (심지어 빈 GameObject마저도)
                // 따라서 어떤 오브젝트를 건내줄 때 Transform이나 GameObject 어느 쪽을 건내 주더라도 Transform <->GameObject 사이의 변환이 자유롭습니다.
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
            // GetComponentsInChildren: 지정된 컴포넌트와 동일한 게임 오브젝트에서 T 타입의 모든 컴포넌트 및 게임 오브젝트의 모든 자식에 대한 참조를 가져옵니다.
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                // component.name은 Button, Text 등 T 타입의 클래스를 component로 갖고 있는 gameObject를 말합니다.
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
}
