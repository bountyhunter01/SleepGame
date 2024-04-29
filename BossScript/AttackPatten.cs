using System.Collections;
using UnityEngine;

public enum AttackType { CircleFire = 0,SingleFire}
public class AttackPatten : MonoBehaviour
{
    [SerializeField]
    private GameObject bossAttackPrefab;

    [SerializeField]
    private float bossAttackSpeed = 1.5f;
   
    public void StartFiring(AttackType attackType)
    {
        //열거형 이름과같은 코루틴 실행
        StartCoroutine(attackType.ToString());
    }
    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }
    private IEnumerator CircleFire()
    {
        float attackRate = bossAttackSpeed;//공격 주기 
        int count = 0;//발사체 생성개수
        float intervalAngle = 360 / count; //발사체사이의 각도
        float weightAngle = 0; //항상 같은 위치에서 발사하지않도록
                               //원형으로 발사하는 발사체생성
        while (true)
        {
            for (int i = 0; i < count; ++i) 
            {
                //발사체 생성하는 코드
                GameObject clone = Instantiate(bossAttackPrefab, transform.position, Quaternion.identity);


                //발사체 생성하는 코드
                float angle = weightAngle + intervalAngle * i;
                //발사체 이동방향 
                float x = Mathf.Cos(angle*Mathf.PI/180.0f);
                float y = Mathf.Sin(angle*Mathf.PI/180.0f);

                clone.GetComponent<Move2D>().MoveTo(new Vector2(x,y));
            }
            //발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1;

            //공격대기 시간
            yield return new WaitForSeconds(attackRate);
        }
    }
    private IEnumerator SingleFire()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, - 10, transform.position.z); // Y축 방향으로 10의 위치를 목표로 설정
        float attackRate = bossAttackSpeed;
        while (true)
        {
            GameObject clone = Instantiate(bossAttackPrefab, transform.position, Quaternion.identity);
            //발사체 이동방향
            Vector3 direction = (targetPosition - clone.transform.position).normalized;

            clone.GetComponent<Move2D>().MoveTo(direction);

            yield return new WaitForSeconds(attackRate);
        }
    }
}
