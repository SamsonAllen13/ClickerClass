using ClickerClass.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Tools
{
	//TODO dropped item glowmask
	public class MiceDrill : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.IsDrill[Type] = true; //This causes the following changes:
			/*
			 * useTime = (int)((double)useTime * 0.6);
				if (useTime < 1)
					useTime = 1;

				useAnimation = (int)((double)useAnimation * 0.6);
				if (useAnimation < 1)
					useAnimation = 1;

				tileBoost--;
			 */
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 54;
			Item.height = 26;
			Item.damage = 50;
			Item.knockBack = 0f;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 25;
			Item.useTime = 4;
			Item.pick = 225;
			Item.UseSound = SoundID.Item23;
			Item.rare = 10;
			Item.value = Item.sellPrice(0, 7, 0, 0);
			Item.tileBoost += 3;
			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.shootSpeed = 32f;
			Item.shoot = ModContent.ProjectileType<MiceDrillPro>();
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 12).AddIngredient(ItemID.LunarBar, 10).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
