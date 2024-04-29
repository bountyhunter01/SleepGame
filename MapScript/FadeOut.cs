using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image screenFlickerAnimator;// ȭ�� ������ �ִϸ�����
    void Start()
    {
        screenFlickerAnimator = FindObjectOfType<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = screenFlickerAnimator.color;
        if (color.a > 0)
        {
            color.a -= Time.deltaTime;
        }
        //�ٲ� ���������� ����
        screenFlickerAnimator.color = color;
    }
    public void StartFlickerEffect(float duration)
    {
        StartCoroutine(ToggleFlicker(duration));
    }

    private IEnumerator ToggleFlicker(float duration)
    {
        
        screenFlickerAnimator.enabled = false;
        yield return new WaitForSeconds(duration); // ������ �ð� ���� ���
      
        screenFlickerAnimator.enabled = true;
    }

}
