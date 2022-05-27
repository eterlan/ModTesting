using Il2CppSystem.Collections.Generic;
using Il2CppSystem;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace TXL
{
    public class InitListener
    {
        public int SuspectValue;
        public const int FaceMaskId = 888801;
        public const int BloodThirstyId = 888802;
        private System.Action<ETypeData> onKillAct;

        
        public void Init()
        {
            var eKill = EGameType.OneUnitCreateOneActionBack(g.world.playerUnit, Il2CppType.Of<UnitActionRoleKill>());
            var eMove = EGameType.OneCreateActionBack(Il2CppType.Of<UnitActionMovePlayer>());
            g.events.On(eKill, onKillAct);
            g.events.On(EGameType.OneOpenUIEnd(UIType.Town), (Action)OnTownOpenEnd);
            g.events.On(eMove, (Action<ETypeData>)OnPlayerMove);
            onKillAct = OnKill;

        }

        public void Destroy()
        {
            g.events.Off(EGameType.WorldUnitDie, onKillAct);
            g.events.Off(EGameType.OneOpenUIEnd(UIType.Town), (Action)OnTownOpenEnd);

            Debug.Log("QUit");
        }
        private void OnKill(ETypeData eTypeData)
        {
            var eData = eTypeData.Cast<EGameTypeData.OneUnitCreateOneActionBack>();
            var killer = eData.action.unit;
            var hasMask = killer.GetLuck(FaceMaskId);
            if (hasMask == null)
            {
                return;
            }
            API.AddLuck(killer, BloodThirstyId);
        }
        
        private void OnTownOpenEnd()
        {
            Debug.Log("TownOpen");
        }

        private void OnPlayerMove(ETypeData eTypeData)
        {
            var eData = eTypeData.Cast<EGameTypeData.OneUnitCreateOneActionBack>();
        }

        public List<WorldUnitBase> GetRangeUnitsOnGrid(Vector2Int point, int range)
        {
            var points = GetRangePoints(point, range);
            for (var i = 0; i < points.Count; i++)
            {
                g.data.map.GetGridUnit(point);
            }
        }

        public static List<Vector2Int> GetRangePoints(Vector2Int point, int range)
        {
            var points = new List<Vector2Int>();
            var origin = new Vector2Int(point.x - range, point.y - range);
            for (var i = 0; i < 2 * range + 1; i++)
            {
                for (var j = 0; j < 2 * range + 1; j++)
                {
                    var currX = origin.x + j;
                    var currY = origin.y + i;
                    if (Math.Abs(currX - point.x) + Math.Abs(currY - point.y) > range)
                        continue;
                    var v = new Vector2Int(currX, currY);
                    points.Add(v);
                    Debug.Log(v.ToString());
                }
            }

            return points;
        }
    }
}