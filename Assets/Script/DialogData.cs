using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData : MonoBehaviour
{
    // ��ȭ�ڿ� ��ȭ ������ ��� strucure
    private struct Dialog
    {
        public string Type { get; set; }      // ��ȭ��
        public string Data { get; set; }      // ��ȭ ����

        public Dialog(string _type, string _data)
        {
            this.Type = _type;
            this.Data = _data;
        }
    }

    // ��ȭ �����͸� �����ϴ� dictionary ����
    private Dictionary<int, List<Dialog>> guideData;
    private Dictionary<int, List<Dialog>> permissionData;
    private Dictionary<int, List<Dialog>> refuseData;

    // dictionary instance ����
    void Awake()
    {
        guideData = new Dictionary<int, List<Dialog>>();
        GenerateGuideData(guideData);

        permissionData = new Dictionary<int, List<Dialog>>();
        GeneratePermissionData(permissionData);

        refuseData = new Dictionary<int, List<Dialog>>();
        GenerateRefuseData(refuseData);
    }

    // guide data ����
    void GenerateGuideData(Dictionary<int, List<Dialog>> data)
    {
        data.Add(0, new List<Dialog> { 
            new Dialog("��", "�ȳ��ϼ���. �ν� ��� �Դϴ�."),
            new Dialog("����", "��! �����ΰ�? �ſ��� ���� �� ���� ����.")});
        data.Add(1, new List<Dialog> {
            new Dialog("��", "�������. �ν� ��� �Դϴ�."),
            new Dialog("�ϻ���", "��. (��ǥ������ ���� ���� �����Ѵ�.)"),
            new Dialog("�ϻ���", "�ſ��� ���Ρ�")});
        data.Add(2, new List<Dialog> {
            new Dialog("������", "�������� ��ȭ�ϱ� ���� �����׿�~")});
        data.Add(3, new List<Dialog> {
            new Dialog("��������", "���� �뷡�� ���Ƿ���?!")});
        data.Add(4, new List<Dialog> {
            new Dialog("����", "��ü�� ����� �״�� �Բ��ϱ�.")});
        data.Add(5, new List<Dialog> {
            new Dialog("��������", "������ ������ ���� ������� ��� �־��.")});
        data.Add(6, new List<Dialog> {
            new Dialog("��ɲ�", "�� �ǹٶ��� �Ұڱ�.")});
    }

    // permission data ����
    void GeneratePermissionData(Dictionary<int, List<Dialog>> data)
    {
        data.Add(0, new List<Dialog> {
            new Dialog("��", "���εǼ̽��ϴ�. ����� �״� �翡 �Բ��ϱ�.")});
        data.Add(1, new List<Dialog> {
            new Dialog("��", "���εǾ����ϴ�."),
            new Dialog("�ϻ���", "�� (���� �����̸� �ſ����� �޾ư���.)"),
            new Dialog("��", "(�ϻ��ڵ��� �ϳ� ���� ��ħ�� ������ �ִٴϱ�...)")});
        data.Add(2, new List<Dialog> {
            new Dialog("��", "���εǼ̽��ϴ�. ����� �״� �翡 �Բ��ϱ�.")});
        data.Add(3, new List<Dialog> {
            new Dialog("��", "���εǾ����ϴ�."),
            new Dialog("��������", "��ſ� ������ �� �� ������~")});
        data.Add(4, new List<Dialog> {
            new Dialog("��", "���εǼ̽��ϴ�. ����� �״� �翡 �Բ��ϱ�.")});
        data.Add(5, new List<Dialog> {
            new Dialog("��", "���εǼ̽��ϴ�. ����� �״� �翡 �Բ��ϱ�."),
            new Dialog("��������", "����� �ð��� �ٰ����� �־��.")});
        data.Add(6, new List<Dialog> {
            new Dialog("��", "���εǾ����ϴ�."),
            new Dialog("��ɲ�", "�̰����� ���� �����°� ���� �ž�.")});
    }

    // refuse data ����
    void GenerateRefuseData(Dictionary<int, List<Dialog>> data)
    {
        data.Add(0, new List<Dialog> {
            new Dialog("��", "�ҼӰ� ������ �ٸ��ϴ�."),
            new Dialog("����", "���� �����̶� �׷��� �Ĳ��ϱ���...?!")});
        data.Add(1, new List<Dialog> {
            new Dialog("��", "�ҼӰ� ������ �ٸ��ϴ�."),
            new Dialog("�ϻ���", "�� (�ƹ� �� ���� ���Դ� ������ ���Ѵ�.)"),
            new Dialog("��", "(�ϻ��ڵ��� �ϳ� ���� ��ħ�� ������ �ִٴϱ�...)")});
        data.Add(2, new List<Dialog> {
            new Dialog("��", "�ҼӰ� ������ �ٸ��ϴ�."),
            new Dialog("������", "��...�������� ������ ��ȭ�ϵ��� ����.")});
        data.Add(3, new List<Dialog> {
            new Dialog("��", "�ҼӰ� ������ �ٸ��ϴ�."),
            new Dialog("��������", "�̷��̷� ��ġ�� �����ñ���~")});
        data.Add(4, new List<Dialog> {
            new Dialog("��", "�ҼӰ� ������ �ٸ��ϴ�."),
            new Dialog("����", "����� ���� ������ �ʳ� ������.")});
        data.Add(5, new List<Dialog> {
            new Dialog("��", "�ҼӰ� ������ �ٸ��ϴ�."),
            new Dialog("��������", "����� ���� �ź��ϳ׿�.")});
        data.Add(6, new List<Dialog> {
            new Dialog("��", "�ҼӰ� ������ �ٸ��ϴ�."),
            new Dialog("��ɲ�", "ĩ. �̹� ����� �۷���.")});
    }

    // �ʿ��� guideData�� return
    public Tuple<string, string> GetGuideDialogData(int id, int index)
    {
        Debug.Log("TalkID : " + id);
        if (index == guideData[id].Count)       // index�� guideData[id]�� ������ index + 1�̸�
            return Tuple.Create<string, string>(null, null);

        return Tuple.Create<string, string>(guideData[id][index].Type, guideData[id][index].Data);     // �ʿ��� ������ id�� index�� ���� return
    }

    // �ʿ��� permissionData�� return
    public Tuple<string, string> GetPermissionDialogData(int id, int index)
    {
        Debug.Log("TalkID : " + id);
        if (index == permissionData[id].Count)       // index�� permissionData[id]�� ������ index + 1�̸�
            return Tuple.Create<string, string>(null, null);

        return Tuple.Create<string, string>(permissionData[id][index].Type, permissionData[id][index].Data);     // �ʿ��� ������ id�� index�� ���� return
    }

    // �ʿ��� refuseData�� return
    public Tuple<string, string> GetRefuseDialogData(int id, int index)
    {
        Debug.Log("TalkID : " + id);
        if (index == refuseData[id].Count)       // index�� refuseData[id]�� ������ index + 1�̸�
            return Tuple.Create<string, string>(null, null);

        return Tuple.Create<string, string>(refuseData[id][index].Type, refuseData[id][index].Data);     // �ʿ��� ������ id�� index�� ���� return
    }
}
