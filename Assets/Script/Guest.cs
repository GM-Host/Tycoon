using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    private string guestName;
    private string guestLocal;
    private string guestParty;
    private GuestDB.SpeciesType guestSpecies;
    private GuestDB.ProfessionType guestProfession;

    // »ý¼ºÀÚ
    public Guest(string _name, string _local, string _party, GuestDB.SpeciesType _species, GuestDB.ProfessionType _profession)
    {
        guestName = _name;
        guestLocal = _local;
        guestParty = _party;
        guestSpecies = _species;
        guestProfession = _profession;
    }

    public string GetName()
    {
        return guestName;
    }

    public string GetLocal()
    {
        return guestLocal;
    }

    public string GetParty()
    {
        return guestParty;
    }

    public GuestDB.SpeciesType GetSpecies()
    {
        return guestSpecies;
    }

    public GuestDB.ProfessionType GetProfession()
    {
        return guestProfession;
    }
}
