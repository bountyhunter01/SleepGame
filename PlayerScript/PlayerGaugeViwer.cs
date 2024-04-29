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
        // 게임 카메라를 찾아 할당합니다. 메인 카메라가 아니라면 직접 할당해야 합니다.
        if (gameCamera == null)
            gameCamera = Camera.main;
    }

    private void Update()
    { //슬라이더에 현재 체력정보를 업데이트 
        sliderGauge.value = playerGage.CurrentGauge + playerGage.MinGauge;
        // 플레이어 캐릭터의 월드 위치를 스크린 좌표로 변환
       // Vector3 screenPosition = gameCamera.WorldToScreenPoint(playerGage.transform.position + uiOffset);

        // UI 요소의 위치를 업데이트
       // sliderGauge.transform.position = screenPosition;
    }
}
