using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Interfaces.Configuration;
using Robust.Shared.IoC;
using Robust.Shared.Localization;
using Robust.Shared.Maths;

#nullable enable

namespace Content.Client.UserInterface
{
    public sealed partial class OptionsMenu : SS14Window
    {
        [Dependency] private readonly IConfigurationManager _configManager = default!;

        protected override Vector2? CustomSize => (800, 450);

        public OptionsMenu()
        {
            IoCManager.InjectDependencies(this);

            Title = Loc.GetString("Game Options");

            GraphicsControl graphicsControl;
            KeyRebindControl rebindControl;
            AudioControl audioControl;
            InterfaceControl interfaceControl;

            var tabs = new TabContainer
            {
                Children =
                {
                    (graphicsControl = new GraphicsControl(_configManager)),
                    (rebindControl = new KeyRebindControl()),
                    (interfaceControl = new InterfaceControl(_configManager)),
                    (audioControl = new AudioControl(_configManager))
                }
            };

            TabContainer.SetTabTitle(graphicsControl, Loc.GetString("Graphics"));
            TabContainer.SetTabTitle(rebindControl, Loc.GetString("Controls"));
            TabContainer.SetTabTitle(interfaceControl, Loc.GetString("Interface"));
            TabContainer.SetTabTitle(audioControl, Loc.GetString("Audio"));

            Contents.AddChild(tabs);
        }
    }
}
