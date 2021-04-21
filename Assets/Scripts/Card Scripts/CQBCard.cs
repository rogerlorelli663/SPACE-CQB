using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CQBCard : MonoBehaviour
{
    public enum CardType
    {
        NO_TYPE = 0,
        UNIT = 1,
        EFFECT = 2,
        LEADER = 3,
    }

    public enum UnitType
    {
        FIGHTER = 0,
        CORVETTE = 1,
        FRIGATE = 2,
        CAPITAL = 3,
        SUPPORT = 4,
    }

    public enum FactionType
    {
        KUSHAN = 0,
        TAIIDAN = 1,
        NEUTRAL = 2,
    }

    [SerializeField] private string cardName;
    [SerializeField] private int power;
    [SerializeField] private SP_CardPile.CardPileType fieldType;
    [SerializeField] private CardType cardType;
    [SerializeField] private UnitType unitType;
    [SerializeField] private FactionType factionType;
    [SerializeField] private GameObject Back;
    [SerializeField] private GameObject Playable;
    [SerializeField] private GameObject Manage;
    [SerializeField] private GameObject BasePower;
    [SerializeField] private GameObject PositivePower;
    [SerializeField] private GameObject NegativePower;
    [SerializeField] private Text PowerText;
    [SerializeField] private GameObject BaseSymbol;
    [SerializeField] private GameObject PositiveSymbol;
    [SerializeField] private GameObject NegativeSymbol;
    [SerializeField] private Modifiers.CardModifiers ability;
    [SerializeField] private bool is_enviro_modified;
    [SerializeField] private bool is_modified_buddy;
    [SerializeField] private bool is_modified_hunter;
    [SerializeField] private bool is_debuffed;

    private void Start()
    {
        is_enviro_modified = false;
        is_modified_buddy = false;
        is_modified_hunter = false;
        is_debuffed = false;
    }


    public virtual string GetCardName()
    {
        return this.cardName;
    }
    public virtual SP_CardPile.CardPileType GetCardType()
    {
        return this.fieldType;
    }

    public CQBCard.CardType GetType()
    {
        return this.cardType;
    }

    public UnitType GetUnitType()
    {
        return unitType;
    }

    public virtual int GetBasePower()
    {
        return power;
    }

    public void SetPower(int value)
    {
        PowerText.text = "" + value;
    }

    public FactionType GetFaction()
    {
        return this.factionType;
    }

    public void ActivatePlayable()
    {
        if (Back.activeSelf)
        {
            Back.SetActive(false);
        }
        if (!Playable.activeSelf)
        {
            Playable.SetActive(true);
        }
        if (Manage.activeSelf)
        {
            Manage.SetActive(false);
        }
    }

    public void ActivateManagement()
    {
        if (Back.activeSelf)
        {
            Back.SetActive(false);
        }
        if (Playable.activeSelf)
        {
            Playable.SetActive(false);
        }
        if (!Manage.activeSelf)
        {
            Manage.SetActive(true);
        }
    }

    public void ActivateBack()
    {
        if (!Back.activeSelf)
        {
            Back.SetActive(true);
        }
        if (Playable.activeSelf)
        {
            Playable.SetActive(false);
        }
        if (Manage.activeSelf)
        {
            Manage.SetActive(false);
        }
    }

    public void ActivateBaseCost()
    {
        is_enviro_modified = false;
        if(!BasePower.activeSelf)
        {
            BasePower.SetActive(true);
            PowerText.text = "" + power;
        }
        if(NegativePower.activeSelf)
        {
            NegativePower.SetActive(false);
        }
        if(PositivePower.activeSelf)
        {
            PositivePower.SetActive(false);
        }
    }

    public void ActivateNegativeCost(int modified_power)
    {
        if (BasePower.activeSelf)
        {
            BasePower.SetActive(false);
        }
        if (!NegativePower.activeSelf)
        {
            NegativePower.SetActive(true);
            PowerText.text = "" + modified_power;
        }
        if (PositivePower.activeSelf)
        {
            PositivePower.SetActive(false);
        }
    }
    public void ActivatePositiveCost(int modified_power)
    {
        is_enviro_modified = true;
        if (BasePower.activeSelf)
        {
            BasePower.SetActive(false);
        }
        if (NegativePower.activeSelf)
        {
            NegativePower.SetActive(false);
        }
        if (!PositivePower.activeSelf)
        {
            PositivePower.SetActive(true);
            PowerText.text = "" + modified_power;
        }
    }
    public void ActivateNegativeSymbol()
    {
        if (BaseSymbol != null && BaseSymbol != null && BaseSymbol.activeSelf)
        {
            BaseSymbol.SetActive(false);
        }
        if (PositiveSymbol != null && PositiveSymbol.activeSelf)
        {
            PositiveSymbol.SetActive(false);
        }
        if (NegativeSymbol != null && !NegativeSymbol.activeSelf)
        {
            NegativeSymbol.SetActive(true);
        }
    }

    public void ActivatePositiveSymbol()
    {
        if (BaseSymbol != null && BaseSymbol.activeSelf)
        {
            BaseSymbol.SetActive(false);
        }
        if (PositiveSymbol != null && !PositiveSymbol.activeSelf)
        {
            PositiveSymbol.SetActive(true);
        }
        if (NegativeSymbol != null && NegativeSymbol.activeSelf)
        {
            NegativeSymbol.SetActive(false);
        }
    }

    public void ActivateBaseSymbol()
    {
        if (BaseSymbol != null && !BaseSymbol.activeSelf)
        {
            BaseSymbol.SetActive(true);
        }
        if (PositiveSymbol != null && PositiveSymbol.activeSelf)
        {
            PositiveSymbol.SetActive(false);
        }
        if (NegativeSymbol != null && NegativeSymbol.activeSelf)
        {
            NegativeSymbol.SetActive(false);
        }
    }

    public void ActivateBase()
    {
        if (BaseSymbol != null)
        {
            ActivateBaseSymbol();
        }
        ActivateBaseCost();
        PowerText.text = "" + power;
    }

    public bool Is_Modified_Enviro()
    {
        return is_enviro_modified;
    }

    public void SetEnviro()
    {
        is_enviro_modified = true;
    }

    public void ResetEnviro()
    {
        is_enviro_modified = false;
    }

    public int GetCurrentPower()
    {
        return int.Parse(PowerText.text);
    }

    public bool HasAbility()
    {
        return ability != Modifiers.CardModifiers.None;
    }

    public void SetBuddy()
    {
        is_modified_buddy = true;
    }

    public void resetBuddy()
    {
        is_modified_buddy = false;
    }

    public bool IsBuddy()
    {
        return is_modified_buddy;
    }

    public void SetHunter()
    {
        is_modified_hunter = true;
    }

    public void resetHunter()
    {
        is_modified_hunter = false;
    }

    public bool IsHunter()
    {
        return is_modified_hunter;
    }

    public Modifiers.CardModifiers GetAbility()
    {
        return ability;
    }

    public bool Suppressed()
    {
        return NegativeSymbol.activeSelf;
    }

    public void SetDebuff()
    {
        is_debuffed = true;
    }

    public void ResetDebuff()
    {
        is_debuffed = false;
    }

    public bool GetDebuff()
    {
        return is_debuffed;
    }
}
