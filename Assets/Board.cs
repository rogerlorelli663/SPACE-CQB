using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject PrimaryEnvironmentalModifier;

    private void Awake()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public GameObject ActivateDescriptor()
    {
        GameObject token = PrimaryEnvironmentalModifier.GetComponent<ModifierCharacteristics>().ActivateDescriptor();
        return token;
    }

    public Modifiers.EnviroModifier GetModifier()
    {
        return this.PrimaryEnvironmentalModifier.GetComponent<ModifierCharacteristics>().GetModifier();
    }

    public void PrintModifier()
    {
        Debug.Log("Active Modifier: " + PrimaryEnvironmentalModifier.name);
    }
}
