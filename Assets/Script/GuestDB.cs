using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestDB : MonoBehaviour
{
    public enum ProfessionType
    {
        Warrior,    // 전사
        Assassin,   // 암살자
        Mage,       // 마법사
        Bard,       // 음유시인
        Priest,     // 사제
        //Astrologian,// 점성술사
        Hunter,     // 사냥꾼
    }

    public enum SpeciesType
    {
        Human,
        Dwarf,
        Elf,
        //Beastface,
        //Oak,
        //Goblin,
        Reve,
    }

    public enum MoneyType
    {
        Gold,
        Crown,
        Sealing,
        Deek,
    }

    private List<string> nameList = new List<string>() { "발라드", "이안하트", "디르크", "무에르테", "샤카" };
    private List<string> allLocalList = new List<string>() { "아인요르드", "람다", "에르페", "오브리제", "카리알굴", "사몰레아", "오클리드" };
    private Dictionary<SpeciesType, List<string>> speciesToLocal = new Dictionary<SpeciesType, List<string>>()
    {
        { SpeciesType.Human, new List<string>() { "아인요르드", "람다", "에르페", "오브리제"}},
        { SpeciesType.Dwarf, new List<string>() {"카리알굴"} },
        { SpeciesType.Elf, new List<string>() {"사몰레아", "오클리드"} },
        //{ SpeciesType.Beastface, new List<string>() {"temp" } },
        //{ SpeciesType.Oak, new List<string>() {"temp" } },
        //{ SpeciesType.Goblin, new List<string>() {"temp" } },
        { SpeciesType.Reve, new List<string>() { "아인요르드", "람다", "에르페", "오브리제", "카리알굴", "사몰레아", "오클리드" } }
    };

    // 직업-인장
    [SerializeField]
    private List<Sprite> professionSealList;
    private Dictionary<ProfessionType, Sprite> professionToSeal;

    private Dictionary<string, List<string>> localToParty = new Dictionary<string, List<string>>()
    {
        { "아인요르드", new List<string>() { "서리갈기", "강철발톱", "거인숨결", "천둥포효", "눈발바닥"}},
        { "람다", new List<string>() { "노래하는 사과", "걷는 양동이", "일하는 설계자", "날개달린 소" } },
        { "에르페", new List<string>() { "태양 교단", "달 교단", "제국 기사단", "아르웬의 안식" } },
        { "오브리제", new List<string>() {"temp" } },
        { "카리알굴", new List<string>() { "망치", "모루", "쐐기", "광산" } },
        { "사몰레아", new List<string>() { "뿌리", "기둥", "가지", "잎사귀" } },
        { "오클리드", new List<string>() { "개", "고양이", "올빼미", "매" } },
        //{ "temp", new List<string>() { "temp" } }
    };

    private List<string> speciesText = new List<string>() { "인간", "드워프", "엘프", "레브" };
    //private static List<string> speciesText = new List<string>() { "인간", "드워프", "엘프", "비스트페이스", "오크", "고블린", "레브" };
    private List<string> professionText = new List<string>() { "전사", "암살자", "마법사", "음유시인", "사제", "사냥꾼" };
    //private List<string> professionText = new List<string>() { "전사", "암살자", "마법사", "음유시인", "사제", "점성술사", "사냥꾼" };

    private void Start()
    {
        // 직업-인장 dictionary 세팅
        professionToSeal = new Dictionary<ProfessionType, Sprite>();
        for (int i = 0; i < professionSealList.Count; i++)
            professionToSeal.Add((ProfessionType)i, professionSealList[i]);
    }

    public Guest CreateGuest(bool correct)
    {
        string name, local, party;
        SpeciesType species;
        ProfessionType profession;
        Sprite professionSeal;

        // 이름 랜덤
        name = nameList[Random.Range(0, nameList.Count)];

        // 직업 랜덤
        int count = System.Enum.GetValues(typeof(ProfessionType)).Length;
        profession = (ProfessionType)Random.Range(0, count);

        // 종족 랜덤
        count = System.Enum.GetValues(typeof(SpeciesType)).Length;
        species = (SpeciesType)Random.Range(0, count);

        // 종족에 따른 지역 설정
        List<string> localList = speciesToLocal[species];
        local = localList[Random.Range(0, localList.Count)];

        if (correct)    // 지역에 따른 올바른 세력, 직업에 따른 올바른 인장 설정
        {
            List<string> partyList = localToParty[local];
            party = partyList[Random.Range(0, partyList.Count)];

            professionSeal = professionToSeal[profession];
        }
        else    // 지역에 맞지 않는 세력, 직업에 맞지 않는 인장 설정
        {
            // 현재 지역과 다른 지역 정하기
            string wrongLocal = allLocalList[Random.Range(0, allLocalList.Count)];
            while (wrongLocal == local)
                wrongLocal = allLocalList[Random.Range(0, allLocalList.Count)];

            // 다른 지역의 세력 정하기
            List<string> wrongPartyList = localToParty[wrongLocal];
            party = wrongPartyList[Random.Range(0, wrongPartyList.Count)];

            //-------------------------------------------------------------------------

            // 현재 직업과 다른 직업 정하기
            count = System.Enum.GetValues(typeof(ProfessionType)).Length;
            ProfessionType wrongProfession = (ProfessionType)Random.Range(0, count);
            while (wrongProfession == profession)
                wrongProfession = (ProfessionType)Random.Range(0, count);

            // 다른 직업의 인장으로 설정
            professionSeal = professionToSeal[wrongProfession];
        }

        return new Guest(name, local, party, species, profession, professionSeal);
    }

    public string GetSpeciesText(SpeciesType species)
    {
        return speciesText[(int)species];
    }

    public string GetProfessiosText(ProfessionType profession)
    {
        return professionText[(int)profession];
    }
}
