using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.Waist)]
	public class GamerCrate : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("'You don't actually think someone would play this, do you?'"
						+ "\nIncreases click damage by 10%"
						+ "\nIncreases your base click radius by 50%"
						+ "\nReduces the amount of clicks required for a click effect by 20%"
						+ "\nYour clicks produce a burst of mechanical light, while accessory is visible"
						+ "\nPressing the 'Clicker Accessory' key will toggle auto click on all Clickers"
						+ "\nWhile auto click is enabled, click rates are decreased");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Игровой ящик");
			Tooltip.AddTranslation(GameCulture.Russian, "'Вы же не думаете, что кто-то будет играть в это, не так ли?'\nУвеличивает урон от клика на 10%\nУвеличивает ваш базовый радиус курсора на 50%\nСнижает требуемое количество кликов для эффекта курсора на 20%\nВаши клики оставляют механическую вспышку света, пока видимость аксессуара включена\nНажатие кнопки 'Clicker Accessory', переключает на авто-клик все курсоры\nПока авто-клик активирован, частота кликов снижена");
		}

		public override void SetDefaults()
		{
			SetDisplayTotalClicks(item);
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 5);
			item.rare = 7;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerRadius += 1f;
			clickerPlayer.clickerDamage += 0.10f;
			clickerPlayer.clickerBonusPercent -= 0.20f;
			clickerPlayer.accHandCream = true;
			if (!hideVisual)
			{
				clickerPlayer.accEnchantedLED2 = true;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "EnchantedLED", 1);
			recipe.AddIngredient(null, "Soda", 1);
			recipe.AddIngredient(null, "MousePad", 1);
			recipe.AddIngredient(null, "HandCream", 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
