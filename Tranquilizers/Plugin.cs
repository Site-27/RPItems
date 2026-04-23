using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
// using RPItems.Commands;
using Server = Exiled.Events.Handlers.Server;

namespace RPItems;

public class Plugin : Plugin<Config>
{
    public static Plugin Instance = null!;

    public override void OnEnabled()
    {
        Instance = this;
        CustomItem.RegisterItems( /*overrideClass: Config*/);
        InitEvents();
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        DeinitEvents();
        CustomItem.UnregisterItems();
        base.OnDisabled();
        Instance = null!;
    }

    private static void InitEvents()
    {
        // Server.WaitingForPlayers += ItemConfig.GetDefaultValues;
    }

    private static void DeinitEvents()
    {
        // Server.WaitingForPlayers -= ItemConfig.GetDefaultValues;
    }
}