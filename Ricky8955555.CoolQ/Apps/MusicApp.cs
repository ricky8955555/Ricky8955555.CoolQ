using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    class MusicApp : App
    {
        internal override string Name { get; } = "Music";

        internal override string DisplayName { get; } = "点歌";

        internal override Feature[] Features { get; } = new Feature[]
        {
            new MusicCommand()
        };
    }
}