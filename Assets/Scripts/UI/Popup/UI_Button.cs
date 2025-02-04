using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    
    int _score = 0;

    // ������ ������ ��µ�
    // [SerializeField]

    enum Buttons
    {
        PointButton
    }
    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }

    public override void Init()
    {
        base.Init();

        // c# ���÷���
        // typeof(Texts)�� �־��ִ� ������ Reflection���� enum Texts�� ������ ��ĵ�Ͽ�, ������ �̸��� ������Ʈ�� ã�� ���� ����ϴ� ���Դϴ�.
        Bind<Button>(typeof(Buttons)); // <Button> -> Button ������Ʈ�� Button ������Ʈ 
        Bind<Text>(typeof(Texts)); // <Text> -> Text ������Ʈ�� Text ������Ʈ
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        // Component �ڵ忡�� gameObject�� [���� �پ� �ִ� ������Ʈ]�� �ǹ�
        GameObject go = GetImage((int)Images.ItemIcon).gameObject; // ���̾��Ű â�� �ִ� ItemIcon ��ü�� ������
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"���� : {_score}";
    }
}
