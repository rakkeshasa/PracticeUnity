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

        // �Ҹ��� ���ÿ� ��µȴ�
        //audio.PlayOneShot(audioClip2);
        //float lifeTime = Mathf.Max(audioClip.length, audioClip2.length);

        // lifeTime �� �Ҹ��� �������.
        //GameObject.Destroy(gameObject, lifeTime);

        // ��� ��ü�� �̰� ��� �ִ°��� ���� ������?
        // ����ġ ���� �ð��� �ı��ǰų� ������ٸ�?

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
