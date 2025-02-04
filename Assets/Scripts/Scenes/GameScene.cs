using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : BaseScene
{
    #region Info
    class Test
    {
        public int Id = 0;
    }
    class CoroutineTest : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            // return�� �־����� 1�� ���ϵ�����
            // yield�� ���̴� 2,3,4�� ������ ���ش�.
            /*yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;*/

            // yield return�� � ���̶� ������ �� �ִ�.
            /*yield return new Test() { Id = 1 };
            yield return null; // �ӽ� ����
            yield return new Test() { Id = 2 };
            yield return new Test() { Id = 3 };
            yield return new Test() { Id = 4 };*/

            // 2~3 ���̿��� �����Ű�� �ʹ�
            /*yield return new Test() { Id = 1 };
            yield return new Test() { Id = 2 };
            yield break;*/

            for (int i = 0; i < 10000000; i++)
            {
                // 10000��°���� ����.
                if (i % 10000 == 0)
                    yield return null;
            }
        }

        void GenerateItem()
        {
            // ������ ����
            // DB ����
            // �˰���

            // �������� ���������� DB ���忡 �����ҽ�..?
            // �� ������ �������� �����
            // �ذ�å: ������ ���� �� ��� ���߰� DB ������ �Ϸ�Ǹ� �̾ ����
        }

        float deltaTime = 0;
        void ExplodeAfter4Seconds()
        {
            deltaTime += Time.deltaTime;
            if(deltaTime >= 4)
            {
                // ����

                // ������: �� ƽ���� �ð��� ����ؾ���
            }
        }
    }

    void VeryComplicated()
    {
        // �ڷ�ƾ
        // �Լ��� �Ͻ����� ���״ٰ� �� �������� �ٽ� ������ �� �ִ�.
        // i�� 100���� ����ٰ� �ٽ� 100���� ������

        // 1. �Լ��� ���¸� ����/���� �����ϴ�
        //  -> ��û ���� �ɸ��� �۾��� ��� ���ų� 
        //  -> ���ϴ� Ÿ�ֿ̹� �Լ��� ��� Stop�ϰų� �����ϴ� ���
        // 2. �츮�� ���ϴ� Ÿ������ return�� �� �ִ�.(������ classŸ�Ե� ����)
        for (int i = 0; i < 1000000; i++)
        {
            Debug.Log("Hello");
            if (i % 1000 == 0)
                break;
        }
    }
    #endregion

    Coroutine co;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        // Managers.UI.ShowSceneUI<UI_Inven>();

        /*for (int i = 0; i < 5; i++)
        {
            Managers.Resource.Instantiate("Knight");
        }*/

        /*CoroutineTest test = new CoroutineTest();
        foreach(System.Object t in test)
        {
            *//*int value = (int)t;
            Debug.Log(value);*//*

            Test value = (Test)t;
            Debug.Log(value.Id);
        }*/

        //co = StartCoroutine("ExplodeAfterSeconds", 4.0f);

        // �������� �ڷ�ƾ �ߴܽ�Ű��
        //StartCoroutine("CoStopExplode", 2.0f);

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Knight");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        // Managers.Game.Spawn(Define.WorldObject.Monster, "Crusader");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);
    }

    IEnumerator CoStopExplode(float seconds)
    {
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Execute!!");
        if(co != null)
        {
            StopCoroutine(co);
            co= null;
        }
            
    }

    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explode Execute!!");
        co = null;
    }

    public override void Clear()
    {

    }
}
