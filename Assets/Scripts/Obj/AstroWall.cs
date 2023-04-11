using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroWall : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroy"))
            MapMakinManager.ReturnObj(this.gameObject);
    }
}
