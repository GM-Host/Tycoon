using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData : MonoBehaviour
{
    // 대화자와 대화 내용을 담는 strucure
    private struct Dialog
    {
        public string Type { get; set; }      // 대화자
        public string Data { get; set; }      // 대화 내용

        public Dialog(string _type, string _data)
        {
            this.Type = _type;
            this.Data = _data;
        }
    }

    private Dictionary<int, List<Dialog>> talkData;       // 대화 데이터를 저장하는 dictionary 변수

    void Awake()
    {
        // dictionary instance 생성
        talkData = new Dictionary<int, List<Dialog>>();
        GenerateData(talkData);
    }

    // talkData 생성
    void GenerateData(Dictionary<int, List<Dialog>> dialogData)
    {
        dialogData.Add(1001, new List<Dialog> { 
            new Dialog("나", "안녕하세요. 로슈 길드 입니다."),
            new Dialog("전사", "오! 신입인가? 신원서 승인 좀 빨리 해줘.")});
        dialogData.Add(1011, new List<Dialog> {
            new Dialog("나", "승인되셨습니다. 등불이 그대 곁에 함께하길.")});
        dialogData.Add(1021, new List<Dialog> {
            new Dialog("나", "소속과 설명이 다릅니다."),
            new Dialog("전사", "하하 신입이라 그런지 꼼꼼하구만…?!")});
    }

    // 필요한 TalkData를 return
    public Tuple<string, string> GetDialogData(int id, int index)
    {
        Debug.Log("TalkID : " + id);
        if (index == talkData[id].Count)       // talkIndex가 talkData[id]의 마지막 index + 1이면
            return Tuple.Create<string, string>(null, null);

        return Tuple.Create<string, string>(talkData[id][index].Type, talkData[id][index].Data);     // 필요한 문장을 id와 index를 통해 return
    }
}
