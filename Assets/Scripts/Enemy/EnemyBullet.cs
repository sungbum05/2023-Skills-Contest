using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject ParticlePrefab;

    public float Damage = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroy"))
            Destroy(this.gameObject);

        else if(other.CompareTag("Player"))
        {
            Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
            GameMgr.Instance.Hp -= Damage;
            Destroy(this.gameObject);
        }

        else if(other.CompareTag("Shield"))
        {
            Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
