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

    private Dictionary<int, List<Dialog>> talkData;       // ��ȭ �����͸� �����ϴ� dictionary ����

    void Awake()
    {
        // dictionary instance ����
        talkData = new Dictionary<int, List<Dialog>>();
        GenerateData(talkData);
    }

    // talkData ����
    void GenerateData(Dictionary<int, List<Dialog>> dialogData)
    {
        dialogData.Add(1000, new List<Dialog> { 
            new Dialog("��", "�ȳ��ϼ���. �ν� ��� �Դϴ�."),
            new Dialog("����", "��! �����ΰ�? �ſ��� ���� �� ���� ����.")});
        dialogData.Add(1001, new List<Dialog> {
            new Dialog("��", "�������. �ν� ��� �Դϴ�."),
            new Dialog("�ϻ���", "��. (��ǥ������ ���� ���� �����Ѵ�.)"),
            new Dialog("�ϻ���", "�ſ��� ���Ρ�")});
        dialogData.Add(1002, new List<Dialog> {
            new Dialog("������", "�������� ��ȭ�ϱ� ���� �����׿�~")});
        dialogData.Add(1003, new List<Dialog> {
            new Dialog("��������", "���� �뷡�� ���Ƿ���?!")});
        dialogData.Add(1004, new List<Dialog> {
            new Dialog("����", "��ü�� ����� �״�� �Բ��ϱ�.")});
        dialogData.Add(1005, new List<Dialog> {
            new Dialog("��������", "������ ������ ���� ������� ��� �־��.")});
        dialogData.Add(1006, new List<Dialog> {
            new Dialog("��ɲ�", "�� �ǹٶ��� �Ұڱ�.")});

        /*
        dialogData.Add(1010, new List<Dialog> {
            new Dialog("��", "���εǼ̽��ϴ�. ����� �״� �翡 �Բ��ϱ�.")});
        dialogData.Add(1011, new List<Dialog> {
            new Dialog("��", "���εǾ����ϴ�."),
            new Dialog("�ϻ���", "�� (���� �����̸� �ſ����� �޾ư���.)"),
            new Dialog("��", "(�ϻ��ڵ��� �ϳ� ���� ��ħ�� ������ �ִٴϱ�...)")});

        dialogData.Add(1020, new List<Dialog> {
            new Dialog("��", "�ҼӰ� ������ �ٸ��ϴ�."),
            new Dialog("����", "���� �����̶� �׷��� �Ĳ��ϱ�����?!")});
        */
    }

    // �ʿ��� TalkData�� return
    public Tuple<string, string> GetDialogData(int id, int index)
    {
        Debug.Log("TalkID : " + id);
        if (index == talkData[id].Count)       // talkIndex�� talkData[id]�� ������ index + 1�̸�
            return Tuple.Create<string, string>(null, null);

        return Tuple.Create<string, string>(talkData[id][index].Type, talkData[id][index].Data);     // �ʿ��� ������ id�� index�� ���� return
    }
}
