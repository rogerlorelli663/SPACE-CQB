using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    [SerializeField] private List<GameObject> Tokens;
    [SerializeField] private GameObject Blackhole_Description;
    [SerializeField] private GameObject NoCapital_Description;
    [SerializeField] private GameObject JunkYard_Description;
    [SerializeField] private GameObject Supernova_Description;
    [SerializeField] private GameObject PowerCap_Description;
    [SerializeField] private GameObject ECM_Description;
    public enum EnviroModifier
    {
        None = 0,
        NoCapital,  //Astroid field - kill off all capital ships played
        Blackhole,
        JunkYard,
        SuperNova,
        PowerCap,
        ECM //No card modifiers
    }

    public enum CardModifiers
    {
        None = 0,
        HunterPack,
        BattleBuddiesAssault,
        BattleBuddiesIon,
        Quick_Deploy,
        Bomber,
        Anti_Strike
    }

     


}
