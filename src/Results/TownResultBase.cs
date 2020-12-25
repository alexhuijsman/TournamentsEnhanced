using TaleWorlds.CampaignSystem;

namespace TournamentsEnhanced
{
    public interface ITownResultBase : IResultBase
    {
        Town Town { get; }
    }

    public abstract class TownResultBase : ResultBase, ITownResultBase
    {
        public Town Town { get; private set; }

        protected TownResultBase(ResultStatus status) : base(status) { }
        protected TownResultBase(ResultStatus status, Town town) : base(status)
        {
            Town = town;
        }
    }
}
