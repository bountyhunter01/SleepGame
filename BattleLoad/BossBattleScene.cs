using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossBattleScene : MonoBehaviour
{

    private BossGauge bossObj;
    private BoxCollider2D boxCollider;

  
    private GameManager gameManager;
   

    public GameObject monsterObj;

    private CameraPlayerMode playerMode;
    private PlayerController playerController;

    public Camera rpgCamera;
    
    // Start is called before the first frame update

    TalkManager talkManager;

    public GameObject battleMonster;
    public Animator animator;
    private Image fadeIn;
    

    private EnemySpawner enemySpawner;

    public bool isSpace =false;
    public static BossBattleScene instance;

    public BgmController bgmController;
    public int npcId = 500;  // 예시로 NPC에 대한 고유 ID

    private MeteoriteSpawner meteoriteSpawner;

    private void Awake()
    {
        boxCollider = FindObjectOfType<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        playerMode = FindObjectOfType<CameraPlayerMode>();
        playerController = FindObjectOfType<PlayerController>();
        fadeIn = FindObjectOfType<Image>();
        talkManager = FindObjectOfType<TalkManager>();
     
        bgmController = GetComponent<BgmController>();
        bossObj = FindObjectOfType<BossGauge>();  // 씬이 로드될 때 BossGauge를 찾습니다.
        animator = FindObjectOfType<Animator>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        meteoriteSpawner = FindObjectOfType<MeteoriteSpawner>();

        if (instance == null)
        {

            DontDestroyOnLoad(animator);

        }
        else if (instance != this)
        {

            Destroy(animator);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 대화 시작
            StartCoroutine(StartDialogue(npcId)); // 
                  
        }
    }



    private IEnumerator StartDialogue(int npcId)
    {

        gameManager.talkPanel.SetActive(true); // 대화 패널 활성화
        int talkIndex = 0;
        string dialogue;
        isSpace = true;
        // 대화를 순차적으로 표시
        while ((dialogue = talkManager.GetTalk(npcId, talkIndex)) != null)
        {
            gameManager.talkText.text = dialogue;// UI에 대화 내용을 표시
            gameManager.Action(gameObject);// 대화 내용 업데이트와 함께 NPC 액션 수행

            // 'Jump' 버튼이 눌릴 때까지 대기
            yield return new WaitUntil(() => Input.GetButtonDown("Jump") && isSpace);

            yield return new WaitForSeconds(1f);
            talkIndex++;  // 다음 대화로 이동
        }
        gameManager.talkPanel.SetActive(false); // 대화가 끝나면 패널 비활성화

        // 대화 종료 후 전투 시작
        GoBattleStage();
    }

        //지금은 스테이지로 안감
        public void GoBattleStage()
        {
       
        animator.enabled = true;
        animator.SetBool("IsComeBack", false);
        isSpace = false;//스페이스바 눌러도 텍스트 안나오게
        Debug.Log(isSpace);
            //fadeIn.enabled = true;//페이드 인효과
            playerController.enabled = true;
            // battleMonster.SetActive(true);//배틀용 몬스터 오브젝트 활성화
           
            // fadeIn.enabled = false;//이거 제대로 작동하도록 고쳐야하고 아니면 애니메이터가 다시 작동하도록 만들어도됌
            // bgmController.ChangeBGM(BGMType.Boss);//배틀음악시작
            StartCoroutine(DisableAnimatorAfterDelay(2f));
              meteoriteSpawner.StartCoroutine(meteoriteSpawner.SpawnMeteorite());
    }
    private IEnumerator DisableAnimatorAfterDelay(float delay)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 애니메이터 비활성화
        animator.enabled = false;
       // fadeIn.enabled = false;
       enemySpawner.StartCoroutine(enemySpawner.SpawnEnemy());
    }
    private void Update()
    {


        if (bossObj.IsSleeping )
        {
            // battleMonster.SetActive(!bossObj.IsSleeping);
            animator.enabled = true;
            animator.SetBool("IsComeBack",true);//돌아오는 애니메이션
            gameManager.player.SetActive(true);   // RPG 플레이어 활성화
            rpgCamera.enabled = true;
            
           
            boxCollider.enabled = false; // 콜라이더 비활성화
           
            bgmController.StopBGM(); // 배틀 음악 정지
        }
    }

}
