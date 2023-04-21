using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class GameUtils :MonoBehaviour
{
    //�̱��� ���� �� ���� ���� �ϳ��� ������Ʈ�� ����Ѵ� 
    static GameUtils _inst = null;
    public static GameUtils Inst
    {
        get
        {
            if (_inst == null)
            {
               _inst = FindObjectOfType<GameUtils>();
                if (_inst==null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameUtils";
                    obj.AddComponent<GameUtils>();
                }
            }
            return _inst;
        }
    }
    public StringBuilder strBuilder = null;
    private void Awake()
    {
       GameUtils ins=FindObjectOfType<GameUtils>();
        if (ins != this)
        {
            Destroy(this); //�̹� �ִٸ� �ı� 
            return;
        }
        strBuilder = new StringBuilder();
    }
    public string MergeChar(string a,char b)
    {
        strBuilder.Clear();
        strBuilder.Append(a);
        strBuilder.Append(b);
        return strBuilder.ToString();
    }
    public string MergeString(string a, string b)
    {
        strBuilder.Clear();
        strBuilder.Append(a);
        strBuilder.Append(b);
        return strBuilder.ToString();
    }
}
