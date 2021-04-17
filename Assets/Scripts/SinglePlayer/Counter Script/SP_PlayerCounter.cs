using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SP_PlayerCounter : MonoBehaviour
{
    public SP_PileCounter Melee;
    public SP_PileCounter Range;
    public SP_PileCounter Siege;
    private int totalPoints;

    public int Counter()
    {
        int sum = 0;
        string totalPower;
        sum += Melee.GetPileTotal();
        totalPower = "" + sum;
        this.GetComponentInChildren<Text>().text = totalPower;
        this.totalPoints = sum;
        return sum;
    }

    public int GetCurrentPoints()
    {
        return this.totalPoints;
    }

}
