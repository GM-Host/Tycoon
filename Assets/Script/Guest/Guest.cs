using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest
{
    private string guestName;
    private string guestLocal;
    private string guestParty;
    private GuestDB.SpeciesType guestSpecies;
    private GuestDB.ProfessionType guestProfession;
    private Sprite professionSeal;
    private int tier;
    private Sprite tierSeal;

    // »ý¼ºÀÚ
    public Guest(string _name, string _local, string _party, GuestDB.SpeciesType _species, GuestDB.ProfessionType _profession, Sprite _professionSeal, int _tier, Sprite _tierSeal)
    {
        guestName = _name;
        guestLocal = _local;
        guestParty = _party;
        guestSpecies = _species;
        guestProfession = _profession;
        professionSeal = _professionSeal;
        tier = _tier;
        tierSeal = _tierSeal;
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

    public Sprite GetProfessionSeal()
    {
        return professionSeal;
    }

    public int GetTier()
    {
        return tier;
    }

    public Sprite GetTierSeal()
    {
        return tierSeal;
    }
}
