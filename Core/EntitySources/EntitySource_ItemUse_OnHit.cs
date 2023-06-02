using Terraria;
using Terraria.DataStructures;

namespace ClickerClass.Core.EntitySources
{
	//Vanilla has no source like this
	public class EntitySource_ItemUse_OnHit : EntitySource_ItemUse, IEntitySource_OnHit
	{
		public Entity Attacker => Entity;

		public Entity Victim { get; }

		public EntitySource_ItemUse_OnHit(Player player, Item item, Entity victim, string context = null) : base(player, item, context)
		{
			Victim = victim;
		}
	}
}
