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

    private static List<string> nameList = new List<string>() { "��ī", "�̸�2", "�̸�3" };
    private static Dictionary<SpeciesType, List<string>> speciesToParty = new Dictionary<SpeciesType, List<string>>()
    {
        { SpeciesType.Human, new List<string>() { "���ο丣��", "��������", "��ö����", "���μ���", "õ����ȿ", "���߹ٴ�", "����", "�뷡�ϴ� ���", "�ȴ� �絿��", "���ϴ� ������", "�����޸� ��", "������", "�¾� ����", "�� ����", "���� ����", "�Ƹ����� �Ƚ�", "���긮��"}},
        { SpeciesType.Dwarf, new List<string>() {"ī���˱�", "��ġ", "���", "����", "����"} },
        { SpeciesType.Elf, new List<string>() {"�������", "�θ�", "���", "����", "�ٻ��", "��Ŭ����", "��", "�����", "�û���", "��"} },
        { SpeciesType.Beastface, new List<string>() {"temp" } },
        { SpeciesType.Oak, new List<string>() {"temp" } },
        { SpeciesType.Goblin, new List<string>() {"temp" } },
        { SpeciesType.Reve, new List<string>() { "���ο丣��", "��������", "��ö����", "���μ���", "õ����ȿ", "���߹ٴ�", "����", "�뷡�ϴ� ���", "�ȴ� �絿��", "���ϴ� ������", "�����޸� ��", "������", "�¾� ����", "�� ����", "���� ����", "�Ƹ����� �Ƚ�", "���긮��", "ī���˱�", "��ġ", "���", "����", "����", "�������", "�θ�", "���", "����", "�ٻ��", "��Ŭ����", "��", "�����", "�û���", "��" } }
    };

    private static List<string> speciesText = new List<string>() { "�ΰ�", "�����", "����", "��Ʈ���̽�", "��ũ", "���" };
    private static List<string> professionText = new List<string>() { "����", "�ϻ���", "������", "��������", "����", "��������", "��ɲ�" };

    

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
