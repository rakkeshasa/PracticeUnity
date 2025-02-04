using UnityEngine;

public class TestSound : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public AudioClip audioClip;
    public AudioClip audioClip2;

    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayClipAtPoint();
        //audio.PlayOneShot(audioClip);

        // 소리가 동시에 출력된다
        //audio.PlayOneShot(audioClip2);
        //float lifeTime = Mathf.Max(audioClip.length, audioClip2.length);

        // lifeTime 후 소리도 사라진다.
        //GameObject.Destroy(gameObject, lifeTime);

        // 어느 객체가 이걸 들고 있는것이 과연 좋은가?
        // 예상치 못한 시간에 파괴되거나 사라진다면?

        i++;

        if(i % 2 == 0 )
        {
            Managers.Sound.Play(audioClip, Define.Sound.Bgm);
        }
        else
        {
            Managers.Sound.Play(audioClip2, Define.Sound.Bgm);
        }
        
    }
}
