using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveFile 
{
    public string playername;
    public List<string> deck1;
    public List<string> deck2;
    public List<string> deck3;
    public List<string> KushanCollection;
    public List<string> TaiidanCollection;

    public SaveFile(string name, List<string> deck1, List<string>deck2, List<string>deck3, List<string> kushan, List<string> taiidan)
    {
        this.playername = name;
        this.deck1 = deck1;
        this.deck2 = deck2;
        this.deck3 = deck3;
        KushanCollection = kushan;
        TaiidanCollection = taiidan;
    }
    


    private List<string> ParseDictionaryToList(Dictionary<string,int> collectionDictionary)
    {
        List<string> collection = new List<string>();
        foreach(var card in collectionDictionary)
        {
            for(int i = 0; i < card.Value; i++)
            {
                collection.Add(card.Key);
            }
        }
        return collection;
    }
}
