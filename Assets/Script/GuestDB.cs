using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GuestDB
{
    public enum ProfessionType
    {
        Warrior,    // ����
        Assassin,   // �ϻ���
        Mage,       // ������
        Bard,       // ��������
        Priest,     // ����
        Astrologian,// ��������
        Hunter,     // ��ɲ�
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

    private static List<string> nameList = new List<string>() { "�߶��", "�̾���Ʈ", "��ũ", "��������", "��ī" };
    private static List<string> allLocalList = new List<string>() { "���ο丣��", "����", "������", "���긮��", "ī���˱�", "�������", "��Ŭ����" };
    private static Dictionary<SpeciesType, List<string>> speciesToLocal = new Dictionary<SpeciesType, List<string>>()
    {
        { SpeciesType.Human, new List<string>() { "���ο丣��", "����", "������", "���긮��"}},
        { SpeciesType.Dwarf, new List<string>() {"ī���˱�"} },
        { SpeciesType.Elf, new List<string>() {"�������", "��Ŭ����"} },
        //{ SpeciesType.Beastface, new List<string>() {"temp" } },
        //{ SpeciesType.Oak, new List<string>() {"temp" } },
        //{ SpeciesType.Goblin, new List<string>() {"temp" } },
        { SpeciesType.Reve, allLocalList }
    };

    private static Dictionary<string, List<string>> localToParty = new Dictionary<string, List<string>>()
    {
        { "���ο丣��", new List<string>() { "��������", "��ö����", "���μ���", "õ����ȿ", "���߹ٴ�"}},
        { "����", new List<string>() { "�뷡�ϴ� ���", "�ȴ� �絿��", "���ϴ� ������", "�����޸� ��" } },
        { "������", new List<string>() { "�¾� ����", "�� ����", "���� ����", "�Ƹ����� �Ƚ�" } },
        { "���긮��", new List<string>() {"temp" } },
        { "ī���˱�", new List<string>() { "��ġ", "���", "����", "����" } },
        { "�������", new List<string>() { "�Ѹ�", "���", "����", "�ٻ��" } },
        { "��Ŭ����", new List<string>() { "��", "�����", "�û���", "��" } },
        //{ "temp", new List<string>() { "temp" } }
    };

    private static List<string> speciesText = new List<string>() { "�ΰ�", "�����", "����", "����" };
    //private static List<string> speciesText = new List<string>() { "�ΰ�", "�����", "����", "��Ʈ���̽�", "��ũ", "���", "����" };
    private static List<string> professionText = new List<string>() { "����", "�ϻ���", "������", "��������", "����", "��������", "��ɲ�" };

    

    public static Guest CreateGuest(bool correct)
    {
        string name, local, party;
        SpeciesType species;
        ProfessionType profession;

        // �̸� ����
        name = nameList[Random.Range(0, nameList.Count)];

        // ���� ����
        int count = System.Enum.GetValues(typeof(ProfessionType)).Length;
        profession = (ProfessionType)Random.Range(0, count);

        // ���� ����
        count = System.Enum.GetValues(typeof(SpeciesType)).Length;
        species = (SpeciesType)Random.Range(0, count);

        // ������ ���� ���� ����
        List<string> localList = speciesToLocal[species];
        local = localList[Random.Range(0, localList.Count)];

        if (correct)    // ������ ���� �ùٸ� ���� ����
        {
            List<string> partyList = localToParty[local];
            party = partyList[Random.Range(0, partyList.Count)];
        }
        else    // ������ ���� �ʴ� ���� ����
        {
            // ���� ������ �ٸ� ���� ���ϱ�
            string wrongLocal = allLocalList[Random.Range(0, allLocalList.Count)];
            while (wrongLocal == local)
                wrongLocal = allLocalList[Random.Range(0, allLocalList.Count)];

            // �ٸ� ������ ���� ���ϱ�
            List<string> wrongPartyList = localToParty[wrongLocal];
            party = wrongPartyList[Random.Range(0, wrongPartyList.Count)];
        }

        return new Guest(name, local, party, species, profession);
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
