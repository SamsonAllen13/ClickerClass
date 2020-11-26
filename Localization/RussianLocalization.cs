using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Localization
{
	public class RussianLocalization : GlobalItem
	{
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (LanguageManager.Instance.ActiveCulture == GameCulture.Russian)
			{
				foreach (TooltipLine tooltipLine in tooltips)
				{
					switch (tooltipLine.Name)
					{
						case "ClickerTag":
						{
							tooltipLine.text = tooltipLine.text.Replace("-Clicker Class-", "Кликер");
							break;
						}
						case "Damage":
						{
							tooltipLine.text = tooltipLine.text.Replace("1% enemy life click damage", "1% здоровья врага к урону клика").Replace("click damage", "урона клика");
							break;
						}
						case "itemClickerAmount":
						{
							Dictionary<string, string> replacementTranslations = new Dictionary<string, string>
							{
								{"clicks", "кликов"},
								{"click", "клик"},
								{"Auto Click", "Авто-клик"},
								{"Bombard", "Бомбардировка"},
								{"Collision", "Столкновение"},
								{"Conqueror", "Завоеватель"},
								{"Curse", "Проклятие"},
								{"Cursed Eruption", "Проклятое извержение"},
								{"Dark Burst", "Тёмный взрыв"},
								{"Dazzle", "Ослепление"},
								{"Discharge", "Разряд"},
								{"Double Click", "Двойной клик"},
								{"Embrittle", "Хрупкость"},
								{"Freeze", "Заморозка"},
								{"Haste", "Спешка"},
								{"Holy Nova", "Кольцо света"},
								{"Illuminate", "Просветление"},
								{"Inferno", "Ад"},
								{"Infest", "Заражение"},
								{"Lacerate", "Разрыв"},
								{"Linger", "Промедление"},
								{"Petal Storm", "Лепестковая буря"},
								{"Phase Reach", "Фазовая досягаемость"},
								{"Razor's Edge", "Лезвие бритвы"},
								{"Regenerate", "Регенерация"},
								{"Shadow Lash", "Теневая плеть"},
								{"Siphon", "Сифон"},
								{"Solar Flare", "Солнечная вспышка"},
								{"Spiral", "Спираль"},
								{"Splash", "Всплеск"},
								{"Star Storm", "Звёздная буря"},
								{"Sticky Honey", "Липкий мёд"},
								{"Stinging Thorn", "Жалящий шип"},
								{"The Click", "Клик"},
								{"Totality", "Тотальность"},
								{"Toxic Release", "Токсичный выброс"},
								{"True Strike", "Истинный удар"},
								{"Wild Magic", "Необузданная магия"}
							};
							
							foreach (var translation in replacementTranslations)
								tooltipLine.text = tooltipLine.text.Replace(translation.Key, translation.Value);
							break;
						}
						case "PrefixClickerRadius":
						{
							tooltipLine.text = tooltipLine.text.Replace("% base clicker radius", "% базовый радиус курсора");
							break;
						}
						case "PrefixClickBoost":
						{
							tooltipLine.text = tooltipLine.text.Replace("clicks required", "кликов необходимо");
							break;
						}
						case "transformationText":
						{
							tooltipLine.text = tooltipLine.text.Replace("Total clicks", "Всего кликов");
							break;
						}
					}
				}
			}
		}
	}
}