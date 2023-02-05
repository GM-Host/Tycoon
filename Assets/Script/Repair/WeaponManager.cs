using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repair
{
    public struct WeaponState
    {
        public int iDurabilityState;
        public int iDamageState;
        public int iDefenseState;
        public bool bCurseState;
        public int iRuneLevel;
    }

    public enum WeaponStateType
    {
        Worst,
        Bad,
        Good,
        Best
    }

    public static class WeaponInfo
    {
        public static string name;
        public static WeaponState state;

        public static void Set(string pstrname, int piDurabilityState, int piDamageState, int piDefenseState, bool pbCurseState, int piRuneLevel)
        {
            name = string.Copy(pstrname);
            state.iDurabilityState = piDurabilityState;
            state.iDamageState = piDamageState;
            state.iDefenseState = piDefenseState;
            state.bCurseState = pbCurseState;
            state.iRuneLevel = piRuneLevel;
        }
    }
}