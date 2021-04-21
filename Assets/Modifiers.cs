using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
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
