using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ephicentr : MonoBehaviour
{
    public bool Status = false;

    public GameObject meteorePrefab;

    private float p_damage, p_duaration, p_radius, p_interval, p_curDuaration, p_curInterval;

    private int p_countPerWave;

    private Vector3 p_spawnPoint;
    private Quaternion p_rotate;

    public void Init(float _damage, float _duartion, float _radius, float interval, int _countPerWave, Vector3 _spawnPoint, Quaternion _rotate)
    {
        p_damage = _damage;
        p_duaration = _duartion;
        p_radius = _radius;
        p_interval = p_interval;
        p_spawnPoint = _spawnPoint;
        p_countPerWave = _countPerWave;
        p_rotate = _rotate;

        Status = true;
    }

    public void Start()
    {
        transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
    }

    public void Update()
    {
        p_curInterval += Time.deltaTime;

        if (p_curInterval >= p_interval)
        {
            for (int i = 0; i < p_countPerWave; i++)
            {
                var spawnPosition = new Vector3(Random.Range(p_spawnPoint.x - p_radius, p_spawnPoint.x + p_radius), 10.0f, Random.Range(p_spawnPoint.z - p_radius, p_spawnPoint.z + p_radius));
                var temp = Instantiate(meteorePrefab, spawnPosition, Quaternion.identity).GetComponent<Metior>();
                
                temp.Init(p_damage);
            }
            p_interval = 0.0f;
        }

        if (p_curDuaration >= p_duaration)
        {
            Destroy(gameObject);
        }
        p_curDuaration += Time.deltaTime;
    }
}
