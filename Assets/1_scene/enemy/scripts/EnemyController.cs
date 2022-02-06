using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float headRotWeight;
    public Transform targetBot;
    public GameObject zone;
    Animator animator;
    BoxCollider BC;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        BC = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
