using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personLeaveZone : MonoBehaviour
{

    public GameObject enemy;
    protected BoxCollider BC;

    // Start is called before the first frame update
    void Start()
    {
        BC = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            EnemyController em = enemy.GetComponent<EnemyController>();
            em.headRotWeight = 0;
            BC.enabled = false;
        }
    }

}
