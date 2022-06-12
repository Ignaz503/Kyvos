using Kyvos.Core;
using Kyvos.Core.Configuration;

namespace FullyAutomatedGayLuxurySpaceCommunism;

public abstract class ConfigurableEngineTest : IEngineTest
{
    protected IConfig? config;
    public ConfigurableEngineTest()
    {
        config = null;  
    }

    public ConfigurableEngineTest(IConfig config)
    {
        this.config = config;
    }

    protected abstract IModifyableApplication ApplySetup(IModifyableApplication app);

    public virtual IModifyableApplication BuildApp()
        => ApplySetup(config is null ? IEngineTest.DefaultAppSetup() : IEngineTest.DefaultAppSetup(config));
}
