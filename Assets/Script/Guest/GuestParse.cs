using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���谡 ������ �ҷ�����
public class GuestParse : MonoBehaviour
{
    private static string[] speciesList;    // ���� ����Ʈ
    private static Dictionary<string, string[]> localDictionary = new Dictionary<string, string[]>();   // ������ ���� ����
    private static Dictionary<string, string[]> partyDictionary = new Dictionary<string, string[]>();   // ������ ���� ����

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

        // �Ʒ� �� �� ����
        string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1);
        // �ٹٲ�(�� ��)�� �������� csv ������ �ɰ��� string�迭�� �� ������� ����
        string[] rows = csvText.Split(new char[] { '\n' });

        // ���� ���� 1��° ���� ���Ǹ� ���� �з��̹Ƿ� i = 1���� ����
        for (int i = 1; i < rows.Length; i++)
        {
            // A, B, C���� �ɰ��� �迭�� ����
            string[] rowValues = rows[i].Split(new char[] { ',' });

            // ��ȿ�� �̺�Ʈ �̸��� ���ö����� �ݺ�
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

        while (rowValues[0].Trim() != "end") // localList �ϳ��� ����� �ݺ���
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
