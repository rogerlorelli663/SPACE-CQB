using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCollection : MonoBehaviour
{
    [SerializeField] private List<string> deck1; //Easy
    [SerializeField] private List<string> deck2; //Normal
    [SerializeField] private List<string> deck3; //Hard

    public List<string> GetDeck(int val)
    {
        switch (val)
        {
            case 0:
                return deck1;
            case 1:
                return deck2;
            case 2:
                return deck3;
            default:
                return deck1;
        }
    }
}
