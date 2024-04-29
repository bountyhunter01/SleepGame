using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject luncherPrefab;
    [SerializeField]
    private float attackRate = 0.5f;//공격속도

   
    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }
    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }

    private IEnumerator TryAttack()
    {
        while (true)
        {
            Instantiate(luncherPrefab, transform.position, Quaternion.identity);
            
            yield return new WaitForSeconds(attackRate);
        }
    }
   
   
}
