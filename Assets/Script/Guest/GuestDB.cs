using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestDB : MonoBehaviour
{
    private List<string> nameList = new List<string>() { "�߶��", "�̾���Ʈ", "��ũ", "��������", "��ī" };

    // ����-����
    [SerializeField]
    private List<Sprite> professionSealList;    // ���� �̹��� ����Ʈ
    private Dictionary<GuestParse.ProfessionType, Sprite> professionToSeal = new Dictionary<GuestParse.ProfessionType, Sprite>();    // ������ ���� ���� �̹���

    // Ƽ��-��ǥ
    [SerializeField]
    private List<Sprite> tierSealList;    // ��ǥ �̹��� ����Ʈ


    private void Start()
    {
        for (int i = 0; i < professionSealList.Count; i++)
            professionToSeal.Add((GuestParse.ProfessionType) i, professionSealList[i]);
    }

    public Guest CreateGuest(bool correct)
    {
        int tier;
        string name, local, party;
        GuestParse.SpeciesType species;
        GuestParse.ProfessionType profession;
        Sprite professionSeal, tierSeal;

        // �̸� ����
        name = nameList[Random.Range(0, nameList.Count)];

        // ���� ����
        species = GuestParse.GetRandomSpecies();

        // ������ ���� ���� ����
        local = GuestParse.GetRandomLocal(species);

        // ������ ���� �ùٸ� ���� ����
        party = GuestParse.GetRandomParty(local);

        // ������ ���� �ùٸ� ���� ����
        profession = GuestParse.GetRandomProfession(local);

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

            // ������ �������� �ʴ� ������ ���ٸ� ������ ���� �ʴ� ���� ���� �Ұ���
            if (GuestParse.GetWrongRandomProfession(local) == (GuestParse.ProfessionType) (-1))
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
                    GuestParse.ProfessionType wrongProfession;
                    do
                    {
                        wrongProfession=GuestParse.GetRandomProfession(local);
                    } while (wrongProfession == profession);

                    // �ٸ� ������ �������� ����
                    professionSeal = professionToSeal[wrongProfession];

                    break;

                case 2: // Ƽ�� ��ǥ ����ġ
                    Debug.Log("Ƽ��=��ǥ");

                    // ���� Ƽ��� �ٸ� Ƽ�� ���ϱ�
                    int count = tierSealList.Count;
                    int wrongTier = Random.Range(0, count);
                    while (tier == wrongTier)
                        wrongTier = Random.Range(0, count);

                    // �ٸ� Ƽ���� ��ǥ�� ����
                    tierSeal = tierSealList[wrongTier];

                    break;

                case 3: // ���� ���� ����ġ
                    Debug.Log("����=����");

                    profession = GuestParse.GetWrongRandomProfession(local);    // �ش� ������ ���� ���� ����

                    break;

                default:
                    break;
            }
        }
        
        return new Guest(name, local, party, species, profession, professionSeal, ++tier, tierSeal);
    }
}
