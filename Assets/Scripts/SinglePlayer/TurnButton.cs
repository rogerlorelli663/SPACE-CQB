using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnButton : MonoBehaviour
{
    public GameMaster gm;

    void Start()
    {
        SpriteState ss = new SpriteState();
        
    }

    public void OnClick()
    {
        
        gm.NextTurn();
    }
}
