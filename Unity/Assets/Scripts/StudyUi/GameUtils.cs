using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class GameUtils :MonoBehaviour
{
    //싱글톤 패턴 씬 게임 에서 하나의 컴포턴트만 허용한다 
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
            Destroy(this); //이미 있다면 파괴 
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
