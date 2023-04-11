using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBomb : MonoBehaviour
{
    public static DodgeBomb Inst = null;
    public enum State
    {
        Create, Title, Play, GameOver
    }
    public State myState = State.Create;
    int myLife = 3;
    public int Life
    {
        get => myLife;
        set
        {
            myLife = Mathf.Clamp(value, 0 , 5);
            myLifeUI.SetLife(myLife);
            if(myLife == 0)
            {
                ChangeState(State.GameOver);
            }
        }
    }
    int myScore = 0;
    public int Score
    {
        get => myScore;
        set
        {
            myScore = value;
            myScoreUI.text = myScore.ToString();
        }
    }

    public DodgePlayer myPlayer;
    public SpaceSheep mySheep;
    public GameObject myTitleUI;
    public GameObject myGameOverUI;
    public LifeUI myLifeUI;
    public TMPro.TMP_Text myScoreUI;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case State.Title:
                myGameOverUI.SetActive(false);
                myTitleUI.SetActive(true);
                myPlayer.gameObject.SetActive(false);
                mySheep.StopDrop();
                break;
            case State.Play:
                myGameOverUI.SetActive(false);
                myTitleUI.SetActive(false);
                myPlayer.gameObject.SetActive(true);
                myPlayer.SetActive(true);
                mySheep.StartDrop();
                myLifeUI.SetLife(myLife);
                break;
            case State.GameOver:
                myGameOverUI.SetActive(true);
                mySheep.StopDrop();
                myPlayer.SetActive(false);
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Title:
                if(Input.anyKey)
                {
                    ChangeState(State.Play);
                }
                break;
            case State.Play:
                break;
            case State.GameOver:
                break;
        }
    }

    private void Awake()
    {
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(State.Title);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    public void OnRetry()
    {
        Score = 0;
        Life = 3;
        ChangeState(State.Play);
    }
}
