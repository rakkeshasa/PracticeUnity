using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    private void Start()
    {
        Init();
    }
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        // c#에서 지원하는 enum을 string으로 가져오는 기능
        // Enum에 있는 모든 요소를 가져옴 -> 배열로 관리
        // Reflection을 이용해서 Enum.GetNames를 하면 enum Buttons를 구성하는 각 요소들을 string 배열로 추출할 수 있는 것
        string[] names = Enum.GetNames(type);

        // _objects에는 [PointText 오브젝트의 Text 컴포넌트, ScoreText 오브젝트의 Text 컴포넌트]
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            // GameObject는 Component가 아니여서 찾지 못한다.
            // Object - GameObject, Component(상속 관계)
            if (typeof(T) == typeof(GameObject))
            {
                // GameObject 버전만 따로 만들어줌
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                // gameObject는 이 스크립트 컴포넌트가 부착된 게임 오브젝트(UI_Button 프리펩)
                // 배열은 참조형이기 때문에, dictionary에 넣어준 애와 동일한 데이터를 수정하게 됩니다.
                // FindChild는 Text나 Image같은 컴포넌트를 return함
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }

            if (objects[i] == null)
            {
                Debug.Log($"Failed to bind({names[i]})");
            }
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
        {
            return null;
        }

        // object[idx]를 T로 cast하고 리턴
        return objects[idx] as T;
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go); // ItemIcon객체에 있는 UI_EventHandler.cs 컴포넌트를 가져옴

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }

        evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
    }
}
