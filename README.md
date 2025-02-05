# PracticeUnity

![background](https://github.com/user-attachments/assets/9fc2bce6-37fb-4bfb-a942-8d5cde243b80)

간단한 게임을 이용한 Unity 입문하기</br>

목차
---

## 간단한 소개
![1](https://github.com/user-attachments/assets/f32700a7-bb47-4c9b-8010-6d0b1386eaff)

기본적인 기능만 구현한 게임입니다.</BR></BR>
캐릭터 이동, 몬스터 AI 및 자동 생성, 공격을 이용해 상대 체력을 깍는 기능을 구현했습니다.</BR></BR>

## 플레이 영상
[![Video Label](http://img.youtube.com/vi/rVVuH5oMTKc/0.jpg)](https://youtu.be/rVVuH5oMTKc)
</BR>
👀Link: https://youtu.be/rVVuH5oMTKc</BR>
이미지나 주소 클릭하시면 영상을 보실 수 있습니다. </BR></BR>


## 기능 구현

### [매니저 구현]
모든 기능을 관리하는 최상위 매니저입니다.</br>
최상위 매니저인 Managers는 각각의 매니저(입력, 리소스, UI 등)에 접근할 수 있도록 해줍니다.</br>
따라서 Managers는 어디서든 접근할 수 있어야하므로 게임 시작시 Managers가 존재해야합니다.</br>

```
static Managers s_instance;
static Managers Instance { get { Init(); return s_instance; } }
```
Managers는 싱글톤으로 구현했습니다.</br>
싱글톤은 특정 클래스에 인스턴스가 하나만 존재하는 디자인 패턴입니다.</br>

```
static void Init()
{
    if(s_instance == null)
    {
        GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
        }

        DontDestroyOnLoad(go);
        s_instance = go.GetComponent<Managers>();
    }
}
```
Init함수는 Start에서 호출하는 함수로 싱글톤인 Managers의 s_instance를 생성해주는 역할을 합니다.</br>
```GameObject.Find```는 World상에 해당 이름으로 존재하는 객체를 찾아 GameObject 타입으로 반환합니다.</br>
만약에 '@Managers'라는 게임 오브젝트가 없다면 새롭게 생성하고 Managers 컴포넌트를 붙여 World에 생성합니다.</br>
```DontDestroyOnLoad```는 게임 실행중 오브젝트가 파괴되지 않도록 방지해줍니다.</br>
```GetComponent<Managers>```는 GameObject에 Managers 클래스의 컴포넌트를 갖고옵니다.</br>



## 메모사항

### Component Pattern
언리얼 엔진은 상속과 컴포넌트를 섞어서 사용하지만, 유니티 엔진은 컴포넌트 패턴을 이용한다.</br>
컴포넌트란 하나의 기능을 부품화 시켜 기능이 필요한 객체한테 부품을 붙이는 방식이다.</br></br>

컴포넌트는 각자의 기능을 가지며 스스로 동작하는 부품이므로 서로 독립적이여서 영향을 끼치지 않는다.</br>
따라서 컴포넌트는 코드의 의존성을 줄이고 재활용성을 높일 수 있다.</br></br>

### MonoBehaviour
모든 Unity 스크립트가 파생되는 기본 클래스이며 Start, Update와 같은 이벤트들을 제공한다.</br>
MonoBehaviour를 상속한 클래스는 new를 통해 인스턴스화 할 수 없다.</br>

## 프로퍼티(Property)
정보 은닉을 위해 클래스 내부의 변수나 함수들을 private나 protected로 선언합니다.</br>
이러한 변수에 접근하기 위해서는 public으로 선언한 get이나 set 함수를 만들어 접근할 수 있게합니다.</br>
하지만 프로젝트가 점점 커지면 get, set함수의 개수가 계속 늘어날 것입니다.</br>
따라서 C#에서는 get, set의 관리가 쉽도록 프로퍼티(Property)라는 문법을 제공합니다.</br>

```
public int Gold { get { return _gold; } set { _gold = value; } }

public int Gold
{
  get { return _gold; }
  set { _gold = value; }
}
```

두 버전은 같은 버전이며, 프로퍼티의 <strong>set접근자 내부의 value는 전달 받은 인자 값</strong>을 의미합니다.</br>
프로퍼티를 더 간략화 하기 위해 자동 구현 프로퍼티라는 문법이 있으며 이를 이용하면 코드를 한줄로 줄일 수 있습니다.</br>

```
class Knight
{
  protected int hp;
  public int Hp { get; set; }
```
