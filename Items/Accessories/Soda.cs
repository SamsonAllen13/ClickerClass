using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.Waist)]
	public class Soda : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("Reduces the amount of clicks required for a click effect by 1");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Сода");
			Tooltip.AddTranslation(GameCulture.Russian, "Снижает требуемое количество кликов для эффекта курсора на 1 единицу");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 25000;
			item.rare = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().clickerBonus += 1;
		}
	}
}
