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
        // c#���� �����ϴ� enum�� string���� �������� ���
        // Enum�� �ִ� ��� ��Ҹ� ������ -> �迭�� ����
        // Reflection�� �̿��ؼ� Enum.GetNames�� �ϸ� enum Buttons�� �����ϴ� �� ��ҵ��� string �迭�� ������ �� �ִ� ��
        string[] names = Enum.GetNames(type);

        // _objects���� [PointText ������Ʈ�� Text ������Ʈ, ScoreText ������Ʈ�� Text ������Ʈ]
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            // GameObject�� Component�� �ƴϿ��� ã�� ���Ѵ�.
            // Object - GameObject, Component(��� ����)
            if (typeof(T) == typeof(GameObject))
            {
                // GameObject ������ ���� �������
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                // gameObject�� �� ��ũ��Ʈ ������Ʈ�� ������ ���� ������Ʈ(UI_Button ������)
                // �迭�� �������̱� ������, dictionary�� �־��� �ֿ� ������ �����͸� �����ϰ� �˴ϴ�.
                // FindChild�� Text�� Image���� ������Ʈ�� return��
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

        // object[idx]�� T�� cast�ϰ� ����
        return objects[idx] as T;
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go); // ItemIcon��ü�� �ִ� UI_EventHandler.cs ������Ʈ�� ������

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
