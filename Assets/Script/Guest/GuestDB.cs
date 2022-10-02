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
        Astrologian,// 점성술사
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

    // 지역에 존재하지 않는 직업 목록
    private Dictionary<string, List<ProfessionType>> professionNotInLocal = new Dictionary<string, List<ProfessionType>>()
    {
        { "에르페", new List<ProfessionType>() { ProfessionType.Astrologian}},
        { "람다", new List<ProfessionType>() {} },
        { "아인요르드", new List<ProfessionType>() { ProfessionType.Priest} },
        { "사몰레아", new List<ProfessionType>() { ProfessionType.Warrior, ProfessionType.Priest } },
        { "오클리드", new List<ProfessionType>() { ProfessionType.Mage } },
        { "카리알굴", new List<ProfessionType>() { ProfessionType.Assassin, ProfessionType.Hunter } },
    };

    // 직업-인장
    [SerializeField]
    private List<Sprite> professionSealList;    // 인장 이미지 리스트
    private Dictionary<ProfessionType, Sprite> professionToSeal;    // 직업에 따른 인장 이미지

    // 티어-증표
    [SerializeField]
    private List<Sprite> tierSealList;    // 증표 이미지 리스트
    
    private List<string> professionText = new List<string>() { "전사", "암살자", "마법사", "음유시인", "사제", "점성술사", "사냥꾼" };


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
        string species;
        ProfessionType profession;
        Sprite professionSeal, tierSeal;
        int tier;

        // 이름 랜덤
        name = nameList[Random.Range(0, nameList.Count)];

        // 종족 랜덤
        species = GuestParse.GetRandomSpecies();

        // 종족에 따른 지역 설정
        local = GuestParse.GetRandomLocal(species);

        // 지역에 따른 올바른 세력 설정
        party = GuestParse.GetRandomParty(local);

        // 지역에 따른 올바른 직업 설정
        int count = System.Enum.GetValues(typeof(ProfessionType)).Length;
        do
        {
            profession = (ProfessionType)Random.Range(0, count);
        } while (professionNotInLocal[local].Exists(x=>x==profession)); // 지역에 맞지 않는 직업이면 다시 설정

        // 직업에 따른 올바른 인장 설정
        professionSeal = professionToSeal[profession];

        // 티어 랜덤
        tier = Random.Range(0, 3);

        // 티어에 따른 올바른 증표 설정
        tierSeal = tierSealList[tier];

        // 지역에 맞지 않는 세력 or 직업에 맞지 않는 인장 or 지역에 맞지 않는 직업 설정
        if (!correct)
        {
            int wrongKind;
            if (professionNotInLocal[local].Count == 0) // 지역에 존재하지 않는 직업이 없다면 지역에 맞지 않는 직업 설정 불가능
                wrongKind = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, 3);
            else
                wrongKind = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, 4);

            switch (wrongKind)
            {
                case 0: // 지역 세력 불일치
                    Debug.Log("지역=세력");

                    // 현재 지역과 다른 지역 정하기
                    string wrongLocal;
                    do
                    {
                        wrongLocal = GuestParse.GetRandomLocal(species);
                    } while (wrongLocal == local);

                    // 다른 지역의 세력 정하기
                    party = GuestParse.GetRandomParty(wrongLocal);

                    break;

                case 1: // 직업 인장 불일치
                    Debug.Log("직업=인장");

                    // 현재 직업과 다른 직업 정하기
                    count = System.Enum.GetValues(typeof(ProfessionType)).Length;
                    ProfessionType wrongProfession = (ProfessionType)Random.Range(0, count);
                    while (wrongProfession == profession)
                        wrongProfession = (ProfessionType)Random.Range(0, count);

                    // 다른 직업의 인장으로 설정
                    professionSeal = professionToSeal[wrongProfession];

                    break;

                case 2: // 티어 증표 불일치
                    Debug.Log("티어=증표");

                    // 현재 티어와 다른 티어 정하기
                    count = tierSealList.Count;
                    int wrongTier = Random.Range(0, count);
                    while (tier == wrongTier)
                        wrongTier = Random.Range(0, count);

                    // 다른 티어의 증표로 설정
                    tierSeal = tierSealList[wrongTier];

                    break;

                case 3: // 지역 직업 불일치
                    Debug.Log("지역=직업");

                    count = professionNotInLocal[local].Count;
                    do
                    {
                        profession = professionNotInLocal[local][Random.Range(0, count)];
                    } while (!professionNotInLocal[local].Exists(x => x == profession)); // 지역에 맞지 않는 직업이 아니면 다시 설정
                    break;

                default:
                    break;
            }
        }
        
        return new Guest(name, local, party, species, profession, professionSeal, ++tier, tierSeal);
    }

    public string GetProfessiosText(ProfessionType profession)
    {
        return professionText[(int)profession];
    }
}
