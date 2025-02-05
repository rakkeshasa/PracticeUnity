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

### [이동 구현]


## 메모사항

### Component Pattern
언리얼 엔진은 상속과 컴포넌트를 섞어서 사용하지만, 유니티 엔진은 컴포넌트 패턴을 이용한다.</br>
컴포넌트란 하나의 기능을 부품화 시켜 기능이 필요한 객체한테 부품을 붙이는 방식이다.</br></br>

컴포넌트는 각자의 기능을 가지며 스스로 동작하는 부품이므로 서로 독립적이여서 영향을 끼치지 않는다.</br>
따라서 컴포넌트는 코드의 의존성을 줄이고 재활용성을 높일 수 있다.</br></br>

### MonoBehaviour
모든 Unity 스크립트가 파생되는 기본 클래스이며 Start, Update와 같은 이벤트들을 제공한다.</br>
