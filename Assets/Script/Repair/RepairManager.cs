﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;

namespace Repair
{
    public class RepairManager : MonoBehaviour
    {
        class ActionData
        {
            public string action;
            public int level; // 낮 -> 높, 매우 나쁨 -> 매우 좋음

            public ActionData(string pstrAction, int piLevel)
            {
                action = string.Copy(pstrAction);
                level = piLevel;
            }
        }

        private Hint                hintManager;
        private string              strOwnerName;
        private Queue<ActionData>   qWeaponState = new Queue<ActionData>();

        [SerializeField] private TMP_Text HintText;
        [SerializeField] private TextAsset csvHint = null;

        public void Start()
        {
            // 임시 설정
            strOwnerName = "톨문드";


            WeaponInfo.state.iDamageState = 1;
            WeaponInfo.state.iDurabilityState = 2;
            WeaponInfo.state.iDefenseState = 3;

            List<ActionData> curWeaponStateList = new List<ActionData>();
            hintManager = new Hint();

            hintManager.SetHintDataFromCSV(csvHint);

            // 현재 무기 상태를 리스트에 저장
            curWeaponStateList.Add(new ActionData("공격력", WeaponInfo.state.iDamageState));
            curWeaponStateList.Add(new ActionData("내구도", WeaponInfo.state.iDurabilityState));
            curWeaponStateList.Add(new ActionData("방어력", WeaponInfo.state.iDefenseState));

            // 무기 상태 순서를 랜덤으로 조정
            var tRand = new System.Random();
            var randList = curWeaponStateList.OrderBy(item => tRand.Next()).ToList();

            for(int i=0; i<randList.Count(); i++)
            {
                qWeaponState.Enqueue(randList[i]);
            }

            // 힌트 세팅
            SetHint();
        }

        public void SetHint()
        {
            ActionData tAction = qWeaponState.Dequeue();

            List<string> arrHint = hintManager.GetHint(strOwnerName, tAction.action);
        }
    }
}