using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountCharacteristics : MonoBehaviour
{

    private int NUMBER_OF_DECKS = 3;
    private int NUMBER_OF_FACTIONS = 2;
    //User Name
    public string playername;

    //3 Allowed Decks Max
    [SerializeField] private List<string> deck1;
    [SerializeField] private List<string> deck2;
    [SerializeField] private List<string> deck3;

    [SerializeField] private List<string>[] decks;

    //Collections
    [SerializeField] private List<string> Kushan_cards;
    [SerializeField] private List<string> Taiidan_cards;


    // Save File
    [SerializeField] private SaveFile save;

    // Account Persistence Across Screens
    [SerializeField] static AccountCharacteristics current;

    // Ensuring there is only ever one account active during a game
    private void Awake()
    {
        if (current != null)
        {
            Destroy(this.gameObject);
            return;
        }

        current = this;
        GameObject.DontDestroyOnLoad(this);

        decks = new List<string>[NUMBER_OF_DECKS];

        deck1 = new List<string>();
        decks[0] = deck1;
        deck2 = new List<string>();
        decks[1] = deck2;
        deck3 = new List<string>();
        decks[2] = deck3;

        Kushan_cards = new List<string>();
        Taiidan_cards = new List<string>();

        gameObject.name = "ActiveAccount";
    }

    private void OnDestroy()
    {
        Debug.Log("Account was destroyed.");
    }

    public void NewPlayer(string player_name, CQBCard.FactionType faction)
    {
        this.playername = player_name;
        CreateNewCollection();
        deck1 = GetStarterKushan();
        save = new SaveFile(playername, deck1, deck2, deck3, Kushan_cards, Taiidan_cards);
        CreateSaveFile(playername+"Save", save);
    }

    public void LoadExistingPlayer(string player_name)
    {
        playername = player_name;
        save = LoadFile(playername+"Save");
        deck1 = save.deck1;
        deck2 = save.deck2;
        deck3 = save.deck3;

        //Debug.Log("Entering Collection Parse");
        Kushan_cards = save.KushanCollection;
        //Debug.Log("Kushan");
       // PrintCollection(0);
        Taiidan_cards = save.TaiidanCollection;
        //Debug.Log("Taiidan");
       // PrintCollection(1);
       // Debug.Log("Loaded all data");
    }

    public void SetNewName(string s)
    {
        playername = s;
    }

    public string GetPlayerName()
    {
        return playername;
    }

    public void CreateSaveFile(string path, SaveFile account)
    {
        string json = JsonUtility.ToJson(account);

        File.WriteAllText(Application.dataPath + path + ".json", json);
        Debug.Log("Wrote " + json + "\n to a new file");
    }

    public void UpdateSaveFile()
    {
        save = new SaveFile(playername, deck1, deck2, deck3, Kushan_cards, Taiidan_cards);
        CreateSaveFile(playername + "Save", save);
    }

    public void SetDeck(int deck_num, List<string> list)
    {
        switch(deck_num)
        {
            case 1:
                deck1 = list;
                break;
            case 2:
                deck2 = list;
                break;
            case 3:
                deck3 = list;
                break;
            default:
                break;
        }
    }
    public SaveFile LoadFile(string path)
    {
        string loadedFile = File.ReadAllText(Application.dataPath + path + ".json");
        return JsonUtility.FromJson<SaveFile>(loadedFile);
    }

    private Dictionary<string, int> ParseListToDictionary(List<string> list)
    {
        Dictionary<string, int> localcollection = new Dictionary<string, int>();
        while (list.Count > 0)
        {
            bool found = true;
            int count = 1;
            int i = 0;
            int j = 0;
            string card = list[i];
            list.RemoveAt(i);
            while (found && j < list.Count)
            {
                if (card.CompareTo(list[j]) == 0)
                {
                    count++;
                    list.RemoveAt(j);
                }
                else
                {
                    j++;
                    found = false;
                }
            }
            localcollection.Add(card, count);
            //Debug.Log("Added " + count + " instances of " + card + " to the collection");
        }
        return localcollection;
    }

    public List<string> GetDeck(int val)
    {
        switch(val)
        {
            case 0:
                return deck1;
            case 1:
                return deck2;
            case 2:
                return deck3;
            default:
                return deck1;
        }
    }

    public List<string> GetFactionCollection(CQBCard.FactionType faction)
    {
        switch(faction)
        {
            case CQBCard.FactionType.KUSHAN:
                return GetKushan();
            case CQBCard.FactionType.TAIIDAN:
                return GetTaiidan();
            default:
                return null;
        }
    }

    public List<string> GetStarterKushan()
    {
        Random.InitState(12345);
        int index;
        string card;
        List<string> tempDeck = new List<string>();
        for(int i = 0; i <= 5; i++)
        {
            index = Random.Range(0, Kushan_cards.Count);
            card = Kushan_cards[index];
            tempDeck.Add(card);
        }
        return tempDeck;
    }

    public List<string> GetRandomTaiidan()
    {
        return Taiidan_cards;
    }

    public List<string> GetKushan()
    {
        return Kushan_cards;
    }

    public List<string> GetTaiidan()
    {
        return Taiidan_cards;
    }

    public void CreateNewCollection()
    {
        //Default Kushan Ships
        this.Kushan_cards.Add("Kushan_Assault_Frigate");
        this.Kushan_cards.Add("Kushan_Assault_Frigate");
        this.Kushan_cards.Add("Kushan_Bomber");
        this.Kushan_cards.Add("Kushan_Carrier");
        this.Kushan_cards.Add("Kushan_Decoy");
        this.Kushan_cards.Add("Kushan_Destroyer");
        this.Kushan_cards.Add("Kushan_Destroyer");
        this.Kushan_cards.Add("Kushan_Destroyer");
        this.Kushan_cards.Add("Kushan_Heavy_Corvette");
        this.Kushan_cards.Add("Kushan_Heavy_Corvette");
        this.Kushan_cards.Add("Kushan_Heavy_Corvette");
        this.Kushan_cards.Add("Kushan_Heavy_Corvette");
        this.Kushan_cards.Add("Kushan_Heavy_Cruiser");
        this.Kushan_cards.Add("Kushan_Heavy_Cruiser");
        this.Kushan_cards.Add("Kushan_Interceptor");
        this.Kushan_cards.Add("Kushan_Interceptor");
        this.Kushan_cards.Add("Kushan_Interceptor");
        this.Kushan_cards.Add("Kushan_Interceptor");
        this.Kushan_cards.Add("Kushan_Ion_Frigate");
        this.Kushan_cards.Add("Kushan_Ion_Frigate");
        this.Kushan_cards.Add("Kushan_Light_Corvette");
        this.Kushan_cards.Add("Kushan_Missle_Destroyer");
        this.Kushan_cards.Add("Kushan_Missle_Destroyer");
        this.Kushan_cards.Add("Kushan_Multigun_Corvette");
        this.Kushan_cards.Add("Kushan_Multigun_Corvette");
        this.Kushan_cards.Add("Kushan_Multigun_Corvette");
        this.Kushan_cards.Add("Kushan_Scout");
        this.Kushan_cards.Add("Kushan_Support_Frigate");
        this.Kushan_cards.Add("Kushan_Salvage_Corvette");
        this.Kushan_cards.Add("Kushan_Repair_Corvette");

        //Default Taiidan Ships
        this.Taiidan_cards.Add("Taiidan_Assault_Frigate");
        this.Taiidan_cards.Add("Taiidan_Attack_Bomber");
        this.Taiidan_cards.Add("Taiidan_Carrier");
        this.Taiidan_cards.Add("Taiidan_Decoy");
        this.Taiidan_cards.Add("Taiidan_Destroyer");
        this.Taiidan_cards.Add("Taiidan_Heavy_Corvette");
        this.Taiidan_cards.Add("Taiidan_Heavy_Cruiser");
        this.Taiidan_cards.Add("Taiidan_Interceptor");
        this.Taiidan_cards.Add("Taiidan_Ion_Frigate");
        this.Taiidan_cards.Add("Taiidan_Light_Corvette");
        this.Taiidan_cards.Add("Taiidan_Missle_Destroyer");
        this.Taiidan_cards.Add("Taiidan_Multigun_Corvette");
        this.Taiidan_cards.Add("Taiidan_Scout_Fighter");
    }
    public List<string> GetFactionCards(CQBCard.FactionType faction)
    {
        switch (faction)
        {
            case CQBCard.FactionType.KUSHAN:
                return GetKushan();
            case CQBCard.FactionType.TAIIDAN:
                return GetTaiidan();
            default:
                return null;
        }
    }

    public void PrintCollection(int collection_num)
    {
        List<string> collection;
        switch(collection_num)
        {
            case 0:
                collection = Kushan_cards;
                break;
            case 1:
                collection = Taiidan_cards;
                break;
            default:
                collection = null;
                break;
        }
        if(collection != null)
        {
            foreach (var card in collection)
            {
                Debug.Log(card);
            }
        }
    }
}
