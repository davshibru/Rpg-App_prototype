using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5.0f))
        {
            EnemyController enemyController = hit.collider.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                GameObject obj = hit.collider.GetComponent<EnemyController>().zone;
                BoxCollider BC = obj.GetComponent<BoxCollider>();
                BC.enabled = true;
                enemyController.headRotWeight = 0.5f;
            }
        }
    }
}
