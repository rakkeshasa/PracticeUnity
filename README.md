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

### [이동 구현]
```
void Update()
{
    if (Input.GetKey(KeyCode.W))
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
        transform.position += Vector3.forward * Time.deltaTime * _speed;
    }
    .
    .
    .
}
```
transform.position : 월드 좌표계 기준</br>
transform.Translate : 로컬 좌표계 기준</br>
Quaternion.LookRotation(Vector3.방향) : 원하는 방향을 바라봄</br>
Quaternion.Slerp: 오브젝트를 부드럽게 회전시키는 함수</br>

기존에는 Update에서 키입력을 체크해 움직이도록 했으나, Update에서 매번 키입력을 확인하는 것은 부하가 크다고 생각해 입력은 따로 매니저가 관리하도록 했습니다.</br>

```
public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }
}

public class PlayerController : BaseController
{
    public override void Init()
    {
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch(State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
        }
    }
}

public class InputManager
{
    public Action<Define.MouseEvent> MouseAction = null;

    public void OnUpdate()
    {
        if(MouseAction != null)
        {
            if(Input.GetMouseButton(0))
                MouseAction.Invoke(Define.MouseEvent.PointerDown);
        }
    }
}
```
InputManager는 유일한 Managers에서 생성되고 호출될 수 있도록 했습니다.</br>
그리고 기존에 입력에 따라 캐릭터의 이동을 처리하던 PlayerController에는 Action을 이용해 입력을 InputManager로 부터 처리할 수 있게 했습니다.</br>
따라서 마우스 입력이 발생하면 구독을 신청한 OnMouseEvent가 호출되며 OnMouseEvent는 swith문에 따라 상황에 따라 알맞은 행동을 하게 됩니다.</br></br>


## 메모사항

### Component Pattern
언리얼 엔진은 상속과 컴포넌트를 섞어서 사용하지만, 유니티 엔진은 컴포넌트 패턴을 이용한다.</br>
컴포넌트란 하나의 기능을 부품화 시켜 기능이 필요한 객체한테 부품을 붙이는 방식이다.</br></br>

컴포넌트는 각자의 기능을 가지며 스스로 동작하는 부품이므로 서로 독립적이여서 영향을 끼치지 않는다.</br>
따라서 컴포넌트는 코드의 의존성을 줄이고 재활용성을 높일 수 있다.</br></br>

### MonoBehaviour
모든 Unity 스크립트가 파생되는 기본 클래스이며 Start, Update와 같은 이벤트들을 제공한다.</br>
MonoBehaviour를 상속한 클래스는 new를 통해 인스턴스화 할 수 없다.</br>

### 프로퍼티(Property)
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

### [SerializeField]
유니티에서 스크립트에서 전역변수를 public으로 설정하면 Inspector창에 보여지게 되지만, 주요 변수는 private로 설정하고 싶을 때가 있습니다.</br>
[SerializeField]를 사용하면 private로 설정할 수 있고 Inspector창에서도 접근이 가능합니다.</br></br>

사실 유니티에서는 스크립트의 public만 직렬화할 수 있습니다. 그러나 [SerializeField] 를 사용하면 private 필드도 직렬화할 수 있습니다. </br>
유니티에서 직렬화는 게임 상태를 저장 및 로드하거나 에디터와 런타임 간에 데이터를 전송하는 데 사용됩니다.</br>
다른 스크립트에서 해당 필드를 여전히 private 으로 감추면서도 직렬화를 통해 유니티 에디터의 인스펙터를 통한 값의 입력이 가능해집니다.</br>

### 리플렉션(Reflection)
리플렉션은 컴파일 시(런타임 중)에 코드에 대한 정보를 조사하고 조작할 수 있게 해주는 기능입니다.</br>
따라서 리플렉션을 사용하면 객체가 가지고 있는 모든 정보를 런타임에 동적으로 뜯어보고 분석할 수 있고, 정보를 얻어 직접 메서드를 호출하거나 프로퍼티를 변경할 수 있습니다.</br></br>

<strong>리플렉션의 주요 구성 요소</strong>
- Assembly: .NET 애플리케이션의 배포 단위로, 컴파일된 코드와 메타데이터를 포함합니다.
- Type: 클래스, 인터페이스, 열거형 등의 타입 정보를 나타냅니다.
- MemberInfo: 타입의 멤버(메서드, 필드, 프로퍼티 등)에 대한 정보를 제공합니다.
- MethodInfo: 메서드에 대한 상세 정보와 호출 기능을 제공합니다.
- FieldInfo: 필드에 대한 정보와 값 읽기/쓰기 기능을 제공합니다.
- PropertyInfo: 프로퍼티에 대한 정보와 값 읽기/쓰기 기능을 제공합니다

```
using System.Reflection;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void SayHello()
    {
        Console.WriteLine($"Hello, I'm {Name} and I'm {Age} years old.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 타입 정보 얻기
        Type personType = typeof(Person);

        // 프로퍼티 정보 얻기
        PropertyInfo[] properties = personType.GetProperties();
        foreach (var prop in properties)
        {
            Console.WriteLine($"Property: {prop.Name}, Type: {prop.PropertyType}");
        }

        // 메서드 정보 얻기
        MethodInfo[] methods = personType.GetMethods();
        foreach (var method in methods)
        {
            Console.WriteLine($"Method: {method.Name}");
        }

        // 동적으로 객체 생성
        object personInstance = Activator.CreateInstance(personType);

        // 프로퍼티 값 설정
        personType.GetProperty("Name").SetValue(personInstance, "Alice");
        personType.GetProperty("Age").SetValue(personInstance, 30);

        // 메서드 호출
        MethodInfo sayHelloMethod = personType.GetMethod("SayHello");
        sayHelloMethod.Invoke(personInstance, null);
    }
}
```
C#의 모든 객체들은 Object에서 파생되어 나오기 때문에 Object에 정의된 함수들을 사용할 수 있다</br></br>

<strong>장점</strong>
- 런타임에 타입 정보에 접근할 수 있어 동적이고 유연한 코드 작성이 가능합니다.
- 단위 테스트에서 private 멤버에 접근할 때 유용합니다.

<strong>단점</strong>
- 성능 오버헤드가 있어 빈번한 사용 시 애플리케이션 성능에 영향을 줄 수 있습니다.
- 코드의 복잡성이 증가할 수 있습니다.

### Action
C#의 Delegate, Func, Action은 C/C++의 함수 포인터와 비슷한 개념으로, 이벤트 처리 및 비동기 프로세스 처리 등을 위해 유연하고 재사용 가능한 코드를 작성하는 데 필수적인 요소입니다.

<strong>Delegate</strong></br>
Delegate는 특정 메서드 시그니처(입력 매개변수와 반환 타입)와 일치하는 메서드를 참조할 수 있는 타입입니다. </br>
이를 통해 함수를 변수처럼 다룰 수 있습니다.</br>
델리게이트는 명시적으로 정의되어야 하며, 호출하려는 메서드와 일치하는 시그니처를 가져야 합니다.</br>

```
public delegate float CustomDelegate(int a, int b);

public class MyClass
{
    public static float Divide(int a, int b)
    {
        return (float)a / b;
    }
}

public class MainClass
{
    public static void Main()
    {
        CustomDelegate del = MyClass.Divide;
        float result = del(10, 5);
        Console.WriteLine("Result: " + result);
    }
}
```
CustomDelegate 델리게이트는 두 개의 int 입력 매개변수를 받아들이고 float 값을 반환하는 MyClass.Divide 메서드를 참조합니다.</br>
델리게이트는 메서드 참조를 저장하는 데 사용되며, 호출 시 해당 메서드를 실행합니다.</br>
델리게이트는 하나의 메서드뿐만 아니라 여러 개의 메서드를 가리킬 수 있습니다.(멀태캐스팅 델리게이트)</br>

```
public delegate void MyDelegate(string message);

public class Example
{
    public static void PrintMessage1(string message)
    {
        Console.WriteLine("Message 1: " + message);
    }

    public static void PrintMessage2(string message)
    {
        Console.WriteLine("Message 2: " + message);
    }

    public static void Main()
    {
        MyDelegate del = PrintMessage1;
        del += PrintMessage2; // 델리게이트에 두 번째 메서드 추가

        del("Hello, Multicast!"); // 두 메서드를 순차적으로 호출
    }
}
```

<strong>Func</strong></br>
Func는 반환 타입이 void가 아닌 여러개의 매개변수를 가진 함수를 나타내는 제네릭 델리게이트입니다.</br>
제네릭(generic)이란 프로그래밍 언어에서 타입에 종속되지 않고, 재사용 가능한 코드를 작성하는 방법입니다.</br>

```
public class MyClass
{
    public static int GetNumber()
    {
        return 42;
    }

    public static string ToString(int number)
    {
        return "Number: " + number;
    }
}

public class MainClass
{
    public static void Main()
    {
        Func<int> numberFunc = MyClass.GetNumber;
        int number = numberFunc();
        Console.WriteLine("Number: " + number);

        Func<int, string> toStringFunc = MyClass.ToString;
        string result = toStringFunc(42);
        Console.WriteLine(result);
    }
}
```
이 경우, Func<int>는 매개변수가 없고 int 값을 반환하는 MyClass.GetNumber 메서드를 참조합니다. </br>
그리고 Func<int, string>은 하나의 int 매개변수를 받고 string을 반환하는 MyClass.ToString 메서드를 참조합니다.</br>
따라서 마지막으로 추가된 메서드의 반환 값이 최종적으로 반환됨에 유의해야 합니다.</br>

<strong>Action</strong></br>
Action은 반환 타입이 void인 메소드를 위해 특별히 설계된 제네릭 델리게이트입니다. </br>
여러개의 매개변수를 가질 수 있으며 멀티캐스팅이 가능합니다.</br>

```

```

<strong>정리</strong></br>
- <strong>Delegate</strong>: 특정 메서드 시그니처(입력 매개변수와 반환 타입)와 일치하는 메서드를 참조할 수 있는 타입이다.
- <strong>Func</strong>: 반환 값이 있는 메서드를 참조하는 제네릭 델리게이트다. 마지막 타입 매개 변수는 반환 타입을 나타내며, 나머지 매개 변수는 입력 매개 변수의 타입이다.
- <strong>Action</strong>: 반환 값이 없는(void) 메서드를 참조하는 제네릭 델리게이트다. 모든 타입 매개 변수는 입력 매개 변수의 타입을 나타낸다.
- 모두 멀티캐스팅이 가능하나, 반환값이 있는 Delegate/Func를 여러개 캐스팅한 경우 그 Delegate/Func의 반환값은 멀티캐스팅 시 마지막으로 추가된 메서드의 반환값이 된다.

