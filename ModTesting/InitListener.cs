using System.Collections.Generic;
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
            var unitData = g.world.playerUnit.data.unitData;
            var units = GetRangeUnitsOnGrid(unitData.GetPoint(), 2);
            for (var i = 0; i < units.Count; i++)
            {
                // 获取念力, 获取id, 判断是否在组织中.
                var id = units[i].data.unitData.unitID;
                var sp = units[i].data.dynUnitData.sp;
                
            }
            Debug.Log($"terrainType: {unitData.pointGridData._terrainType.name}, id: {unitData.pointGridData._terrainType.id}");
        }

        public List<WorldUnitBase> GetRangeUnitsOnGrid(Vector2Int point, int range)
        {
            var points = GetRangePoints(point, range);
            var units = new List<WorldUnitBase>();
            for (var i = 0; i < points.Count; i++)
            {
                var oneGridUnits = g.data.map.GetGridUnit(point);
                if (oneGridUnits == null)
                {
                    Debug.Log("This grid has no unit, continue to next grid...");
                    continue;
                }
                for (var j = 0; j < oneGridUnits.Count; j++)
                {   
                    units.Add(oneGridUnits[j]);
                }
            }

            Debug.Log($"point: {point}, range: {range}, {units.Count} units found");
            return units;
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