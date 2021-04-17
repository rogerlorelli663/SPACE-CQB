using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoundToken : MonoBehaviour
{
    private bool done;
    public Sprite active;
    public Sprite disabled;

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = active;
        done = false;
    }

    public void SetDone()
    {
        gameObject.GetComponent<Image>().sprite = disabled;
        done = true;
    }

    public bool IsDone()
    {
        return done;
    }
}
