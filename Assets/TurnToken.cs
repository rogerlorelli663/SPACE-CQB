using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnToken : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Opponent;
    void Start()
    {
        Player.SetActive(false);
        Opponent.SetActive(false);
    }

    public void ActivatePlayerTurn()
    {
        Player.SetActive(true);
        if(Opponent.activeSelf)
        {
            Opponent.SetActive(false);
        }
    }

    public void ActivateOpponentTurn()
    {
        Opponent.SetActive(true);
        if(Player.activeSelf)
        {
            Player.SetActive(false);
        }
    }
}
