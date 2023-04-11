using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazierBullet : MonoBehaviour
{
    public GameObject ParticlePrefab;

    public float Speed;

    public Vector3[] Points = new Vector3[4];
    public float CurTime;
    public float MaxTime;
    public bool IsTarget = false;

    public GameObject Target = null;

    public void Init(Transform StartPos, Transform EndPos, float BulletSpeed, float StPow, float EdPow)
    {
        Speed = BulletSpeed;
        MaxTime = Random.Range(0.8f, 1.0f);

        Points[0] = StartPos.position;

        Points[1] = StartPos.position
            + StPow * Random.Range(-1.0f, 1.0f) * StartPos.right
            + StPow * Random.Range(-1.0f, 1.0f) * StartPos.up
            + StPow * Random.Range(0.8f, 1.0f) * StartPos.forward;

        Points[1] = EndPos.position
          + EdPow * Random.Range(-1.0f, 1.0f) * EndPos.right
          + EdPow * Random.Range(-1.0f, 1.0f) * EndPos.up
          + EdPow * Random.Range(-1.0f, -0.8f) * EndPos.forward;

        Points[3] = EndPos.position;
        Target = EndPos.gameObject;

        transform.position = StartPos.position;
        IsTarget = true;
    }

    private void Update()
    {
        if (IsTarget && Target)
        {
            CurTime += Speed * Time.deltaTime;
            Points[3] = Target.transform.position;

            transform.position = BazierMove(Points[0], Points[1], Points[2], Points[3]);
        }

        else if (!Target || (!Target.CompareTag("Enemy") && !Target.CompareTag("Boss")))
            Destroy(this.gameObject);
    }

    public Vector3 BazierMove(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        float t = CurTime / MaxTime;

        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 cd = Vector3.Lerp(c, d, t);

        Vector3 abbc = Vector3.Lerp(ab, bc, t);
        Vector3 bccd = Vector3.Lerp(bc, cd, t);

        return Vector3.Lerp(abbc, bccd, t);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameMgr.Instance.SoundList[0].Play();
            Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
            other.GetComponent<Enemy>().Hit(10);
            Destroy(this.gameObject);
        }

        else if (other.CompareTag("Boss"))
        {
            GameMgr.Instance.SoundList[0].Play();
            Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
            other.GetComponent<Boss>().Hit(10);
            Destroy(this.gameObject);
        }
    }
}
