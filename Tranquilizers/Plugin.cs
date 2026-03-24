using Exiled.API.Features;

namespace RPItems

{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;

        public override void OnEnabled()
        {
            Instance = this;
            Exiled.CustomItems.API.Features.CustomItem.RegisterItems(overrideClass: Config);
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Instance = null;
            Exiled.CustomItems.API.Features.CustomItem.UnregisterItems();
            base.OnDisabled();
        }
    }
}
