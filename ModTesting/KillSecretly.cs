using Il2CppSystem.Collections.Generic;
using Il2CppSystem;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace TXL
{
    public class KillSecretly
    {
        private System.Action<ETypeData> onKillAct;

        
        public void Init()
        {
            var eKill = EGameType.OneUnitCreateOneActionBack(g.world.playerUnit, Il2CppType.Of<UnitActionRoleKill>());
            var eMove = EGameType.OneUnitCreateOneActionBack(g.world.playerUnit, Il2CppType.Of<UnitActionMovePlayer>());
            g.events.On(eKill, onKillAct);
            g.events.On(EGameType.OneOpenUIEnd(UIType.Town), (Action)OnTownOpenEnd);
            g.events.On(eMove, (Action<ETypeData>)OnPlayerMove);
            onKillAct = OnKill;

        }

        public void Destroy()
        {
            g.events.Off(EGameType.WorldUnitDie, onKillAct);
            g.events.Off(EGameType.OneOpenUIEnd(UIType.Town), (Action)OnTownOpenEnd);
            g.events.Off(EGameType.OneUnitCreateOneActionBack(g.world.playerUnit, Il2CppType.Of<UnitActionMovePlayer>()));

            Debug.Log("QUit");
        }
        private void OnKill(ETypeData eTypeData)
        {
            var eData = eTypeData.Cast<EGameTypeData.OneUnitCreateOneActionBack>();
            var killer = eData.action.unit;
            var hasMask = killer.GetLuck(Variable.FaceMaskId);
            if (hasMask == null)
            {
                return;
            }
            API.AddLuck(killer, Variable.BloodThirstyId);
        }
        
        private void OnTownOpenEnd()
        {
            Debug.Log("TownOpen");
        }

        public void OnPlayerMove(ETypeData eTypeData)
        {
            var gridData = g.world.playerUnit.data.unitData.pointGridData;
            Debug.Log(g.world.playerUnit.data.unitData.pointGridData._terrainType.name);
            Debug.Log(VAR);
            var eData = eTypeData.Cast<EGameTypeData.OneUnitCreateOneActionBack>();
            var point = g.world.playerUnit.data.unitData.GetPoint();
            Debug.Log(point.ToString());
            var units = GetRangeUnitsOnGrid(point, 2);
            for (var i = 0; i < units.Count; i++)
            {
                var sp = units[i].data.dynUnitData.sp;
                var level = units[i].data.dynUnitData.curGrade;
                var name = units[i].data.unitData.unitID;
                var pos = units[i].data.unitData.GetPoint();
                Debug.Log($"{name} at {pos.ToString()} level: {level}, sp: {sp}");
            }
        }

        public List<WorldUnitBase> GetRangeUnitsOnGrid(Vector2Int point, int range)
        {
            var points = GetRangePoints(point, range);
            var units = new List<WorldUnitBase>();
            for (var i = 0; i < points.Count; i++)
            {
                units.AddRange(g.data.map.GetGridUnit(point).Cast<IEnumerable<WorldUnitBase>>());
            }

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