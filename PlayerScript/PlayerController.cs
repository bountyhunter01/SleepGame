using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;

    [SerializeField]
    private string nextSceneName;

    private bool isInsomia = false;
    private Move2D move2D;
    private Weapon weapon;
    private Animator animator;
    private AttackPatten attackPatten;
    public GameObject playrtGauge;

    

    public CameraPlayerMode cameraMode;

    private float damage = 2;
    private void Awake()
    {
        move2D = GetComponent<Move2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
        attackPatten = GetComponent<AttackPatten>();
       cameraMode = FindObjectOfType<CameraPlayerMode>();
        playrtGauge.SetActive(false);
    } 
    private void Update()
    {
        //이동 공격 불가능하게설정
        if (isInsomia == true) return; 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        move2D.MoveTo(new Vector3(x, y, 0));

        if (Input.GetMouseButtonDown(0))
        {
            weapon.StartFiring();
        }if (Input.GetMouseButtonUp(0))
        {
            weapon.StopFiring();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossGauge>().TakeDamge(damage);
            StopAllCoroutines();
            Destroy(gameObject);
            Destroy(move2D);
            //공격패턴도 같이 사라지면 좋을것
        }
    }

    private void LateUpdate()
    {
        //플레이어 캐릭터가 화면 범위 바깥으로 나가지 못하도록함
        if(animator.enabled==false)
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x,stageData.LimitMax.x), Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));

        
    }
    public void OnInsomia()
    {
        move2D.MoveTo(Vector3.zero);//이동 초기화
       // animator.SetTrigger("OnInsomia");//사망애니메이션 재생
        Destroy(GetComponent<PolygonCollider2D>());//적과 충돌하지않게 충독박스 삭제
        isInsomia = true;//사망시 키 작동 불가 
        OnInsomiaEvent();
    }

    public void OnInsomiaEvent()
    {   //게임오버 씬으로이동
        SceneManager.LoadScene(nextSceneName);
        Destroy(cameraMode.gameObject);
    }
}

