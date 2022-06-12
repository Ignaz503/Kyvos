namespace Kyvos.Maths.Graphs.Noise;
public partial class NoiseGraph
{
    public interface INodeSetupPhase<TLabel>
    {
        public INodeCreationPhase<TLabel> MarkRoot();
        public INodeCreationPhase<TLabel> InputOf(TLabel label);

        public INodeCreationPhase<TLabel> InputOfAt(TLabel label, int at);

        public INodeCreationPhase<TLabel> InputOfAs(TLabel label, string inputName);

        public INodeSetupPhase<TLabel> InputFrom(TLabel label);
        public INodeSetupPhase<TLabel> InputFromAt(TLabel label, int newChildIdx);
        public INodeSetupPhase<TLabel> InputFromAs(TLabel label, string childLocationName);
    }

}
