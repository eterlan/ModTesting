using MelonLoader;
using UnityEngine;

namespace TXL
{
    public class TXL : MelonMod
    {
        public int SuspectValue;
        public const int FaceMaskId = 888801;
        public const int BloodThirstyId = 888802;
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
            base.OnSceneWasLoaded(buildIndex, sceneName);
            MelonLogger.Msg($"sceneWasLoaded, {sceneName}");
        }
        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasUnloaded(buildIndex, sceneName);
            MelonLogger.Msg($"sceneWasUnloaded, {sceneName}");
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


    }
}