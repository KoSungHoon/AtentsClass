using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject orgObject;
    public int TotalCount = 3;
    public float Width = 5.0f;
    public float Height = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<TotalCount; ++i)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-Width*0.5f, Width *0.5f);
            pos.z += Random.Range(-Height * 0.5f, Height * 0.5f);
            GameObject obj =Instantiate(orgObject, pos, Quaternion.Euler(0, Random.Range(0.0f, 360.0f),0));
            obj.GetComponent<CharacterProperty>().DeathAlarm += ReSpawn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ReSpawning()
    {
        yield return new WaitForSeconds(10.0f);
        if (Monster.TotalCount < 3)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-Width * 0.5f, Width * 0.5f);
            pos.z += Random.Range(-Height * 0.5f, Height * 0.5f);
            GameObject obj = Instantiate(orgObject, pos, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            obj.GetComponent<CharacterProperty>().DeathAlarm += ReSpawn;
        }
    }
    void ReSpawn()
    {
        StartCoroutine(ReSpawning());
    }
}
