﻿using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class PortableParticleAccelerator : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("Clicking within the inner 20% of your clicker radius deals 20% more damage");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Карманный ускоритель частиц");
			Tooltip.AddTranslation(GameCulture.Russian, "Клики в пределах 20% общего радиуса вашего курсора наносят на 20% больше урона");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 45000;
			item.rare = -1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accPortableParticleAccelerator = true;
		}

		/*
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 12);
			recipe.AddIngredient(ItemID.Cog, 10);
			recipe.AddIngredient(ItemID.Wire, 25);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		*/
	}
}
