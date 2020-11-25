using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class OverclockBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Overclock");
			Description.SetDefault("Every click will trigger a clicker effect");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Разгон");
			Description.AddTranslation(GameCulture.Russian, "Каждый клик активирует эффект курсора");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{

		}
	}
}
