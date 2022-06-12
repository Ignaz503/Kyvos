using Kyvos.Maths.Graphs.Noise.Nodes.InputLabelResolvers;

namespace Kyvos.Maths.Graphs.Noise.Nodes.Base;

    public abstract class ThreeChildNode<TLabel> : FixedChildSizeNode<TLabel>
    {
        protected INoiseGraphNode<TLabel> FirstChild => Children[0];
        protected INoiseGraphNode<TLabel> SecondChild => Children[1];
        protected INoiseGraphNode<TLabel> ThirdChild => Children[2];


        public ThreeChildNode(IChildLabelToIndexResolver resolver) : base( 3, resolver)
        {

        }

    }

