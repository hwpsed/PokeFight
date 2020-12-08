using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PokeInfo
{
    public string No { get; set; }
    public string Name { get; set; }
    public string Total { get; set; }
    public string Hp { get; set; }
    public string Attack { get; set; }
    public string Defense { get; set; }
    public string SpecialAttack { get; set; }
    public string SpecialDefense { get; set; }
    public string Speed { get; set; }
    public string Type { get; set; }
    public string Rank { get; set; }
    public string IsEvolved { get; set; }
    public string EvolveTo { get; set; }
    public string Tier { get; set; }
    public string Moves { get; set; }

    public PokeInfo(Dictionary<string, object> info)
    {
        No = info["No"].ToString();
        Name = info["Name"].ToString();
        Total = info["Total"].ToString();
        Hp = info["Hp"].ToString();
        Attack = info["Attack"].ToString();
        Defense = info["Defense"].ToString();
        SpecialAttack = info["SpecialAttack"].ToString();
        SpecialDefense = info["SpecialDefense"].ToString();
        Rank = info["Rank"].ToString();
        Type = info["Type"].ToString();
        Speed = info["Speed"].ToString();
        IsEvolved = info["IsEvolved"].ToString();
        EvolveTo = info["EvolveTo"].ToString();
        Tier = info["Tier"].ToString();
        Moves = info["Moves"].ToString();
    }

}
