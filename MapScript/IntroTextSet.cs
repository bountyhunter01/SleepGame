using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroTextSet : MonoBehaviour
{
    public GameObject introText;


    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            introText.SetActive(false);
        }
    }
    //�ϴ��� �����ϰ� ���߿� �ٽ� �ٿ�����
}
