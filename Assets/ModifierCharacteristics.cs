using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierCharacteristics : MonoBehaviour
{
    [SerializeField] private Modifiers.EnviroModifier Modifier;
    [SerializeField] private GameObject Token;
    [SerializeField] private GameObject Descriptor;


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
            Debug.Log("Trying to deactivate Descriptor: " + Descriptor.name);
            Descriptor.SetActive(false);
        }
    }

    public Modifiers.EnviroModifier GetModifier()
    {
        return this.Modifier;
    }

    static public void Blackhole(GameObject card)
    {
        CQBCard.UnitType type = card.GetComponent<CQBCard>().GetUnitType();
        if (type == CQBCard.UnitType.CAPITAL || type == CQBCard.UnitType.FRIGATE)
        {
            Debug.Log("Applying Blackhole to " + card.name);
            int modifiedPower = card.GetComponent<CQBCard>().GetBasePower() / 2;
            card.GetComponent<CQBCard>().ActivateNegativeCost(modifiedPower);
        }
    }

    static public void SuperNova(GameObject card)
    {
        CQBCard.UnitType type = card.GetComponent<CQBCard>().GetUnitType();
        if (type == CQBCard.UnitType.FIGHTER || type == CQBCard.UnitType.CORVETTE)
        {
            Debug.Log("Applying Supernova to " + card.name);
            int modifiedPower = card.GetComponent<CQBCard>().GetBasePower() / 2;
            card.GetComponent<CQBCard>().ActivateNegativeCost(modifiedPower);
        }
    }
    static public void PowerCap(GameObject card)
    {
        Debug.Log("Applying PowerCap to " + card.name);
        int modifiedPower = card.GetComponent<CQBCard>().GetBasePower();
        if(modifiedPower > 5)
        {
            modifiedPower = 5;
            card.GetComponent<CQBCard>().ActivateNegativeCost(modifiedPower);
        }
    }
    static public void ECM(GameObject card)
    {
        if(card.GetComponent<CQBCard>().HasAbility())
        {
            card.GetComponent<CQBCard>().ActivateNegativeSymbol();
        }
    }

    static public void BattleBuddiesAssault(GameObject current_card, Player_Behavior combatant)
    {
        List<GameObject> playfield = combatant.GetPlayField().GetComponent<SP_CardPile>().GetCardsInCardPile();
        foreach(GameObject card in playfield)
        {
            CQBCard cardCharacteristics = card.GetComponent<CQBCard>();
            if (cardCharacteristics.GetAbility() == Modifiers.CardModifiers.BattleBuddiesIon && !cardCharacteristics.IsBuddy())
            {
                int IonIndex = card.transform.GetSiblingIndex() - 1;
                int IonSiblingIndex = card.transform.GetSiblingIndex();
                int AssaultIndex = current_card.transform.GetSiblingIndex() - 1;

                CQBCard currentCharacteristics = current_card.GetComponent<CQBCard>();
                GameObject IonSibling = combatant.GetHand().gameObject.transform.GetChild(IonSiblingIndex).gameObject;

                currentCharacteristics.ActivatePositiveSymbol();
                currentCharacteristics.ActivatePositiveCost(currentCharacteristics.GetBasePower() + 3);
                currentCharacteristics.SetBuddy();

                cardCharacteristics.ActivatePositiveSymbol();
                cardCharacteristics.ActivatePositiveCost(cardCharacteristics.GetBasePower() + 3);
                cardCharacteristics.SetBuddy();

                if(IonSiblingIndex != AssaultIndex)
                {
                    IonSiblingIndex = AssaultIndex;
                    AssaultIndex = IonIndex;
                    IonIndex = AssaultIndex + 1;

                    IonSibling.transform.SetSiblingIndex(IonSiblingIndex);
                }
                else
                {
                    AssaultIndex = IonIndex;
                    IonIndex = AssaultIndex + 1;
                }
                current_card.transform.SetSiblingIndex(AssaultIndex);
                card.transform.SetSiblingIndex(IonIndex);
            }
        }
    }

    static public void BattleBuddiesIon(GameObject current_card, Player_Behavior combatant)
    {
        List<GameObject> playfield = combatant.GetPlayField().GetComponent<SP_CardPile>().GetCardsInCardPile();
        foreach (GameObject card in playfield)
        {
            CQBCard cardCharacteristics = card.GetComponent<CQBCard>();
            if (cardCharacteristics.GetAbility() == Modifiers.CardModifiers.BattleBuddiesAssault && !cardCharacteristics.IsBuddy())
            {
                int AssaultIndex = card.transform.GetSiblingIndex();
                int AssaultSiblingIndex = card.transform.GetSiblingIndex() + 1;
                int IonIndex = current_card.transform.GetSiblingIndex();

                CQBCard currentCharacteristics = current_card.GetComponent<CQBCard>();
                GameObject IonSibling = combatant.GetHand().gameObject.transform.GetChild(AssaultSiblingIndex).gameObject;

                currentCharacteristics.ActivatePositiveSymbol();
                currentCharacteristics.ActivatePositiveCost(currentCharacteristics.GetBasePower() + 3);
                currentCharacteristics.SetBuddy();

                cardCharacteristics.ActivatePositiveSymbol();
                cardCharacteristics.ActivatePositiveCost(cardCharacteristics.GetBasePower() + 3);
                cardCharacteristics.SetBuddy();

                if (AssaultSiblingIndex != IonIndex)
                {
                    AssaultSiblingIndex = IonIndex;
                    IonIndex = AssaultIndex + 1;

                    IonSibling.transform.SetSiblingIndex(AssaultSiblingIndex);
                    current_card.transform.SetSiblingIndex(IonIndex);
                }
            }
        }
    }

    static public void Bomber(Player_Behavior opponent)
    {
        List<GameObject> opponentPlayfield = opponent.GetPlayField().GetComponent<SP_CardPile>().GetCardsInCardPile();
        foreach(GameObject card in opponentPlayfield)
        {
            CQBCard cardCharacteristics = card.GetComponent<CQBCard>();
            if (cardCharacteristics.GetUnitType() == CQBCard.UnitType.FRIGATE && !cardCharacteristics.GetDebuff())
            {
                cardCharacteristics.SetPower(cardCharacteristics.GetCurrentPower() / 2);
                cardCharacteristics.SetDebuff();
                return;
            }
        }
    }
}
