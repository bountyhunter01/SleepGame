using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterColliderDestroy : MonoBehaviour
{
    GameManager gameManager;
    BoxCollider2D boxCollider;
   
   
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {//���� ���������� ���������� �ݶ��̴��� ��������
        if (!gameManager.actionCollider) // GameManager���� actionCollider�� false�� ���� ����
        {
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
        }
    }
}
