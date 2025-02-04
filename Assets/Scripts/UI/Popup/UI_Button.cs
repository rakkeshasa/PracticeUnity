using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    
    int _score = 0;

    // 툴에서 변수가 출력됨
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

        // c# 리플렉션
        // typeof(Texts)를 넣어주는 이유는 Reflection으로 enum Texts의 값들을 스캔하여, 동일한 이름의 오브젝트를 찾기 위해 사용하는 것입니다.
        Bind<Button>(typeof(Buttons)); // <Button> -> Button 오브젝트의 Button 컴포넌트 
        Bind<Text>(typeof(Texts)); // <Text> -> Text 오브젝트의 Text 컴포넌트
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        // Component 코드에서 gameObject는 [내가 붙어 있는 오브젝트]를 의미
        GameObject go = GetImage((int)Images.ItemIcon).gameObject; // 하이어라키 창에 있는 ItemIcon 객체를 가져옴
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
    }
}
