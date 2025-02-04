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
            // return만 있었으면 1만 리턴됐으나
            // yield를 붙이니 2,3,4도 리턴을 해준다.
            /*yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;*/

            // yield return은 어떤 값이라도 리턴할 수 있다.
            /*yield return new Test() { Id = 1 };
            yield return null; // 임시 정지
            yield return new Test() { Id = 2 };
            yield return new Test() { Id = 3 };
            yield return new Test() { Id = 4 };*/

            // 2~3 사이에서 종료시키고 싶다
            /*yield return new Test() { Id = 1 };
            yield return new Test() { Id = 2 };
            yield break;*/

            for (int i = 0; i < 10000000; i++)
            {
                // 10000번째마다 쉰다.
                if (i % 10000 == 0)
                    yield return null;
            }
        }

        void GenerateItem()
        {
            // 아이템 생성
            // DB 저장
            // 알고리즘

            // 아이템을 생성했으나 DB 저장에 실패할시..?
            // 둘 사이의 괴리감이 생긴다
            // 해결책: 아이템 생성 후 잠시 멈추고 DB 저장이 완료되면 이어서 진행
        }

        float deltaTime = 0;
        void ExplodeAfter4Seconds()
        {
            deltaTime += Time.deltaTime;
            if(deltaTime >= 4)
            {
                // 로직

                // 문제점: 매 틱마다 시간을 계산해야함
            }
        }
    }

    void VeryComplicated()
    {
        // 코루틴
        // 함수를 일시정지 시켰다가 그 시점에서 다시 시작할 수 있다.
        // i가 100에서 멈췄다가 다시 100에서 시작함

        // 1. 함수의 상태를 저장/복원 가능하다
        //  -> 엄청 오래 걸리는 작업을 잠시 끊거나 
        //  -> 원하는 타이밍에 함수를 잠시 Stop하거나 복원하는 경우
        // 2. 우리가 원하는 타입으로 return할 수 있다.(심지어 class타입도 가능)
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

        // 진행중인 코루틴 중단시키기
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
