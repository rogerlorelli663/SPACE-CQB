using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outcome : MonoBehaviour
{
    public void Start()
    {
        TurnDescriptionFieldOFF();
    }
    public void Display(string winner)
    {
        TurnDescriptionFieldON(gameObject); 
        this.GetComponentInChildren<Text>().text = winner;

        //StartCoroutine(Waiter());

        Invoke("TurnDescriptionFieldOFF", 5);
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(5);
        TurnDescriptionFieldOFF();
    }

    private void TurnDescriptionFieldON(GameObject field)
    {
        foreach (Transform child in field.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void TurnDescriptionFieldOFF()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
