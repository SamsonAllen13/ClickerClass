using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Misc
{
	public class TrickyKeychain : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToVanitypet(ModContent.ProjectileType<Projectiles.Pets.TrickyKeychainPro>(), ModContent.BuffType<Buffs.Pets.TrickyKeychainBuff>());
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);
			return false;
		}
	}
}
