using System.Collections;
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
            WeaponInfo.state.iDamageState = 1;
            WeaponInfo.state.iDurabilityState = 2;
            WeaponInfo.state.iDefenseState = 3;

            List<ActionData> tList = new List<ActionData>();
            hintManager = new Hint();

            hintManager.SetHintData(csvHint);

            tList.Add(new ActionData("공격력", WeaponInfo.state.iDamageState));
            tList.Add(new ActionData("내구도", WeaponInfo.state.iDurabilityState));
            tList.Add(new ActionData("방어력", WeaponInfo.state.iDefenseState));

            var rnd = new System.Random();
            var randList = tList.OrderBy(item => rnd.Next()).ToList();

            for(int i=0; i<randList.Count(); i++)
            {
                qWeaponState.Enqueue(randList[i]);
            }
        }

        public void SetHint()
        {
            ActionData tAction = qWeaponState.Dequeue();

            List<string> arrHint = hintManager.GetHint(strOwnerName, tAction.action);
        }
    }
}