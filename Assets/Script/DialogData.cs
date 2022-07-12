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
        dialogData.Add(1000, new List<Dialog> { 
            new Dialog("나", "안녕하세요. 로슈 길드 입니다."),
            new Dialog("전사", "오! 신입인가? 신원서 승인 좀 빨리 해줘.")});
        dialogData.Add(1001, new List<Dialog> {
            new Dialog("나", "어서오세요. 로슈 길드 입니다."),
            new Dialog("암살자", "…. (무표정으로 나의 얼굴을 응시한다.)"),
            new Dialog("암살자", "신원서 승인…")});
        dialogData.Add(1002, new List<Dialog> {
            new Dialog("마법사", "마물들을 정화하기 좋은 날씨네요~")});
        dialogData.Add(1003, new List<Dialog> {
            new Dialog("음유시인", "저의 노래를 들어보실래요?!")});
        dialogData.Add(1004, new List<Dialog> {
            new Dialog("사제", "루체의 등불이 그대와 함께하길.")});
        dialogData.Add(1005, new List<Dialog> {
            new Dialog("점성술사", "세상의 지혜는 전부 세계수에 담겨 있어요.")});
        dialogData.Add(1006, new List<Dialog> {
            new Dialog("사냥꾼", "곧 피바람이 불겠군.")});

        /*
        dialogData.Add(1010, new List<Dialog> {
            new Dialog("나", "승인되셨습니다. 등불이 그대 곁에 함께하길.")});
        dialogData.Add(1011, new List<Dialog> {
            new Dialog("나", "승인되었습니다."),
            new Dialog("암살자", "… (고개를 끄덕이며 신원서를 받아간다.)"),
            new Dialog("나", "(암살자들은 하나 같이 음침한 구석이 있다니까...)")});

        dialogData.Add(1020, new List<Dialog> {
            new Dialog("나", "소속과 설명이 다릅니다."),
            new Dialog("전사", "하하 신입이라 그런지 꼼꼼하구만…?!")});
        */
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
