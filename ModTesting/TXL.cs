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
        private KillSecretly killSecretly;
        private WearMask wearMask;

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Input.GetKeyDown(KeyCode.T))
            {
                //
            }
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
            killSecretly = new KillSecretly();
            killSecretly.Init();

            wearMask = new WearMask();
        }

        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            killSecretly.Destroy();
        }

        public override void OnGUI()
        {
            base.OnGUI();
            wearMask.OnGUI();
        }
    }
}