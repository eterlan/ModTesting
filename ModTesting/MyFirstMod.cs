using MelonLoader;
using UnityEngine;


namespace ModTesting
{
    public class MyFirstMod : MelonMod
    {
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Input.GetKeyDown(KeyCode.T))
            {
                
                MelonLogger.Msg("HELLO WORLD");
            }
        }

        public override void OnGUI()
        {
            base.OnGUI();
                // Make a background box
                GUI.Box(new Rect(10,10,100,90), "Loader Menu");
    
                // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
                if(GUI.Button(new Rect(20,40,80,20), "Level 1"))
                {
                    OnLoad();
                }
    
                // Make the second button.
                if(GUI.Button(new Rect(20,70,80,20), "Level 2")) 
                {
                    Application.LoadLevel(2);
                }
        }

        public void OnLoad()
        {
            
            var allText = g.conf.localText.allText;
            allText.Add("role_feature_postnatal_name9919911", new ConfLocalTextItem(999999, "role_feature_postnatal_name9919911", "杀气", "", ""));          // 文本，数组是为了后续的多语言支持，只填中文一个就ok.);
            allText.Add("role_feature_postnatal_tips9919911", new ConfLocalTextItem(9999999, "role_featur   e_postnatal_tips9919911", "杀人之后不会被其亲友发觉, 开启后持续三个月, 期间经过其他修仙者附近时会被攻击", "", ""));
            var luckItem = new ConfRoleCreateFeatureItem(111111, 2, 0, 6, "3", 0, 0, "role_feature_postnatal_name9919911", "0", "role_feature_postnatal_tips9919911", "0", 0, "0", 0);
            g.conf.roleCreateFeature.AddItem(luckItem);
            var act = new UnitActionLuckAdd(111111);
            g.world.playerUnit.CreateAction(act);
        }
    }
}