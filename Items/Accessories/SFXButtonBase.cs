using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public abstract class SFXButtonBase : ClickerItem
	{
		public static readonly int StackAmount = 5;

		public static LocalizedText DefaultTooltipText { get; private set; }

		public override LocalizedText Tooltip => DefaultTooltipText.WithFormatArgs(StackAmount);

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			DefaultTooltipText ??= Language.GetOrRegister(Mod.GetLocalizationKey("Common.Tooltips.SFXButtonTip"));
		}

		public sealed override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.maxStack = 5;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Green;
			
			SafeSetDefaults();
		}
		
		public virtual void SafeSetDefaults()
		{

		}
	}
}
