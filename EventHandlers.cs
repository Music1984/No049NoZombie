using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features.Roles;
using Exiled.Events.Commands.Reload;
using Exiled.Events.EventArgs;
using GameCore;
using MEC;
using PlayerStatsSystem;
using UnityEngine;

namespace No049NoZombie
{
    using Exiled.API.Features;

    /// <summary>
    /// General event handlers.
    /// </summary>
    public class EventHandlers
    {
        private readonly Plugin plugin;
         

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">The <see cref="Plugin{TConfig}"/> class reference.</param>
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        /// <inheritdoc cref="SpawningEventArgs"/>
        public void OnRoundStart(SpawningEventArgs ev)
        {
            Timing.RunCoroutine(DamageTick());
        }

        private IEnumerator<float> DamageTick()
        {
            while (Round.IsStarted)
            {
                yield return Timing.WaitForSeconds(1f);

                List<Player> scpList = Player.Get(player => player.IsScp && player.Role.Type != RoleType.Scp0492).ToList();

                if ((Player.Get(RoleType.Scp049).Any() && !plugin.Config.AllScpsDead) || (plugin.Config.AllScpsDead && scpList.Count != 0))
                    continue;

                foreach (Player player in Player.List)
                {
                    if (player.Role.Type == RoleType.Scp0492)
                    {
                        player.Hurt(plugin.Config.DamageIntensity,
                            plugin.Config.AllScpsDead
                                ? "Decayed away from all other SCPs no longer being alive."
                                : "Decayed away from SCP-049 no longer being alive.");
                        player.EnableEffect<Poisoned>(400);
                        PlayerEffect effect = player.GetEffect(EffectType.Poisoned);
                        effect.Intensity = plugin.Config.PoisonIntensity;
                    }
                }
            }

        }
    }
}