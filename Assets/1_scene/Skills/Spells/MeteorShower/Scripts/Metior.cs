using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metior : MonoBehaviour
{
    private float Damage;

    public void Init(float _damage)
    {
        Damage = _damage;
    }

    private void Start()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
    }


    private void Update()
    {
        if (transform.position.y <= 0.0f)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position -= new Vector3(0.0f, 6.0f * Time.deltaTime, 0.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            Debug.Log("Damage");
            Destroy(gameObject);
        }
    }
}
