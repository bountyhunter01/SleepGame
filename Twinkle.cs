using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 0.1f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("TwinkleLoop");
    }

    public IEnumerator TwinkleLoop()
    {
        while (true)
        {
            yield return StartCoroutine(FadeEffect(1,0));
            yield return StartCoroutine(FadeEffect(0,1));
        }
    }

    public IEnumerator FadeEffect(float start , float end)
    {
        float currentTime = 0;
        float percent = 0;

        while (percent<1)
        {
            currentTime +=Time.deltaTime;
            percent = currentTime / fadeTime;


            //spriteRenderer.color.a =1.0f�� ���� ������ �Ұ����ϴ�
            //spriteRenderer.color = new Color()�� ���� �����ؾ��Ѵ�
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start, end , percent);
            spriteRenderer.color = color;

            yield return null; 
        }
    }
}
