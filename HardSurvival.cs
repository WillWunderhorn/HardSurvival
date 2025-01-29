using MelonLoader;

namespace LowHealth
{
    public class Implementation : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Createb by: " + BuildInfo.ModAuthor);
        }
    }
}