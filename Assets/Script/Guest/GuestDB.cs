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

    // ������ �������� �ʴ� ���� ���
    private Dictionary<string, List<ProfessionType>> professionNotInLocal = new Dictionary<string, List<ProfessionType>>()
    {
        { "������", new List<ProfessionType>() { ProfessionType.Astrologian}},
        { "����", new List<ProfessionType>() {} },
        { "���ο丣��", new List<ProfessionType>() { ProfessionType.Priest} },
        { "�������", new List<ProfessionType>() { ProfessionType.Warrior, ProfessionType.Priest } },
        { "��Ŭ����", new List<ProfessionType>() { ProfessionType.Mage } },
        { "ī���˱�", new List<ProfessionType>() { ProfessionType.Assassin, ProfessionType.Hunter } },
    };

    // ����-����
    [SerializeField]
    private List<Sprite> professionSealList;    // ���� �̹��� ����Ʈ
    private Dictionary<ProfessionType, Sprite> professionToSeal;    // ������ ���� ���� �̹���

    // Ƽ��-��ǥ
    [SerializeField]
    private List<Sprite> tierSealList;    // ��ǥ �̹��� ����Ʈ
    
    private List<string> professionText = new List<string>() { "����", "�ϻ���", "������", "��������", "����", "��������", "��ɲ�" };


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
        string species;
        ProfessionType profession;
        Sprite professionSeal, tierSeal;
        int tier;

        // �̸� ����
        name = nameList[Random.Range(0, nameList.Count)];

        // ���� ����
        species = GuestParse.GetRandomSpecies();

        // ������ ���� ���� ����
        local = GuestParse.GetRandomLocal(species);

        // ������ ���� �ùٸ� ���� ����
        party = GuestParse.GetRandomParty(local);

        // ������ ���� �ùٸ� ���� ����
        int count = System.Enum.GetValues(typeof(ProfessionType)).Length;
        do
        {
            profession = (ProfessionType)Random.Range(0, count);
        } while (professionNotInLocal[local].Exists(x=>x==profession)); // ������ ���� �ʴ� �����̸� �ٽ� ����

        // ������ ���� �ùٸ� ���� ����
        professionSeal = professionToSeal[profession];

        // Ƽ�� ����
        tier = Random.Range(0, 3);

        // Ƽ� ���� �ùٸ� ��ǥ ����
        tierSeal = tierSealList[tier];

        // ������ ���� �ʴ� ���� or ������ ���� �ʴ� ���� or ������ ���� �ʴ� ���� ����
        if (!correct)
        {
            int wrongKind;
            if (professionNotInLocal[local].Count == 0) // ������ �������� �ʴ� ������ ���ٸ� ������ ���� �ʴ� ���� ���� �Ұ���
                wrongKind = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, 3);
            else
                wrongKind = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, 4);

            switch (wrongKind)
            {
                case 0: // ���� ���� ����ġ
                    Debug.Log("����=����");

                    // ���� ������ �ٸ� ���� ���ϱ�
                    string wrongLocal;
                    do
                    {
                        wrongLocal = GuestParse.GetRandomLocal(species);
                    } while (wrongLocal == local);

                    // �ٸ� ������ ���� ���ϱ�
                    party = GuestParse.GetRandomParty(wrongLocal);

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

                case 2: // Ƽ�� ��ǥ ����ġ
                    Debug.Log("Ƽ��=��ǥ");

                    // ���� Ƽ��� �ٸ� Ƽ�� ���ϱ�
                    count = tierSealList.Count;
                    int wrongTier = Random.Range(0, count);
                    while (tier == wrongTier)
                        wrongTier = Random.Range(0, count);

                    // �ٸ� Ƽ���� ��ǥ�� ����
                    tierSeal = tierSealList[wrongTier];

                    break;

                case 3: // ���� ���� ����ġ
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
        
        return new Guest(name, local, party, species, profession, professionSeal, ++tier, tierSeal);
    }

    public string GetProfessiosText(ProfessionType profession)
    {
        return professionText[(int)profession];
    }
}
