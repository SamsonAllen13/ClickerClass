using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ClickerClass.Items
{
	public class MiceFragment : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mice Fragment");
			Tooltip.SetDefault("'Erratic sparks skitter across this fragment'");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Фрагмент мышки");
			Tooltip.AddTranslation(GameCulture.Russian, "'Вокруг этого фрагмента витают беспорядочные искры'");
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.sellPrice(0, 0, 20, 0);
			item.rare = 9;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
	}
}