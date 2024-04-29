using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData _stageData;//적 생성을 위한 스테이지 크기 정보
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnerTime;//생성추가
    [SerializeField]
    private int maxEnemyCount = 100;//최대적 생성수
    [SerializeField]
    private GameObject bossGaugeBar;//보스 체력바
    [SerializeField]
    private GameObject textBossWarning;//보스 대사

    private TreeBossMove2D treeBossMove2D;

    [SerializeField]
    private BgmController bgmController;
    [SerializeField]
    private float WarningTextWait = 3.6f;

    [SerializeField]
    private GameObject bossObj;

    private void Awake()
    {
        treeBossMove2D = bossObj.GetComponent<TreeBossMove2D>();
        textBossWarning.SetActive(false);// 보스등장후 활성화
        bossObj.SetActive(false);
        bossGaugeBar.SetActive(false);
       
    }

    public IEnumerator SpawnEnemy()
    {
        Debug.Log("적이 생성됨");
        int currentEnemyCount = 0;
        
        while (true)
        {
            //x위치는 스테이지의 크기 범위 내에서 임의 값을 선택
            float positionX = Random.Range(_stageData.LimitMin.x, _stageData.LimitMax.x);
            Instantiate(enemyPrefab, new Vector3(positionX,_stageData.LimitMax.y+1, 0),Quaternion.identity);

            currentEnemyCount++;
            if (currentEnemyCount == maxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }
            yield return new WaitForSeconds(spawnerTime);

        }

    }
    private IEnumerator SpawnBoss()
    {
        bgmController.ChangeBGM(BGMType.Boss);
        textBossWarning.SetActive(true);
        yield return new WaitForSeconds(WarningTextWait);
        textBossWarning.SetActive(false);
        bossObj.SetActive(true);
        bossGaugeBar.SetActive(true);
        treeBossMove2D.StartCoroutine(treeBossMove2D.MoveToAppearPoint());
        //보스의 첫번쨰 상태인 지정된 위치로 이동실행
        //bossObj.GetComponent<BossMove2D>().ChangState(BossState.MoveToAppearPoint);
    }


}
