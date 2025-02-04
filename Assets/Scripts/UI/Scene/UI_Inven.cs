using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        // gridPanel 하위의 UI_Inven_Item 다 날리기
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for(int i = 0; i < 8; i++)
        {
            GameObject item =  Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            //GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item");
            // item.transform.SetParent(gridPanel.transform);

            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"집행검{i}번");
        }
    }

}
