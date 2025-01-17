﻿using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using System.Collections.Generic;
using GameServerCore.Domain.GameObjects.Spell.Sector;
using GameServerCore.Scripting.CSharp;
using GameServerCore.Domain.GameObjects.Spell.Missile;
using GameServerCore.Domain;


namespace Spells
{
    public class PoppyParagonOfDemacia : ISpellScript
    {
        public ISpellScriptMetadata ScriptMetadata => new SpellScriptMetadata()
        {
            TriggersSpellCasts = true,
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,


        };
        IObjAIBase daowner;
        ISpell daspell;

        public void OnActivate(IObjAIBase owner, ISpell spell)
        {
        }

        public ISpellSector DamageSector;

        public void OnDeactivate(IObjAIBase owner, ISpell spell)
        {
        }

        public void OnSpellPreCast(IObjAIBase owner, ISpell spell, IAttackableUnit target, Vector2 start, Vector2 end)
        {
            daowner = owner;
            daspell = spell;
            //ApiEventManager.OnLevelUpSpell.AddListener(this, owner.GetSpell("PoppyParagonOfDemacia"), AddPoppyWPassive, false);
            ApiEventManager.OnHitUnit.AddListener(this, owner, GivePoppySomething, false);
        }

        public void GivePoppySomething(IDamageData damage)
        {
            AddBuff("PoppyParagonManager", 5f, 1, daspell, daowner, daowner, false);
        }

        public void AddPoppyWPassive(ISpell spell)
        {
            AddBuff("PoppyParagonManager", 5f, 1, spell, daowner, daowner, false);
        }

        public void OnSpellCast(ISpell spell)
        {
            ApiEventManager.OnHitUnit.RemoveListener(this, daowner);
            var owner = spell.CastInfo.Owner;
            AddBuff("PoppyParagonSpeed", 5f, 1, spell, daowner, daowner);
            //owner.RemoveBuffsWithName("PoppyParagonManager");
            for (int i = 1; i <= 10; i++)
            {
                AddBuff("PoppyParagonManager", 5f, 1, spell, daowner, daowner, false);

            }
        }

        public void OnSpellPostCast(ISpell spell)
        {
            ApiEventManager.OnHitUnit.AddListener(this, daowner, GivePoppySomething, false);

        }

        public void OnSpellChannel(ISpell spell)
        {
        }
        public void TargetExecute(ISpell spell, IAttackableUnit target, ISpellMissile missile, ISpellSector sector)
        {
        }
        public void OnSpellChannelCancel(ISpell spell, ChannelingStopSource reason)
        {
        }

        public void OnSpellPostChannel(ISpell spell)
        {
        }

        public void OnUpdate(float diff)
        {
        }
    }
}