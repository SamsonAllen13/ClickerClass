using Terraria;
using Terraria.ID;

namespace ClickerClass.Utilities
{
	internal static partial class NPCHelper
	{
		/// <summary>
		/// Checks if this NPC is a vanilla limb
		/// </summary>
		/// <param name="npc">The NPC</param>
		/// <returns>True if a boss limb</returns>
		public static bool IsBossLimb(this NPC npc)
		{
			switch (npc.type)
			{
				//Technically none of them have npc.boss, so each one can be a limb
				case NPCID.EaterofWorldsHead:
				case NPCID.EaterofWorldsBody:
				case NPCID.EaterofWorldsTail:

				//Golem
				case NPCID.GolemHead:
				case NPCID.GolemHeadFree:
				case NPCID.GolemFistLeft:
				case NPCID.GolemFistRight:

				//MartianSaucerCore
				case NPCID.MartianSaucerCannon:
				case NPCID.MartianSaucerTurret:
				case NPCID.MartianSaucer:

				//MoonLordCore
				case NPCID.MoonLordHand:
				case NPCID.MoonLordHead:

				//SkeletronPrime
				case NPCID.PrimeCannon:
				case NPCID.PrimeLaser:
				case NPCID.PrimeSaw:
				case NPCID.PrimeVice:

				//PirateShip
				case NPCID.PirateShipCannon:

				//Skeletron
				case NPCID.SkeletronHand:
					return true;
			}

			return false;
		}

		/// <summary>
		/// Includes "pseudo-bosses" from vanilla, namely Dungeon Guardian, the three EoW segments, and anything in NPCID.Sets.TechnicallyABoss
		/// </summary>
		/// <param name="npc">The NPC</param>
		/// <returns>True if the NPC is a boss, or technically a boss</returns>
		public static bool IsBoss(this NPC npc)
		{
			int type = npc.type;
			switch (type)
			{
				//Technically none of them have npc.boss
				case NPCID.EaterofWorldsHead:
				case NPCID.EaterofWorldsBody:
				case NPCID.EaterofWorldsTail:

				//Does not have npc.boss
				case NPCID.DungeonGuardian:
					return true;
			}

			if (npc.IsChild(out NPC parent) && parent.whoAmI != npc.whoAmI && parent.IsBoss())
			{
				return true;
			}

			return npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[type]; //Lunar Pillars don't have npc.boss
		}

		/// <summary>
		/// Checks if this NPC is a miniboss from vanilla or Thorium
		/// </summary>
		/// <param name="npc">The NPC</param>
		/// <returns>True if a miniboss</returns>
		public static bool IsMiniBoss(this NPC npc)
		{
			switch (npc.type)
			{
				//Just because
				case NPCID.WyvernHead:

				//Biome bosses
				case NPCID.SandElemental:
				case NPCID.IceGolem:
				case NPCID.Paladin:
				case NPCID.BigMimicCorruption:
				case NPCID.BigMimicCrimson:
				case NPCID.BigMimicHallow:
				case NPCID.Mothron:
				case NPCID.MartianSaucerCore: //Saucer is not a boss in 1.4 anymore, just for info

				//Questionable
				case NPCID.GoblinSummoner:
				case NPCID.PirateCaptain:
				case NPCID.HeadlessHorseman:
				case NPCID.Nailhead:

				//Event bosses
				case NPCID.PirateShip:
				case NPCID.IceQueen:
				case NPCID.SantaNK1:
				case NPCID.Everscream:
				case NPCID.Pumpking:
				case NPCID.MourningWood:
				case NPCID.DD2Betsy:
				case NPCID.DD2DarkMageT1:
				case NPCID.DD2DarkMageT3:
				case NPCID.DD2OgreT2:
				case NPCID.DD2OgreT3:
					return true;
			}

			if (npc.IsChild(out NPC parent) && parent.whoAmI != npc.whoAmI && parent.IsMiniBoss())
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Combination of <see cref="IsBoss"/>, <see cref="IsBossLimb"/>, and <see cref="IsMiniBoss"/>.
		/// </summary>
		/// <param name="npc">The NPC</param>
		/// <returns><see langword="true"/>if this NPC is a boss or in any way related to a boss (but not a boss minion)</returns>
		public static bool IsBossOrRelated(this NPC npc)
		{
			return npc.IsBoss() || npc.IsBossLimb() || npc.IsMiniBoss();
		}
	}
}
