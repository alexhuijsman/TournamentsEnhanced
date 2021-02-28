namespace TournamentsEnhanced.Wrappers
{
  public class Harmony : HarmonyLib.Harmony
  {
    public Harmony(string id) : base(id)
    {
    }

    public new void PatchAll()
    {

    }
  }
}