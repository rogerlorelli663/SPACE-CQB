using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTokenPile : MonoBehaviour
{
    private List<RoundToken> tokens;

    private void Start()
    {
        tokens = new List<RoundToken>();
        for (int i = 0; i < transform.childCount; i++)
        {
            tokens.Add(gameObject.transform.GetChild(i).gameObject.GetComponent<RoundToken>());
        }
    }

    public void DisableToken()
    {
        foreach(RoundToken token in tokens)
        {
            if(!token.IsDone())
            {
                token.SetDone();
                return;
            }
        }
    }
}
