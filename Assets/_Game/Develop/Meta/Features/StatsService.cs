using _Game.Develop.Utils.DataManagement;
using _Game.Develop.Utils.DataManagement.DataProviders;

namespace _Game.Develop.Meta.Features
{
    public class StatsService : IDataReader<StatsData>, IDataWriter<StatsData>
    {
        private readonly StatsDataProvider _statsDataProvider;
        public int VictoryCount { get; private set; }
        public int DefeatCount { get; private set; }

        public StatsService(StatsDataProvider statsDataProvider)
        {
            _statsDataProvider = statsDataProvider;

            _statsDataProvider.RegisterReader(this);
            _statsDataProvider.RegisterWriter(this);
        }

        public void IncreaseVictoryCount()
        {
            VictoryCount++;
        }

        public void IncreaseDefeatCount()
        {
            DefeatCount++;
        }

        public void ReadFrom(StatsData data)
        {
            VictoryCount = data.VictoryCount;
            DefeatCount = data.DefeatCount;
        }

        public void WriteTo(StatsData data)
        {
            data.VictoryCount = VictoryCount;
            data.DefeatCount = DefeatCount;
        }
    }
}