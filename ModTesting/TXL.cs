using System;
using MelonLoader;
using UnityEngine;


namespace TXL
{
    public class TXL : MelonMod
    {
        public int SuspectValue;
        public const int FaceMaskId = 888801;
        public const int BloodThirstyId = 888802;
        private System.Action<ETypeData> onKillAct;

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Input.GetKeyDown(KeyCode.T))
            {
                MelonLogger.Msg($"怀疑度: {SuspectValue.ToString()}");
                SuspectValue += 10;
            }
        }

        public class{ 

            void ss(){
                Action s = ()=>{MelonLogger.Msg("asda");}; 
                g.events.On(EGameType.WorldUnitDie, (Il2CppSystem.Action)s);
            }	

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (SceneType.login.sceneName == sceneName)
            {
                Action s = () => { MelonLogger.Msg("asda");}; 
                void ss(){};
                g.events.On(EGameType.WorldUnitDie, (Il2CppSystem.Action)s);
                onKillAct = OnKill;
                MelonLogger.Msg($"{sceneName}");
                g.events.On(EGameType.WorldUnitDie, onKillAct);
                
            }
            base.OnSceneWasLoaded(buildIndex, sceneName);
            MelonLogger.Msg($"sceneWasLoaded, {sceneName}");
        }

        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            g.events.Off(EGameType.WorldUnitDie, onKillAct);
            Debug.Log("QUit");
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
                    API.RemoveLuck(g.world.playerUnit, BloodThirstyId);
                }
    
                // Make the second button.
                if(GUI.Button(new Rect(20,70,80,20), "开启杀气")) 
                {
                    API.AddLuck(g.world.playerUnit, BloodThirstyId);
                }
                
                GUI.Label(new Rect(20, 100, 80, 20), "怀疑度");
                GUI.Label(new Rect(20, 130, 80, 20), $"{SuspectValue.ToString()}");
        }

        private void OnKill(ETypeData eTypeData)
        {
            var eData = eTypeData as EGameTypeData.WorldUnitDie;
            var dieUnitId = eData.unit.data.unitData.unitID;
            Debug.Log(dieUnitId);
            var killerId = g.data.unitDie.GetUnitDie(dieUnitId).killUnitID;
            Debug.Log(killerId);
        }
    }
}