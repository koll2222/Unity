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
    int myScore = 0, myLife = 3;
    public int Life
    {
        get => myLife;
        set
        {
            myLife = Mathf.Clamp(value, 0, 5);
            myLifeUI.SetLife(myLife);
            if(myLife == 0)
            {
                ChangeState(State.GameOver);
            }
        }
    }
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
    public SpaceShip myShip;
    public GameObject myTitleUI;
    public LifeUI myLifeUI;
    public GameObject myGameOverUI;
    public TMPro.TMP_Text myScoreUI;    //Text Mesh Pro ÂüÁ¶

    public State myState = State.Create;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Title:
                myGameOverUI.SetActive(false);
                myTitleUI.SetActive(true);
                myPlayer.gameObject.SetActive(false);
                myShip.StopDrop();
                break;
            case State.Play:
                myGameOverUI.SetActive(false);
                myTitleUI.SetActive(false);
                myPlayer.gameObject.SetActive(true);
                myPlayer.Set_Active(true);
                myShip.StartDrop();
                myLifeUI.SetLife(myLife);
                break;
            case State.GameOver:
                myGameOverUI.SetActive(true);
                myShip.StopDrop();
                myPlayer.Set_Active(false);
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case State.Title:
                if (Input.anyKey)
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
