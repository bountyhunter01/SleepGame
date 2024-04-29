using System.Collections;
using UnityEngine;


public enum BossState { MoveToAppearPoint = 0, Phase01,Phase02 }
public class TreeBossMove2D : MonoBehaviour
{
    [SerializeField]
    private float bossAppearUpPointY = 4.64f;
    
    [SerializeField]
    private float bossAppearDownPointY = -3.45f;




    // 이동 속도와 방향 설정
    public float bossSpeed = 5f; // 예시로 5의 속도를 사용합니다.

    [SerializeField]
    private StageData stageData;

    private BossGauge bossGauge;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Move2D move2D;
    private AttackPatten attackPatten;

    private float bossTouchDamage = 3;//보스에 닿으면 죽음

    private void Awake()
    {
        move2D = GetComponent<Move2D>();
        attackPatten = GetComponent<AttackPatten>();
       bossGauge = GetComponent<BossGauge>();
    }


    public void ChangState(BossState newState)
    { //이전상태에 대한 
        StopCoroutine(bossState.ToString());
        //상태변경
        bossState = newState;
        //새로운 상태 재생
        StartCoroutine(bossState.ToString());

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerGage>().TakeDamage(bossTouchDamage);

        }
    }
    public IEnumerator MoveToAppearPoint()
    {
        move2D.MoveTo(Vector3.zero);
        while (true)
        {
            if (transform.position.y <= bossAppearUpPointY)
            {
                move2D.MoveTo(Vector3.zero);
                
                
            }
            if (transform.position.y >= bossAppearDownPointY)
            {//이부분 삭제해도될듯 
             // move2D.MoveTo(Vector3.up);
            }
         //  ChangState(BossState.Phase01);
            yield return null;
        }
    }
    private IEnumerator Phase01()
    {   
        attackPatten.StartFiring(AttackType.SingleFire);
        //처음이동방향을 오른쪽으로설정

        while (true)
        {

            // 현재 프레임에서 이동할 거리 계산
            float step = bossSpeed * Time.deltaTime;

            // 스테이지의 경계에 도달하면 방향 전환
            if (transform.position.x >= stageData.LimitMax.x)
            {
                Vector3 moveLeft = step*Vector3.left;
            }
            else if (transform.position.x <= stageData.LimitMin.x)
            {
              Vector3 moveRight = step*Vector3.right;
            }


            // Translate를 사용하여 오브젝트 이동
            // transform.Translate(Vector3.left * step);

           
        }
    }
   

}
