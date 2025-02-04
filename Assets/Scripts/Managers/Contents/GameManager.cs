using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // 게임 오브젝트들 int화 시키기

    GameObject _player;
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    // Action에 몬스터가 늘어난 숫자(int)를 넣어줌
    public Action<int> OnSpawnEvent;
    public GameObject GetPlayer() { return _player; }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        
        switch(type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }
        
        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        // 가지고 있는 컨트롤러에 따라 타입 확인하기
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch(type)
        {
            case Define.WorldObject.Monster:
                if(_monsters.Contains(go))
                {
                    _monsters.Remove(go);
                    if (OnSpawnEvent != null)
                        OnSpawnEvent.Invoke(-1);
                }
                break;
            case Define.WorldObject.Player:
                if(_player == go)
                {
                    _player = null;
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
}
