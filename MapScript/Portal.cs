using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private string sceneName; //�̵��� ���� �̸�
    [SerializeField]
    private Vector2 exitPosition;//��Ż�ⱸ��ġ
    private PlayerMove2D playerMove;


    private void Start()
    {
        if (playerMove == null)
        {
           playerMove = FindObjectOfType<PlayerMove2D>();    
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
        {
            playerMove = other.GetComponent<PlayerMove2D>();
            if (playerMove != null )
            {
                playerMove.resetPosition = exitPosition;
                playerMove.currentMapName = sceneName;
                SceneManager.LoadScene(sceneName);
            }
           
        }
    }
}
