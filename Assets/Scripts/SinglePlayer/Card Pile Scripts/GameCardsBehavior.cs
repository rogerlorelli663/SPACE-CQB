using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardsBehavior : MonoBehaviour
{
    public GameObject Kushan_Assault_Frigate;
    public GameObject Kushan_Bomber;
    public GameObject Kushan_Carrier;
    public GameObject Kushan_Decoy;
    public GameObject Kushan_Destroyer;
    public GameObject Kushan_Heavy_Corvette;
    public GameObject Kushan_Heavy_Cruiser;
    public GameObject Kushan_Interceptor;
    public GameObject Kushan_Ion_Frigate;
    public GameObject Kushan_Light_Corvette;
    public GameObject Kushan_Missle_Destroyer;
    public GameObject Kushan_Multigun_Corvette;
    public GameObject Kushan_Scout;
    public GameObject Kushan_Support_Frigate;
    public GameObject Kushan_Salvage_Corvette;
    public GameObject Kushan_Repair_Corvette;

    public GameObject Taiidan_Assault_Frigate;
    public GameObject Taiidan_Attack_Bomber;
    public GameObject Taiidan_Carrier;
    public GameObject Taiidan_Decoy;
    public GameObject Taiidan_Destroyer;
    public GameObject Taiidan_Heavy_Corvette;
    public GameObject Taiidan_Heavy_Cruiser;
    public GameObject Taiidan_Interceptor;
    public GameObject Taiidan_Ion_Frigate;
    public GameObject Taiidan_Light_Corvette;
    public GameObject Taiidan_Missle_Destroyer;
    public GameObject Taiidan_Multigun_Corvette;
    public GameObject Taiidan_Scout_Fighter;

    private GameObject GetOriginalCard(string card_name)
    {
        GameObject card;

        switch (card_name)
        {
            case "Kushan_Assault_Frigate":
                card = Kushan_Assault_Frigate;
                break;
            case "Kushan_Bomber":
                card = Kushan_Bomber;
                break;
            case "Kushan_Carrier":
                card = Kushan_Carrier;
                break;
            case "Kushan_Decoy":
                card = Kushan_Decoy;
                break;
            case "Kushan_Destroyer":
                card = Kushan_Destroyer;
                break;
            case "Kushan_Heavy_Corvette":
                card = Kushan_Heavy_Corvette;
                break;
            case "Kushan_Heavy_Cruiser":
                card = Kushan_Heavy_Cruiser;
                break;
            case "Kushan_Interceptor":
                card = Kushan_Interceptor;
                break;
            case "Kushan_Ion_Frigate":
                card = Kushan_Ion_Frigate;
                break;
            case "Kushan_Light_Corvette":
                card = Kushan_Light_Corvette;
                break;
            case "Kushan_Missle_Destroyer":
                card = Kushan_Missle_Destroyer;
                break;
            case "Kushan_Multigun_Corvette":
                card = Kushan_Multigun_Corvette;
                break;
            case "Kushan_Scout":
                card = Kushan_Scout;
                break;
            case "Kushan_Support_Frigate":
                card = Kushan_Support_Frigate;
                break;
            case "Kushan_Salvage_Corvette":
                card = Kushan_Salvage_Corvette;
                break;
            case "Kushan_Repair_Corvette":
                card = Kushan_Repair_Corvette;
                break;
            default:
                card = null;
                break;
        }
        return card;
    }

    public GameObject CreateCard(string card_name)
    {
        //Debug.Log("Trying to instantiate a " + card_name);
        GameObject card = GetOriginalCard(card_name);
        card = Instantiate(card,null);
        return card;
    }
}
