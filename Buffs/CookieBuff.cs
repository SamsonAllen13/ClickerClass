using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class CookieBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Cookie");
			Description.SetDefault("Increases your click damage, radius, and life regeneration");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Печенье");
			Description.AddTranslation(GameCulture.Russian, "Увеличивает ваш урон от кликов, радиус курсора и регенерацию здоровья");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 0.4f;
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.1f;
			player.lifeRegen += 2;
		}
	}
}
