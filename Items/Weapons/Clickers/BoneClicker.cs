using ClickerClass.Buffs;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BoneClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Earth, projsInheritItemElements: true);

			ClickEffect.Lacerate = ClickerSystem.RegisterClickEffect(Mod, "Lacerate", 12, new Color(225, 225, 200), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<BoneClickerPro>(), damage, knockBack, player.whoAmI);
			},
			preHardMode: true,
			descriptionArgs: new object[] { Gouge.DamageOverTime });
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.1f);
			SetColor(Item, new Color(225, 225, 200));
			SetDust(Item, 216);
			AddEffect(Item, ClickEffect.Lacerate);

			Item.damage = 8;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 0, 30, 0);
			Item.rare = ItemRarityID.Blue;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.FossilOre, 8).AddTile(TileID.Anvils).Register();
		}
	}
}
