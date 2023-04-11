using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public BazierSkill Skill2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Hit(50.0f);
        }

        else if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            Skill2.SkillCount++;
        }

        else if(other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().Hit(50.0f);
        }
    }
}
