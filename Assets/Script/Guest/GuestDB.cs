using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모험가 데이터 불러오기
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
    }

    public enum MoneyType
    {
        Gold,
        Crown,
        Sealing,
        Deek,
    }

    private static Dictionary<SpeciesType, string[]> localDictionary = new Dictionary<SpeciesType, string[]>();   // 종족에 따른 지역
    private static Dictionary<string, string[]> partyDictionary = new Dictionary<string, string[]>();   // 지역에 따른 세력
    private static Dictionary<string, ProfessionType[]> professionDictionary = new Dictionary<string, ProfessionType[]>();   // 지역에 따른 직업
    private static Dictionary<string, ProfessionType[]> nonProfessionDictionary = new Dictionary<string, ProfessionType[]>();   // 지역에 따라 없는 직업

    [SerializeField] private TextAsset csvFile1 = null; // 종족-지역-세력 데이터
    [SerializeField] private TextAsset csvFile2 = null; // 지역-직업 데이터

    private void Awake()
    {
        SetGuestData();
        SetProfessionData();
    }

    // 종족 랜덤 리턴
    public static SpeciesType GetRandomSpecies()
    {
        int count = System.Enum.GetValues(typeof(SpeciesType)).Length;
        SpeciesType species = (SpeciesType)Random.Range(0, count);

        return species;
    }

    // 종족에 따른 지역 리스트 리턴
    public static string[] GetLocalList(SpeciesType species)
    {
        return localDictionary[species];
    }

    // 종족에 따른 지역 랜덤 리턴
    public static string GetRandomLocal(SpeciesType species)
    {
        string[] localList = GetLocalList(species);
        return localList[Random.Range(0, localList.Length)];
    }

    // 지역에 따른 세력 리스트 리턴
    public static string[] GetPartyList(string local)
    {
        return partyDictionary[local];
    }

    // 지역에 따른 세력 랜덤 리턴
    public static string GetRandomParty(string local)
    {
        string[] partyList = GetPartyList(local);
        return partyList[Random.Range(0, partyList.Length)];
    }

    // 해당 지역의 직업 리스트 리턴
    public static ProfessionType[] GetProfessionList(string local)
    {
        return professionDictionary[local];
    }

    // 지역에 따른 올바른 직업 랜덤 리턴
    public static ProfessionType GetRandomProfession(string local)
    {
        ProfessionType[] professionList = GetProfessionList(local);
        return professionList[Random.Range(0, professionList.Length)];
    }

    // 지역에 없는 틀린 직업 랜덤 리턴
    public static ProfessionType GetWrongRandomProfession(string local)
    {
        ProfessionType[] wrongProfessionList = nonProfessionDictionary[local];
        if (wrongProfessionList.Length == 0)
            return (ProfessionType) (-1);
        else
            return wrongProfessionList[Random.Range(0, wrongProfessionList.Length)];
    }

    // csvFile2(GuestDataFile) 파싱
    public void SetGuestData()
    {
        // 아래 한 줄 빼기
        string csvText = csvFile1.text.Substring(0, csvFile1.text.Length - 1);
        // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
        string[] rows = csvText.Split(new char[] { '\n' });

        // 엑셀 파일 1번째 줄은 편의를 위한 분류이므로 i = 1부터 시작
        for (int i = 1; i < rows.Length; i++)
        {
            // A, B, C열을 쪼개서 배열에 담음
            string[] rowValues = rows[i].Split(new char[] { ',' });

            // 유효한 이벤트 이름이 나올때까지 반복
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end") continue;

            SpeciesType species = (SpeciesType) System.Enum.Parse(typeof(SpeciesType), rowValues[0]);
            string[] localDatas = GetLocalDatas(rows, ref i, rowValues);

            localDictionary.Add(species, localDatas);
        }
    }

    string[] GetLocalDatas(string[] rows, ref int i, string[] rowValues)
    {
        List<string> localList = new List<string>();

        while (rowValues[0].Trim() != "end") // localList 하나를 만드는 반복문
        {
            string local = rowValues[1];
            List<string> partyList = new List<string>();
            for (int j = 2; j < rowValues.Length; j++)
            {
                if (rowValues[j].Trim() == "") break;   // 빈칸인 경우
                partyList.Add(rowValues[j]);
            }

            localList.Add(local);
            partyDictionary.Add(local, partyList.ToArray());

            if (++i < rows.Length) rowValues =
                         rows[i].Split(new char[] { ',' });
            else break;
        }

        return localList.ToArray();
    }

    // csvFile2(ProfessionDataFile) 파싱
    public void SetProfessionData()
    {
        // 아래 한 줄 빼기
        string csvText = csvFile2.text.Substring(0, csvFile2.text.Length - 1);
        // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
        string[] rows = csvText.Split(new char[] { '\n' });

        // 엑셀 파일 2번째 줄부터 시작
        for (int i = 2; i < rows.Length; i++)
        {
            // A, B, C열을 쪼개서 배열에 담음
            string[] rowValues = rows[i].Split(new char[] { ',' });
            if (rowValues[0] == "") continue;
            SetProfessionDictionary(rowValues);
        }
    }

    // professionDictionary, nonProfessionDictionary 생성
    private void SetProfessionDictionary(string[] rowValues)
    {
        List<ProfessionType> professionList = new List<ProfessionType>();
        List<ProfessionType> nonProfessionList = new List<ProfessionType>();

        string local = rowValues[0];
        for (int i = 1; i < rowValues.Length; i++)
        {
            ProfessionType profession = (ProfessionType) (i - 1);
            switch (char.Parse(rowValues[i].Trim()))
            {
                case 'o':
                case 'O':
                    professionList.Add(profession);
                    break;
                case 'x':
                case 'X':
                    nonProfessionList.Add(profession);
                    break;
                default:
                    break;
            }
        }
        professionDictionary.Add(local, professionList.ToArray());
        nonProfessionDictionary.Add(local, nonProfessionList.ToArray());
    }
}
