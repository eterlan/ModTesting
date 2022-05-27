using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using Il2CppSystem;
using Action = Il2CppSystem.Action;


namespace TXL
{
    public class TXL : MelonMod
    {
        private bool init;
        private InitListener initListener;

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Debug.Log($"sceneWasLoaded, {sceneName}");
            if (init)
            {
                return;
            }
            if (g.root == null || g.world == null)
            {
                Debug.Log("isNull");
                return;
            }
            
            Debug.Log("Init");
            init = true;
            initListener = new InitListener();
            initListener.Init();

        }

        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            initListener.Destroy();
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