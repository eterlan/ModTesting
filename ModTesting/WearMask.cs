using MelonLoader;
using UnityEngine;

namespace TXL
{
    public class WearMask
    {
        public int SuspectValue;

        
        public void OnGUI() 
        {
            // Make a background box
            GUI.Box(new Rect(10,10,200,150), "Loader Menu");
    
            // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
            if(GUI.Button(new Rect(20,40,80,20), "蒙面"))
            {
                API.AddLuck(g.world.playerUnit, Variable.FaceMaskId);
            }
            if(GUI.Button(new Rect(120,40,80,20), "取下面罩"))
            {
                API.RemoveLuck(g.world.playerUnit, Variable.FaceMaskId);
            }
    
            // Make the second button.
            if(GUI.Button(new Rect(20,70,80,20), "开启杀气")) 
            {
                API.AddLuck(g.world.playerUnit, Variable.BloodThirstyId);
            }
                
            GUI.Label(new Rect(20, 100, 80, 20), "怀疑度");
            GUI.Label(new Rect(20, 130, 80, 20), $"{SuspectValue.ToString()}");
        }
    }
}