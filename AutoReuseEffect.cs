namespace ClickerClass
{
	/// <summary>
	/// Represents an auto-reuse effect, most notably used for autoclicking
	/// </summary>
	/// <param name="SpeedFactor">The multiplier to use time of 1 when this effect is active. Rounds down the result, minimum is 2 (game limitation). Example: 6f -> effective use time = 1 * 6f -> 60 / 6f -> 10 cps</param>
	/// <param name="ControlledByKeyBind">This effect will only be active if the player toggles it using the keybind from Clicker Class dedicated to it</param>
	/// <param name="PreventsClickEffects">This effect will not activate Click Effects while active</param>
	public record struct AutoReuseEffect(float SpeedFactor, bool ControlledByKeyBind = false, bool PreventsClickEffects = false);
}
