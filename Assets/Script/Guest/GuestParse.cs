using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모험가 데이터 불러오기
public class GuestParse : MonoBehaviour
{
    private static string[] speciesList;    // 종족 리스트
    private static Dictionary<string, string[]> localDictionary = new Dictionary<string, string[]>();   // 종족에 따른 지역
    private static Dictionary<string, string[]> partyDictionary = new Dictionary<string, string[]>();   // 지역에 따른 세력

    [SerializeField] private TextAsset csvFile = null;

    private void Awake()
    {
        SetGuestData();
    }

    public static string[] GetSpeciesList()
    {
        return speciesList;
    }

    public static string GetRandomSpecies()
    {
        return speciesList[Random.Range(0, speciesList.Length)];
    }

    public static string[] GetLocalList(string species)
    {
        return localDictionary[species];
    }

    public static string GetRandomLocal(string species)
    {
        string[] localList = GetLocalList(species);
        return localList[Random.Range(0, localList.Length)];
    }

    public static string[] GetPartyList(string local)
    {
        return partyDictionary[local];
    }

    public static string GetRandomParty(string local)
    {
        string[] partyList = GetPartyList(local);
        return partyList[Random.Range(0, partyList.Length)];
    }

    public void SetGuestData()
    {
        List<string> _speciesList = new List<string>();

        // 아래 한 줄 빼기
        string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1);
        // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
        string[] rows = csvText.Split(new char[] { '\n' });

        // 엑셀 파일 1번째 줄은 편의를 위한 분류이므로 i = 1부터 시작
        for (int i = 1; i < rows.Length; i++)
        {
            // A, B, C열을 쪼개서 배열에 담음
            string[] rowValues = rows[i].Split(new char[] { ',' });

            // 유효한 이벤트 이름이 나올때까지 반복
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end") continue;

            string species = rowValues[0];
            string[] localDatas = GetLocalDatas(rows, ref i, rowValues);

            _speciesList.Add(species);
            localDictionary.Add(species, localDatas);
        }

        speciesList = _speciesList.ToArray();
    }

    string[] GetLocalDatas(string[] rows, ref int i, string[] rowValues)
    {
        List<string> localList = new List<string>();

        while (rowValues[0].Trim() != "end") // localList 하나를 만드는 반복문
        {
            string local = rowValues[1];
            List<string> partyList = new List<string>();
            for (int j = 2; j < rowValues.Length; j++)
                partyList.Add(rowValues[j]);

            localList.Add(local);
            partyDictionary.Add(local, partyList.ToArray());

            if (++i < rows.Length) rowValues =
                         rows[i].Split(new char[] { ',' });
            else break;
        }

        return localList.ToArray();
    }
}
