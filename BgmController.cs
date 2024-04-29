using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BGMType {Stage = 0, Boss }
public class BgmController : MonoBehaviour
{//������ �ٲ� ����� ����� 
    [SerializeField]
    private AudioClip[] bgmClips;

    private AudioSource bgmSource;

    private BossBattleScene bgmBattleScene;

    private void Awake()
    {
        bgmSource = GetComponent<AudioSource>();
        bgmBattleScene = GetComponent<BossBattleScene>();
    }
    public void ChangeBGM(BGMType index)
    {
        // ����� Ŭ���� �����ϰ� ���
        bgmSource.clip = bgmClips[(int)index];
        bgmSource.Play();
    }

    public void StopBGM()
    {
        // ������� ����
        bgmSource.Stop();
    }

}
    
