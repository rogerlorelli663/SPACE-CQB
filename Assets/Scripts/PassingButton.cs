using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingButton : MonoBehaviour
{
    private enum Type { PLAYER, OPPONENT }
    [SerializeField] Type type;
    private GameObject Player;

    private void Start()
    {
        if(type == Type.PLAYER)
        {
            Player = GameObject.Find("SP_Player");
        }
        else
        {
            Player = GameObject.Find("SP_Opponent");
        }
    }
    public void OnClick()
    {
        Player.GetComponent<Player_Behavior>().GetPlayerState().SetPassing();
        gameObject.SetActive(false);

    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

}
