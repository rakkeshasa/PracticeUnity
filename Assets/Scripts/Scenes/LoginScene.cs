using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        /*List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            list.Add(Managers.Resource.Instantiate("Knight"));
        }

        foreach (GameObject obj in list)
        {
            Managers.Resource.Destroy(obj);
        }*/
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            // Game Scene�� �Ը� Ŭ ��� ���ҽ��� ��׶��忡�� �ε���
            // SceneManager.LoadSceneAsync("Game");

            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }

}
