using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGaugeViwer : MonoBehaviour
{
    [SerializeField]
    private PlayerGage playerGage;

    private Slider sliderGauge;

    [SerializeField]
    private Camera gameCamera;
    [SerializeField]
    private Vector3 uiOffset;

    private void Awake()
    {
        sliderGauge = GetComponent<Slider>();
        // ���� ī�޶� ã�� �Ҵ��մϴ�. ���� ī�޶� �ƴ϶�� ���� �Ҵ��ؾ� �մϴ�.
        if (gameCamera == null)
            gameCamera = Camera.main;
    }

    private void Update()
    { //�����̴��� ���� ü�������� ������Ʈ 
        sliderGauge.value = playerGage.CurrentGauge + playerGage.MinGauge;
        // �÷��̾� ĳ������ ���� ��ġ�� ��ũ�� ��ǥ�� ��ȯ
       // Vector3 screenPosition = gameCamera.WorldToScreenPoint(playerGage.transform.position + uiOffset);

        // UI ����� ��ġ�� ������Ʈ
       // sliderGauge.transform.position = screenPosition;
    }
}
