namespace _Game.Develop.Utils.DataManagement.DataProviders
{
    public class StatsDataProvider : DataProvider<StatsData>
    {
        public StatsDataProvider(ISaveLoadService saveLoadService) : base(saveLoadService)
        {
        }

        protected override StatsData GetOriginData()
        {
            return new StatsData();
        }
    }
}