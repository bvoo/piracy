using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // bullet lifetime timer
    public float lifetime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * 5f;
        if (Physics2D.OverlapCircle(transform.position, 0.01f, LayerMask.GetMask("Enemy"))) {
            Destroy(gameObject);
        }
    }
}
