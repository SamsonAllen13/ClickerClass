using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class InfluenceBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Influence");
			Description.SetDefault("Increases your base click radius by 20%");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Воздействие");
			Description.AddTranslation(GameCulture.Russian, "Увеличивает ваш базовый радиус курсора на 20%");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 0.4f;
		}
	}
}
