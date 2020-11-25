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
					if (tooltipLine.Name == "ClickerTag")
					{
						string str = tooltipLine.text;
						string resultA = str.Replace("-Clicker Class-", "Кликер класс");
						tooltipLine.text = resultA;
					}
					
					if (tooltipLine.Name == "Damage")
					{
						string str = tooltipLine.text;
						string resultA = str.Replace("1% enemy life click damage", "1% здоровья врага к урону от клика");
						string resultB = resultA.Replace("click damage", "урона от клика");
						tooltipLine.text = resultB;
					}
					
					if (tooltipLine.Name == "itemClickerAmount")
					{
						string str = tooltipLine.text;
						string resultA = str.Replace("clicks", "кликов");
						string resultB = resultA.Replace("click", "клик");
						
						string resultС = resultB.Replace("Haste", "Спешка");
						string resultD = resultС.Replace("True Strike", "Истинный удар");
						string resultE = resultD.Replace("Holy Nova", "Кольцо света");
						string resultF = resultE.Replace("Collision", "Столкновение");
						string resultG = resultF.Replace("Spiral", "Спираль");
						string resultH = resultG.Replace("Lacerate", "Разрыв");
						string resultI = resultH.Replace("Illuminate", "Просветление");
						string resultJ = resultI.Replace("Dark Burst", "Тёмный взрыв");
						string resultK = resultJ.Replace("Cursed Eruption", "Проклятое извержение");
						string resultL = resultK.Replace("Double Click", "Двойной клик");
						string resultM = resultL.Replace("Infest", "Заражение");
						string resultN = resultM.Replace("Dazzle", "Ослепление");
						string resultO = resultN.Replace("Wild Magic", "Необузданная магия");
						string resultP = resultO.Replace("Conqueror", "Завоеватель");
						string resultQ = resultP.Replace("Toxic Release", "Токсичный выброс");
						string resultR = resultQ.Replace("Totality", "Тотальность");
						string resultS = resultR.Replace("Sticky Honey", "Липкий мёд");
						string resultT = resultS.Replace("Discharge", "Разряд");
						string resultU = resultT.Replace("Linger", "Промедление");
						string resultV = resultU.Replace("Freeze", "Заморозка");
						string resultW = resultV.Replace("Solar Flare", "Солнечная вспышка");
						string resultX = resultW.Replace("Embrittle", "Хрупкость");
						string resultY = resultX.Replace("Petal Storm", "Лепестковая буря");
						string resultZ = resultY.Replace("Regenerate", "Регенерация");
						string resultAA = resultZ.Replace("Shadow Lash", "Теневая плеть");
						string resultAB = resultAA.Replace("Razor's Edge", "Лезвие бритвы");
						string resultAC = resultAB.Replace("The Click", "Клик");
						string resultAD = resultAC.Replace("Phase Reach", "Фазовая досягаемость");
						string resultAE = resultAD.Replace("Star Storm", "Звёздная буря");
						string resultAF = resultAE.Replace("Splash", "Всплеск");
						string resultAG = resultAF.Replace("Siphon", "Сифон");
						string resultAH = resultAG.Replace("Auto Click", "Авто-клик");
						string resultAI = resultAH.Replace("Curse", "Проклятие");
						string resultAJ = resultAI.Replace("Inferno", "Ад");
						string resultAK = resultAJ.Replace("Stinging Thorn", "Жалящий шип");
						string resultAL = resultAK.Replace("Bombard", "Бомбардировка");
						tooltipLine.text = resultAL;
					}
					
					if (tooltipLine.Name == "PrefixClickerRadius")
					{
						string str = tooltipLine.text;
						string resultA = str.Replace("% base clicker radius", "% базовый радиус курсора");
						tooltipLine.text = resultA;
					}
					
					if (tooltipLine.Name == "PrefixClickBoost")
					{
						string str = tooltipLine.text;
						string resultA = str.Replace("clicks required", "кликов необходимо");
						tooltipLine.text = resultA;
					}
					
					if (tooltipLine.Name == "transformationText")
					{
						string str = tooltipLine.text;
						string resultA = str.Replace("Total clicks", "Всего кликов");
						tooltipLine.text = resultA;
					}
				}
			}
		}
	}
}