using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroModifierHolder : MonoBehaviour
{
    private int MaxModifiers = 8;
    private bool changed;

    private void Start()
    {
        changed = false;
    }
    public void CheckScale()
    {
        if(!changed && gameObject.transform.childCount > MaxModifiers)
        {
            gameObject.transform.localScale = new Vector3(.6F, .6F);
            changed = true;
        }
    }
}
