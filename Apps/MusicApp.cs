using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class MusicApp : App
    {
        public override string Name { get; } = "Music";

        public override string DisplayName { get; } = "点歌";

        public override Feature[] Features { get; } = new Feature[]
        {
            new MusicCommand()
        };
    }
}
