using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repair
{
    public class RepairManager : MonoBehaviour
    {
        private string              strName;
        private int                 iWeaponID;
        private GuestDB.WeaponState dtState;
        private string              strHintText;

        public void SetRepairInfo(string pstrNPCName, int piWeaponID, GuestDB.WeaponState pdtState)
        {
            strName = pstrNPCName;      // string copy 해줘야 할지도
            iWeaponID = piWeaponID;
            dtState = pdtState;         // 메모리 복사 해줘야 할지도
            strHintText = GetHint();
        }

        string GetHint()
        {
            return "hint";
        }
    }
}
