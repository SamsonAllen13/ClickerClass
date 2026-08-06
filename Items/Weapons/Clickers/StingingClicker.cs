using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class StingingClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Poison, projsInheritItemElements: true);

			ClickEffect.Swarm = ClickerSystem.RegisterClickEffect(Mod, "Swarm", 4, new Color(255, 225, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				int beeType = player.strongBees ? ModContent.ProjectileType<StingingClickerPro2>() : ModContent.ProjectileType<StingingClickerPro>();
				int beeDamage = player.strongBees ? (int)(damage * 1.5f) : damage;
				Projectile.NewProjectile(source, position.X, position.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), beeType, beeDamage, knockBack, player.whoAmI);
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.25f);
			SetColor(Item, new Color(255, 200, 100));
			SetDust(Item, 138);
			AddEffect(Item, ClickEffect.Swarm);

			Item.damage = 7;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.Orange;
		}
	}
}
