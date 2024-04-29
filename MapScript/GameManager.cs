using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI talkText;//�ؽ�Ʈâ 
    //public TextMeshProUGUI nowMapLocation;//�� �Ѿ���� �ߴ� ���� �̸��� ����

    public GameObject talkPanel;
    public TextMeshProUGUI playerName;
    public GameObject scanObjact;
    public GameObject menuSet;
    public GameObject player;//�÷��̾��� ��ġ�� �����ϱ�����
                             // public CameraPlayerMode PlayerCamera;
                             //public BossBattleScene BossBattleScene;
    public TextMeshProUGUI MonsterName;

    public bool isAction;//�׼��� �����ϱ�����
    public bool actionCollider;//���� �ݶ��̴��� �����ϱ�����

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
        StartCoroutine(HideDialogueAfterDelay(2.5f)); // ��ȭ ǥ�� �ð�
    }
    public void Talk(int id, bool isMonster)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if (talkData == null)
        {
            isAction = false;

            talkIndex = 0;//�ʱ�ȭ
            if (isMonster)
            {
                MonsterName.text = "";
                //�̺κ� ���߿� ��ũ �Ŵ����� �־ ȣ���Ұ���
                actionCollider = false;
            }
            else
                playerName.text = "";
            return;
        }
        if (isMonster)//���������ƴ����� ���� �� ���ڳְ� üũ�ڽ��ϱ�
        {
            talkText.text = talkData.Split(':')[0];             //parse =��ȯ�����ִ� �Լ�
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            MonsterName.text = "���� ";

            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            playerName.text = "��";
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;

        talkIndex++;//�������峪�����ϱ�����



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
    //    //�� �������� ������Ʋ���̶�� ��ũ��Ʈ�� ������  ��Ȱ��ȭ�ϰ�
    //    if (BossBattleScene.gameObject.GetComponent<BossBattleScene>())
    //  {
    //       PlayerCamera.GetComponent<CameraPlayerMode>().enabled = false;
    // }else if(//�������� �װ����� �ٽ� Ȱ��ȭ + �������������� Ȱ��ȭ)
    //  {
    //     PlayerCamera.GetComponent<CameraPlayerMode>().enabled = true;
    // }
    // }
    public void GameSave()
    {   //�����ϰ� �����͸� �������� �����ϴ� Ŭ����
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        //�ؽ�Ʈ�� �����ؾ����� ����غ���
        PlayerPrefs.SetString("PlayerMapId", playerMove2D.currentMapName);
        string dataTimeString = DateTime.Now.ToString("g");//�� ���ڿ��� ����ִ°� �߿���
        PlayerPrefs.SetString("PlayTime", dataTimeString);
        PlayerPrefs.Save();
        menuSet.SetActive(false);

        //  PlayerPrefs.SetFloat("NowDate", );
        // PlayerPrefs.SetFloat("MapId",);
        //�÷��̾� x,y
        //�÷��̾� ���̸�
        //�÷��̾ ������������ ��¥
        //�÷��̾� �̸�?
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
