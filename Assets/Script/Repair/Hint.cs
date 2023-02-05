using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Repair
{
    class Hint
    {
        public class HintData
        {
            public string ownerName;
            public string hintText;
        }

        private Dictionary<string, Dictionary<string, string>> hintDict = new Dictionary<string, Dictionary<string, string>>(); // 상황, 캐릭터 이름, 힌트 내용

        public string GetHint(string pstrOwnerName, string pstrAction)
        {
            return hintDict[pstrAction][pstrOwnerName];
        }

        public void SetHintData(TextAsset csvHint)
        {
            string[] rowValues;

            string tId = "";
            string tAction = "";
            string tHintText = "";
            string tOwnerName = "";

            // 마지막 공백 제외
            string strCsvWeapon = csvHint.text.Substring(0, csvHint.text.Length - 1);

            // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
            string[] rows = strCsvWeapon.Split(new char[] { '\n' });

            // 엑셀 파일 2번째 줄부터 시작
            for (int i = 1; i < rows.Length - 1; i++)
            {
                rowValues = rows[i].Split(new char[] { ',' });

                tId = rowValues[0];
                tHintText = rowValues[2];
                tOwnerName = rowValues[3];

                if (tId == "") continue;

                if (rowValues[1] != "")
                {
                    tAction = string.Copy(rowValues[1]);

                    hintDict.Add(tAction, new Dictionary<string, string>() { { tOwnerName, tHintText } });
                }
                else
                {
                    hintDict[tAction].Add(tOwnerName, tHintText);
                }    
            }

            Debug.Log("");
        }
    }
}
