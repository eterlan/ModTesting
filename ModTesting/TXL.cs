using System;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using Type = Il2CppSystem.Type;


namespace TXL
{
    public class TXL : MelonMod
    {
        public int SuspectValue;
        public const int FaceMaskId = 888801;
        public const int BloodThirstyId = 888802;
        private System.Action<ETypeData> onKillAct;
        private bool eventInit;

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Input.GetKeyDown(KeyCode.T))
            {
                MelonLogger.Msg($"怀疑度: {SuspectValue.ToString()}");
                SuspectValue += 10;
            }
        }
        
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Debug.Log($"sceneWasLoaded, {sceneName}");
            if (eventInit)
            {
                return;
            }
            if (g.root == null || g.world == null)
            {
                Debug.Log("isNull");
                return;
            }
            
            Debug.Log("Init");
            eventInit = true;
            onKillAct = OnKill;
            var eType = Il2CppType.Of<UnitActionRoleKill>();
            var e = EGameType.OneUnitCreateOneActionBack(g.world.playerUnit, eType) ;
            
            g.events.On(e, onKillAct);
            
        }

        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            g.events.Off(EGameType.WorldUnitDie, onKillAct);
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
        
        public override void OnGUI() 
        {
            base.OnGUI();
            // Make a background box
            GUI.Box(new Rect(10,10,200,150), "Loader Menu");
    
            // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
            if(GUI.Button(new Rect(20,40,80,20), "蒙面"))
            {
                API.AddLuck(g.world.playerUnit, FaceMaskId);
            }
            if(GUI.Button(new Rect(120,40,80,20), "取下面罩"))
            {
                API.RemoveLuck(g.world.playerUnit, FaceMaskId);
            }
    
            // Make the second button.
            if(GUI.Button(new Rect(20,70,80,20), "开启杀气")) 
            {
                API.AddLuck(g.world.playerUnit, BloodThirstyId);
            }
                
            GUI.Label(new Rect(20, 100, 80, 20), "怀疑度");
            GUI.Label(new Rect(20, 130, 80, 20), $"{SuspectValue.ToString()}");
        }
    }
}