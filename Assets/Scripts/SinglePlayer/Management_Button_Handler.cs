using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Management_Button_Handler : MonoBehaviour
{
    [SerializeField] private Manage_Card_Collection Collection;
    [SerializeField] private Manage_Card_Collection Deck;
    [SerializeField] private AccountCharacteristics Account;
    [SerializeField] private Dropdown dropdown;

    private void Start()
    {
        Account = GameObject.Find("ActiveAccount").GetComponent<AccountCharacteristics>();
    }

    public void All()
    {
        
    }



}
