using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUi : MonoBehaviour
{
    //�츮�� ui�� �׸��� ������ ��ũ�� �����̽���� �Ѵ�
    public Transform myRoot;
    public Slider mySlider;
    // Start is called before the first frame update
    public void upDateHp(float v)
    {
        mySlider.value = v;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position=Camera.main.WorldToScreenPoint(myRoot.position); //���� ������->��ũ�� �����̽���
        if (transform.position.z < 0.0f)
        {
            transform.position += Vector3.up * 10000.0f;
        }
    }
}
