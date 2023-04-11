using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ParticlePrefab; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroy"))
            Player.ReturnObj(this.gameObject);

        else if (other.CompareTag("Enemy"))
        {
            GameMgr.Instance.SoundList[0].Play();
            Instantiate(ParticlePrefab, transform.position, Quaternion.identity);

            other.GetComponent<Enemy>().Hit(20.0f);
            Player.ReturnObj(this.gameObject);
        }

        else if (other.CompareTag("Boss"))
        {
            GameMgr.Instance.SoundList[0].Play();
            Instantiate(ParticlePrefab, transform.position, Quaternion.identity);

            other.GetComponent<Boss>().Hit(20.0f);
            Player.ReturnObj(this.gameObject);
        }
    }
}
