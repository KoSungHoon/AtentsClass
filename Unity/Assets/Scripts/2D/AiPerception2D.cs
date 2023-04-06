using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AiPerception2D : MonoBehaviour
{
    public LayerMask enemyMask;
    public UnityEvent<Transform> findEnemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Searching());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {

    }
    IEnumerator Searching()
    {
        Collider2D col = null;
        while (!col)
        {
            col = Physics2D.OverlapCircle(transform.position, 3, enemyMask);
            if (col != null)
            {
                findEnemy?.Invoke(col.transform);
            }//설정된 적 발견
            yield return new WaitForFixedUpdate();
        }
    }
}
