using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGage : MonoBehaviour
{
    [SerializeField]
    private float minGauge = 0;

    [SerializeField]
    private float maxGauge = 3;

    private float currentGauge;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    public float MinGauge => minGauge;//minGauge get만 변수에 접근가능 인수값으로
    public float CurrentGauge => currentGauge;

    private void Awake()
    {
        currentGauge = minGauge;
        spriteRenderer = FindObjectOfType<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }
    public void TakeDamage(float damage)//실제로는 데미지라기는얘매하다
    {
        currentGauge += damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        //3번맞으면 게이지가 쌓여서 불면증에 걸림
        if (currentGauge >= maxGauge)
        {
            playerController.OnInsomia();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

}
