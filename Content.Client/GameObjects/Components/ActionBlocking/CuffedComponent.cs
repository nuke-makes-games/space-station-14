﻿
using Content.Client.Utility;
using Robust.Client.Graphics;
using Robust.Client.Interfaces.ResourceManagement;
using Robust.Shared.IoC;
using Robust.Shared.GameObjects;
using Content.Shared.GameObjects.Components.ActionBlocking;
using Content.Shared.Preferences.Appearance;
using Robust.Client.GameObjects;
using Robust.Shared.Utility;
using Robust.Shared.ViewVariables;

namespace Content.Client.GameObjects.Components.ActionBlocking
{
    [RegisterComponent]
    public class CuffedComponent : SharedCuffedComponent
    {
#pragma warning disable 0649
        [Dependency] private readonly IResourceCache _resourceCache;
#pragma warning restore 0649

        [ViewVariables]
        private string _currentRSI = default;

        public override void HandleComponentState(ComponentState curState, ComponentState nextState)
        {
            var cuffState = curState as CuffedComponentState;

            if (cuffState == null)
            {
                return;
            }

            CanStillInteract = cuffState.CanStillInteract;

            if (Owner.TryGetComponent<SpriteComponent>(out var sprite))
            {
                sprite.LayerSetVisible(HumanoidVisualLayers.Handcuffs, cuffState.NumHandsCuffed > 0);
                sprite.LayerSetColor(HumanoidVisualLayers.Handcuffs, cuffState.Color);

                if (cuffState.NumHandsCuffed > 0)
                {
                    if (_currentRSI != cuffState.RSI) // we don't want to keep loading the same RSI
                    {
                        _currentRSI = cuffState.RSI;
                        sprite.LayerSetState(HumanoidVisualLayers.Handcuffs, new RSI.StateId(cuffState.IconState), new ResourcePath(cuffState.RSI));
                    }
                    else
                    {
                        sprite.LayerSetState(HumanoidVisualLayers.Handcuffs, new RSI.StateId(cuffState.IconState)); // TODO: safety check to see if RSI contains the state?
                    }
                }    
            }
        }

        public override void OnRemove()
        {
            base.OnRemove();

            if (Owner.TryGetComponent<SpriteComponent>(out var sprite))
            {
                sprite.LayerSetVisible(HumanoidVisualLayers.Handcuffs, false);
            }
        }
    }
}