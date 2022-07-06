using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestDB : MonoBehaviour
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

    private List<string> nameList = new List<string>() { "�߶��", "�̾���Ʈ", "��ũ", "��������", "��ī" };
    private List<string> allLocalList = new List<string>() { "���ο丣��", "����", "������", "���긮��", "ī���˱�", "�������", "��Ŭ����" };

    // ������ ���� ����
    private Dictionary<SpeciesType, List<string>> speciesToLocal = new Dictionary<SpeciesType, List<string>>()
    {
        { SpeciesType.Human, new List<string>() { "���ο丣��", "����", "������", "���긮��"}},
        { SpeciesType.Dwarf, new List<string>() {"ī���˱�"} },
        { SpeciesType.Elf, new List<string>() {"�������", "��Ŭ����"} },
        //{ SpeciesType.Beastface, new List<string>() {"temp" } },
        //{ SpeciesType.Oak, new List<string>() {"temp" } },
        //{ SpeciesType.Goblin, new List<string>() {"temp" } },
        { SpeciesType.Reve, new List<string>() { "���ο丣��", "����", "������", "���긮��", "ī���˱�", "�������", "��Ŭ����" } }
    };

    // ������ �������� �ʴ� ���� ���
    private Dictionary<string, List<ProfessionType>> professionNotInLocal = new Dictionary<string, List<ProfessionType>>()
    {
        { "������", new List<ProfessionType>() { ProfessionType.Astrologian}},
        { "����", new List<ProfessionType>() {} },
        { "���ο丣��", new List<ProfessionType>() { ProfessionType.Priest} },
        { "���긮��", new List<ProfessionType>() { ProfessionType.Priest } },
        { "�������", new List<ProfessionType>() { ProfessionType.Warrior, ProfessionType.Priest } },
        { "��Ŭ����", new List<ProfessionType>() { ProfessionType.Mage } },
        { "ī���˱�", new List<ProfessionType>() { ProfessionType.Assassin, ProfessionType.Hunter } },
    };

    // ����-����
    [SerializeField]
    private List<Sprite> professionSealList;    // ���� �̹��� ����Ʈ
    private Dictionary<ProfessionType, Sprite> professionToSeal;    // ������ ���� ���� �̹���

    private Dictionary<string, List<string>> localToParty = new Dictionary<string, List<string>>()
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

    private List<string> speciesText = new List<string>() { "�ΰ�", "�����", "����", "����" };
    //private static List<string> speciesText = new List<string>() { "�ΰ�", "�����", "����", "��Ʈ���̽�", "��ũ", "���", "����" };
    private List<string> professionText = new List<string>() { "����", "�ϻ���", "������", "��������", "����", "��ɲ�" };
    //private List<string> professionText = new List<string>() { "����", "�ϻ���", "������", "��������", "����", "��������", "��ɲ�" };

    private void Start()
    {
        // ����-���� dictionary ����
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

        // �̸� ����
        name = nameList[Random.Range(0, nameList.Count)];

        // ���� ����
        int count = System.Enum.GetValues(typeof(SpeciesType)).Length;
        species = (SpeciesType)Random.Range(0, count);

        // ������ ���� ���� ����
        List<string> localList = speciesToLocal[species];
        local = localList[Random.Range(0, localList.Count)];

        // ������ ���� �ùٸ� ���� ����
        List<string> partyList = localToParty[local];
        party = partyList[Random.Range(0, partyList.Count)];

        // ������ ���� �ùٸ� ���� ����
        count = System.Enum.GetValues(typeof(ProfessionType)).Length;
        do
        {
            profession = (ProfessionType)Random.Range(0, count);
        } while (professionNotInLocal[local].Exists(x=>x==profession)); // ������ ���� �ʴ� �����̸� �ٽ� ����

        // ������ ���� �ùٸ� ���� ����
        professionSeal = professionToSeal[profession];

        // ������ ���� �ʴ� ���� or ������ ���� �ʴ� ���� or ������ ���� �ʴ� ���� ����
        if (!correct)
        {
            int wrongKind;
            if (professionNotInLocal[local].Count == 0) // ������ �������� �ʴ� ������ ���ٸ� ������ ���� �ʴ� ���� ���� �Ұ���
                wrongKind = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, 2);
            else
                wrongKind = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, 3);

            switch (wrongKind)
            {
                case 0: // ���� ���� ����ġ
                    Debug.Log("����=����");

                    // ���� ������ �ٸ� ���� ���ϱ�
                    string wrongLocal = allLocalList[Random.Range(0, allLocalList.Count)];
                    while (wrongLocal == local)
                        wrongLocal = allLocalList[Random.Range(0, allLocalList.Count)];

                    // �ٸ� ������ ���� ���ϱ�
                    List<string> wrongPartyList = localToParty[wrongLocal];
                    party = wrongPartyList[Random.Range(0, wrongPartyList.Count)];

                    break;

                case 1: // ���� ���� ����ġ
                    Debug.Log("����=����");

                    // ���� ������ �ٸ� ���� ���ϱ�
                    count = System.Enum.GetValues(typeof(ProfessionType)).Length;
                    ProfessionType wrongProfession = (ProfessionType)Random.Range(0, count);
                    while (wrongProfession == profession)
                        wrongProfession = (ProfessionType)Random.Range(0, count);

                    // �ٸ� ������ �������� ����
                    professionSeal = professionToSeal[wrongProfession];

                    break;

                case 2: // ���� ���� ����ġ
                    Debug.Log("����=����");

                    count = professionNotInLocal[local].Count;
                    do
                    {
                        profession = professionNotInLocal[local][Random.Range(0, count)];
                    } while (!professionNotInLocal[local].Exists(x => x == profession)); // ������ ���� �ʴ� ������ �ƴϸ� �ٽ� ����
                    break;

                default:
                    break;
            }
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
