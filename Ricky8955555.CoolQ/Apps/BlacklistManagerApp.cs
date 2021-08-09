﻿using Ricky8955555.CoolQ.Features;

namespace Ricky8955555.CoolQ.Apps
{
    public class BlacklistManagerApp : App
    {
        public override string Name { get; } = "BlacklistManager";

        public override string DisplayName { get; } = "黑名单管理器";

        public override bool CanDisable { get; } = false;

        public override AppPermission Permission { get; } = AppPermission.Owner;

        public override Feature[] Features { get; } = new Feature[]
        {
            new BlacklistManagerPlainTextCommand(),
            new BlacklistManagerMentionCommand()
        };
    }
}