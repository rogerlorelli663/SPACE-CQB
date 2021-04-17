using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierCharacteristics : MonoBehaviour
{
    [SerializeField] private Modifiers.EnviroModifier Modifier;
    [SerializeField] private GameObject Token;
    [SerializeField] private GameObject Descriptor;

    private void Start()
    {
        DeactivateDescriptor();
    }

    public GameObject CreateToken()
    {
        GameObject token = Instantiate(Token, null);
        return token;
    }

    public GameObject ActivateDescriptor()
    {
        Descriptor.SetActive(true);
        return CreateToken();
    }

    public void DeactivateDescriptor()
    {
        if (Descriptor.activeSelf)
        {
            Descriptor.SetActive(false);
        }
    }
}
