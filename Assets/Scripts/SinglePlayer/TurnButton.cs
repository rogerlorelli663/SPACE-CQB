using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnButton : MonoBehaviour
{
    public GameMaster gm;
    public void OnClick()
    {
        gm.NextTurn();
    }
}
