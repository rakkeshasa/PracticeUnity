using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX
{
    // SceneManager은 이미 Unity에 있다.

    public BaseScene CurrentScene
    {
        get
        {
            return GameObject.FindAnyObjectByType<BaseScene>();
        }
    }

    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }
    
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
