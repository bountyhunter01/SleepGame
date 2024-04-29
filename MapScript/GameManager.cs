using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI talkText;//텍스트창 
    //public TextMeshProUGUI nowMapLocation;//씬 넘어오면 뜨는 맵의 이름이 나옴

    public GameObject talkPanel;
    public TextMeshProUGUI playerName;
    public GameObject scanObjact;
    public GameObject menuSet;
    public GameObject player;//플레이어의 위치를 저장하기위해
                             // public CameraPlayerMode PlayerCamera;
                             //public BossBattleScene BossBattleScene;
    public TextMeshProUGUI MonsterName;

    public bool isAction;//액션을 실행하기위한
    public bool actionCollider;//몬스터 콜라이더를 삭제하기위한

    public TalkManager talkManager;
    public Image portraitImg;
    public int talkIndex;
    private PlayerMove2D playerMove2D;

    private BossBattleScene battleScene;
    private void Awake()
    {
        battleScene = GetComponent<BossBattleScene>();
    }

    private void Start()
    {
        actionCollider = true;
        GameLoad();

    }



    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

    }

    public void Action(GameObject scanObj)
    {

        scanObjact = scanObj;
        ObjData objData = scanObj.GetComponent<ObjData>();


        Talk(objData.id, objData.isMonster);

        talkPanel.SetActive(isAction);
        StartCoroutine(HideDialogueAfterDelay(2.5f)); // 대화 표시 시간
    }
    public void Talk(int id, bool isMonster)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if (talkData == null)
        {
            isAction = false;

            talkIndex = 0;//초기화
            if (isMonster)
            {
                MonsterName.text = "";
                //이부분 나중에 토크 매니저에 넣어서 호출할거임
                actionCollider = false;
            }
            else
                playerName.text = "";
            return;
        }
        if (isMonster)//몬스터인지아닌지의 기준 은 숫자넣고 체크박스하기
        {
            talkText.text = talkData.Split(':')[0];             //parse =전환시켜주는 함수
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            MonsterName.text = "몬스터 ";

            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            playerName.text = "나";
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;

        talkIndex++;//다음문장나오게하기위해



    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
            {

                menuSet.SetActive(false);

            }
            else
            {
                menuSet.SetActive(true);
            }
        }


    }

    //  private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //내 생각에는 보스배틀씬이라는 스크립트를 만나면  비활성화하고
    //    if (BossBattleScene.gameObject.GetComponent<BossBattleScene>())
    //  {
    //       PlayerCamera.GetComponent<CameraPlayerMode>().enabled = false;
    // }else if(//보스한테 죽고나서도 다시 활성화 + 보스를깨고나서도 활성화)
    //  {
    //     PlayerCamera.GetComponent<CameraPlayerMode>().enabled = true;
    // }
    // }
    public void GameSave()
    {   //간단하게 데이터를 저장기능을 지원하는 클래스
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        //텍스트도 저장해야할지 고민해보고
        PlayerPrefs.SetString("PlayerMapId", playerMove2D.currentMapName);
        string dataTimeString = DateTime.Now.ToString("g");//저 문자열에 들어있는게 중요함
        PlayerPrefs.SetString("PlayTime", dataTimeString);
        PlayerPrefs.Save();
        menuSet.SetActive(false);

        //  PlayerPrefs.SetFloat("NowDate", );
        // PlayerPrefs.SetFloat("MapId",);
        //플레이어 x,y
        //플레이어 맵이름
        //플레이어가 마지막저장한 날짜
        //플레이어 이름?
    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        string mapId = PlayerPrefs.GetString("PlayerMapId");
        //string nowDate = PlayerPrefs.GetString("PlayTime");

        player.transform.position = new Vector3(x, y, 0);
        playerMove2D.currentMapName = mapId;

    }
    public void GameExit()
    {
        Application.Quit();
    }

}
