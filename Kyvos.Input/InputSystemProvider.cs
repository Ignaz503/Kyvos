using DefaultEcs.System;
using DefaultEcs.Threading;

namespace Kyvos.Input;

public struct InputSystemProvider
{
    public ISystem<InputSystemContext>[] Systems { get; init; }

    public ParallelContext Context { get; init; }

    public ISystem<InputSystemContext> GetSystem()
    {
        if (Context.Parallel)
        {
            return new ParallelSystem<InputSystemContext>(Context.ParallelRunner!,Systems);
        }
        else
        {
            return new SequentialSystem<InputSystemContext>(Systems);
        }
        
    }

    public struct ParallelContext 
    {
        public IParallelRunner? ParallelRunner;
        public bool Parallel => ParallelRunner is not null;

        public ParallelContext(IParallelRunner runner)
        {
            this.ParallelRunner = runner;
        }

    }

}


