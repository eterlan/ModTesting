namespace TXL
{
    public static class API
    {
        public static void AddLuck(WorldUnitBase unit, int id)
        {
            var act = new UnitActionLuckAdd(id);
            unit.CreateAction(act);
        }

        public static void RemoveLuck(WorldUnitBase unit, int id)
        {
            var act = new UnitActionLuckDel(id);
            unit.CreateAction(act);
        }

        public static bool HasLuck(WorldUnitBase unit, int id)
        {
            return unit.GetLuck(id) != null;
        }
    }
}