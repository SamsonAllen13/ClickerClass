using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class AutoClick : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Auto Click");
			Description.SetDefault("Hold down the Left Mouse Button to auto click");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Авто-клик");
			Description.AddTranslation(GameCulture.Russian, "Зажмите ЛКМ для авто-кликов");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{

		}
	}
}
