using System.Reflection;

namespace TournamentsEnhanced
{
  public static class ModuleConstants
  {
    public static readonly string Name = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
    public static readonly string ProductName = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product;
    public static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
  }
}