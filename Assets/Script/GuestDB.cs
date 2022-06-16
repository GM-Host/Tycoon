using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GuestDB
{
    public enum ProfessionType
    {
        Warrior,    // 전사
        Assassin,   // 암살자
        Mage,       // 마법사
        Bard,       // 음유시인
        Priest,     // 사제
        Astrologian,// 점성술사
        Hunter,     // 사냥꾼
    }

    public enum SpeciesType
    {
        Human,
        Dwarf,
        Elf,
        Beastface,
        Oak,
        Goblin,
        Reve,
    }

    public enum MoneyType
    {
        Gold,
        Crown,
        Sealing,
        Deek,
    }

    private static List<string> nameList = new List<string>() { "샤카", "이름2", "이름3" };
    private static Dictionary<SpeciesType, List<string>> speciesToParty = new Dictionary<SpeciesType, List<string>>()
    {
        { SpeciesType.Human, new List<string>() { "아인요르드", "서리갈기", "강철발톱", "거인숨결", "천둥포효", "눈발바닥", "람다", "노래하는 사과", "걷는 양동이", "일하는 설계자", "날개달린 소", "에르페", "태양 교단", "달 교단", "제국 기사단", "아르웬의 안식", "오브리제"}},
        { SpeciesType.Dwarf, new List<string>() {"카리알굴", "망치", "모루", "쐐기", "광산"} },
        { SpeciesType.Elf, new List<string>() {"사몰레아", "부리", "기둥", "가지", "잎사귀", "오클리드", "개", "고양이", "올빼미", "매"} },
        { SpeciesType.Beastface, new List<string>() {"temp" } },
        { SpeciesType.Oak, new List<string>() {"temp" } },
        { SpeciesType.Goblin, new List<string>() {"temp" } },
        { SpeciesType.Reve, new List<string>() { "아인요르드", "서리갈기", "강철발톱", "거인숨결", "천둥포효", "눈발바닥", "람다", "노래하는 사과", "걷는 양동이", "일하는 설계자", "날개달린 소", "에르페", "태양 교단", "달 교단", "제국 기사단", "아르웬의 안식", "오브리제", "카리알굴", "망치", "모루", "쐐기", "광산", "사몰레아", "부리", "기둥", "가지", "잎사귀", "오클리드", "개", "고양이", "올빼미", "매" } }
    };

    private static List<string> speciesText = new List<string>() { "인간", "드워프", "엘프", "비스트페이스", "오크", "고블린" };
    private static List<string> professionText = new List<string>() { "전사", "암살자", "마법사", "음유시인", "사제", "점성술사", "사냥꾼" };

    

    public static Guest CreateGuest(bool correct)
    {
        string name, party;
        SpeciesType species;
        ProfessionType profession;



        name = nameList[Random.Range(0, nameList.Count)];

        int count = System.Enum.GetValues(typeof(ProfessionType)).Length;
        profession = (ProfessionType)Random.Range(0, count);

        count = System.Enum.GetValues(typeof(SpeciesType)).Length;
        species = (SpeciesType)Random.Range(0, count);

        if (correct)
        {
            List<string> partyList = speciesToParty[species];
            party = partyList[Random.Range(0, partyList.Count)];
        }
        else
        {
            SpeciesType wrongSpecies = (SpeciesType)Random.Range(0, count);
            while (wrongSpecies==species)
            {
                wrongSpecies = (SpeciesType)Random.Range(0, count);
            }

            List<string> wrongPartyList = speciesToParty[wrongSpecies];
            party = wrongPartyList[Random.Range(0, wrongPartyList.Count)];
        }
        return new Guest(name, party, species, profession);
    }

    public static string GetSpeciesText(SpeciesType species)
    {
        return speciesText[(int)species];
    }

    public static string GetProfessiosText(ProfessionType profession)
    {
        return professionText[(int)profession];
    }
}
