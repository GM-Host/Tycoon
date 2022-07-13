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

    // 대화 데이터를 저장하는 dictionary 변수
    private Dictionary<int, List<Dialog>> guideData;
    private Dictionary<int, List<Dialog>> permissionData;
    private Dictionary<int, List<Dialog>> refuseData;

    // dictionary instance 생성
    void Awake()
    {
        guideData = new Dictionary<int, List<Dialog>>();
        GenerateGuideData(guideData);

        permissionData = new Dictionary<int, List<Dialog>>();
        GeneratePermissionData(permissionData);

        refuseData = new Dictionary<int, List<Dialog>>();
        GenerateRefuseData(refuseData);
    }

    // guide data 생성
    void GenerateGuideData(Dictionary<int, List<Dialog>> data)
    {
        data.Add(0, new List<Dialog> { 
            new Dialog("나", "안녕하세요. 로슈 길드 입니다."),
            new Dialog("전사", "오! 신입인가? 신원서 승인 좀 빨리 해줘.")});
        data.Add(1, new List<Dialog> {
            new Dialog("나", "어서오세요. 로슈 길드 입니다."),
            new Dialog("암살자", "…. (무표정으로 나의 얼굴을 응시한다.)"),
            new Dialog("암살자", "신원서 승인…")});
        data.Add(2, new List<Dialog> {
            new Dialog("마법사", "마물들을 정화하기 좋은 날씨네요~")});
        data.Add(3, new List<Dialog> {
            new Dialog("음유시인", "저의 노래를 들어보실래요?!")});
        data.Add(4, new List<Dialog> {
            new Dialog("사제", "루체의 등불이 그대와 함께하길.")});
        data.Add(5, new List<Dialog> {
            new Dialog("점성술사", "세상의 지혜는 전부 세계수에 담겨 있어요.")});
        data.Add(6, new List<Dialog> {
            new Dialog("사냥꾼", "곧 피바람이 불겠군.")});
    }

    // permission data 생성
    void GeneratePermissionData(Dictionary<int, List<Dialog>> data)
    {
        data.Add(0, new List<Dialog> {
            new Dialog("나", "승인되셨습니다. 등불이 그대 곁에 함께하길.")});
        data.Add(1, new List<Dialog> {
            new Dialog("나", "승인되었습니다."),
            new Dialog("암살자", "… (고개를 끄덕이며 신원서를 받아간다.)"),
            new Dialog("나", "(암살자들은 하나 같이 음침한 구석이 있다니까...)")});
        data.Add(2, new List<Dialog> {
            new Dialog("나", "승인되셨습니다. 등불이 그대 곁에 함께하길.")});
        data.Add(3, new List<Dialog> {
            new Dialog("나", "승인되었습니다."),
            new Dialog("음유시인", "즐거운 여행이 될 것 같군요~")});
        data.Add(4, new List<Dialog> {
            new Dialog("나", "승인되셨습니다. 등불이 그대 곁에 함께하길.")});
        data.Add(5, new List<Dialog> {
            new Dialog("나", "승인되셨습니다. 등불이 그대 곁에 함께하길."),
            new Dialog("점성술사", "운명의 시간이 다가오고 있어요.")});
        data.Add(6, new List<Dialog> {
            new Dialog("나", "승인되었습니다."),
            new Dialog("사냥꾼", "이곳에서 빨리 떠나는게 좋을 거야.")});
    }

    // refuse data 생성
    void GenerateRefuseData(Dictionary<int, List<Dialog>> data)
    {
        data.Add(0, new List<Dialog> {
            new Dialog("나", "소속과 설명이 다릅니다."),
            new Dialog("전사", "하하 신입이라 그런지 꼼꼼하구만...?!")});
        data.Add(1, new List<Dialog> {
            new Dialog("나", "소속과 설명이 다릅니다."),
            new Dialog("암살자", "… (아무 말 없이 들어왔던 문으로 향한다.)"),
            new Dialog("나", "(암살자들은 하나 같이 음침한 구석이 있다니까...)")});
        data.Add(2, new List<Dialog> {
            new Dialog("나", "소속과 설명이 다릅니다."),
            new Dialog("마법사", "뭐...마물들은 다음에 정화하도록 하죠.")});
        data.Add(3, new List<Dialog> {
            new Dialog("나", "소속과 설명이 다릅니다."),
            new Dialog("음유시인", "이런이런 눈치가 빠르시군요~")});
        data.Add(4, new List<Dialog> {
            new Dialog("나", "소속과 설명이 다릅니다."),
            new Dialog("사제", "등불이 저를 원하지 않나 보군요.")});
        data.Add(5, new List<Dialog> {
            new Dialog("나", "소속과 설명이 다릅니다."),
            new Dialog("점성술사", "운명이 저를 거부하네요.")});
        data.Add(6, new List<Dialog> {
            new Dialog("나", "소속과 설명이 다릅니다."),
            new Dialog("사냥꾼", "칫. 이번 사냥은 글렀군.")});
    }

    // 필요한 guideData를 return
    public Tuple<string, string> GetGuideDialogData(int id, int index)
    {
        Debug.Log("TalkID : " + id);
        if (index == guideData[id].Count)       // index가 guideData[id]의 마지막 index + 1이면
            return Tuple.Create<string, string>(null, null);

        return Tuple.Create<string, string>(guideData[id][index].Type, guideData[id][index].Data);     // 필요한 문장을 id와 index를 통해 return
    }

    // 필요한 permissionData를 return
    public Tuple<string, string> GetPermissionDialogData(int id, int index)
    {
        Debug.Log("TalkID : " + id);
        if (index == permissionData[id].Count)       // index가 permissionData[id]의 마지막 index + 1이면
            return Tuple.Create<string, string>(null, null);

        return Tuple.Create<string, string>(permissionData[id][index].Type, permissionData[id][index].Data);     // 필요한 문장을 id와 index를 통해 return
    }

    // 필요한 refuseData를 return
    public Tuple<string, string> GetRefuseDialogData(int id, int index)
    {
        Debug.Log("TalkID : " + id);
        if (index == refuseData[id].Count)       // index가 refuseData[id]의 마지막 index + 1이면
            return Tuple.Create<string, string>(null, null);

        return Tuple.Create<string, string>(refuseData[id][index].Type, refuseData[id][index].Data);     // 필요한 문장을 id와 index를 통해 return
    }
}
