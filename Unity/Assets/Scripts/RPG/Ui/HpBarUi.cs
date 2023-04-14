using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    public Transform myRoot;
    public Slider mySlider;

    public void updateHp(float v)
    {
        mySlider.value = v;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(myRoot.position);
        if(transform.position.z < 0.0f)
        {
            transform.position += Vector3.up * 10000.0f;
        }
    }
}
