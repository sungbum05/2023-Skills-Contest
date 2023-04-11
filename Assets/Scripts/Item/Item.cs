using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed;

    // Update is called once per frame
    virtual protected void Update()
    {
        Move();
    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameMgr.Instance.SoundList[3].Play();
            StartCoroutine(ItemEffect());
        }
    }

    void Move()
    {
        transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
    }

    protected virtual IEnumerator ItemEffect()
    {
        yield return null;
    }
}
