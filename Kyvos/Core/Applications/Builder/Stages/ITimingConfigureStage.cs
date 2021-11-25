namespace Kyvos.Core.Applications.Builder.Stages
{
    public interface ITimingConfigureStage
    {
        IApplicationResourceManagerSetupStage WithTiming( Time.Config config );
    }

}
