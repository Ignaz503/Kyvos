using Kyvos.Maths.Graphs.Noise.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kyvos.Maths.Graphs.Noise;
public partial class NoiseGraph
{
    //Make generic for own labling system
    public class Builder<TLabel> : INodeCreationPhase<TLabel>, INodeSetupPhase<TLabel>
    {
        struct RelationshipPromise
        {
            public TLabel WantsRelationShip;
            public Action<INoiseGraphNode<TLabel>> ReslationshipEstablishAction;
        }

        INoiseGraphNode<TLabel> root;
        INoiseGraphNode<TLabel> context;

        List<INoiseGraphNode<TLabel>> allCreatedNodes = new();

        Dictionary<TLabel, List<RelationshipPromise>> relationshipPromises = new();


        private Builder() { }

        public static INodeCreationPhase<TLabel> Get() => new Builder<TLabel>();

        INoiseGraphNode<TLabel> FindNode(TLabel label)
        {
            var n = allCreatedNodes.Find(n => n.Label.Equals(label));
            return n;
        }

        public NoiseGraph Build()
        {
            if (allCreatedNodes.Count == 0)
                throw new InvalidOperationException("Can't build graph without nodes");
            if (root == null)
            {
                if (context == null)
                    throw new InvalidOperationException("Can't build graph, as there is no root set, or context Node");
                MarkRoot();
            }
            Validate();
            return new NoiseGraph(root);
        }

        private void Validate()
        {
            if (relationshipPromises.Count > 0)
                throw new InvalidGraphException($"There are still unresolved relationship promises for nodes:\n{BuildUnresolvedRelationshipString()}");

            var labelAccumulator = new HashSet<TLabel>();

            root.Validate((s) => labelAccumulator.Add(s));

            foreach (var node in allCreatedNodes)
            {
                if (!labelAccumulator.Contains(node.Label))
                {
                    throw new OrphanedNodeException<TLabel>(node.Label);
                }
            }

        }

        string BuildUnresolvedRelationshipString()
        {
            StringBuilder b = new();

            foreach (var promise in relationshipPromises)
            {
                StringBuilder item = new();
                item.Append($"Nodes [");
                for (int i = 0; i < promise.Value.Count; i++)
                {
                    if (i == promise.Value.Count - 1)
                    {
                        item.Append($" {promise.Value[i].WantsRelationShip} ]");
                    }
                    else
                    {
                        item.Append($" {promise.Value[i].WantsRelationShip},");
                    }
                }
                item.Append($"want relation with node '{promise.Key}'");
                b.AppendLine(item.ToString());
            }

            return b.ToString();
        }

        public INodeSetupPhase<TLabel> CreateNode(TLabel lable, Func<INoiseGraphNode<TLabel>> builder)
        {
            if (allCreatedNodes.Any(n => n.Label.Equals(lable)))
            {
                throw new MultiLabelUseException<TLabel>(lable);
            }

            var n = builder();
            n.Label = lable;
            context = n;
            allCreatedNodes.Add(n);

            if (relationshipPromises.ContainsKey(lable))
            {
                foreach (var promise in relationshipPromises[lable])
                    promise.ReslationshipEstablishAction(context);

                relationshipPromises.Remove(lable);
            }

            return this;
        }

        public INodeCreationPhase<TLabel> MarkRoot()
        {
            root = context;
            return this;
        }

        void AddRelationPromise(TLabel label, Action<INoiseGraphNode<TLabel>> action)
        {
            if (relationshipPromises.ContainsKey(label))
            {
                relationshipPromises[label].Add(new() { WantsRelationShip = context.Label, ReslationshipEstablishAction = action });
            }
            else
            {
                relationshipPromises.Add(label, new() { new() { WantsRelationShip = context.Label, ReslationshipEstablishAction = action } });
            }
        }

        public INodeCreationPhase<TLabel> InputOf(TLabel label)
        {
            var n = FindNode(label);

            if (n == null)
            {
                var closure = context;
                AddRelationPromise(label, node => node.AppendChild(closure));
                return this;
            }


            n.AppendChild(context);
            return this;
        }

        public INodeCreationPhase<TLabel> InputOfAt(TLabel label, int at)
        {
            var n = FindNode(label);
            if (n == null)
            {
                var closure = context;
                AddRelationPromise(label, node => node.AppendChildAt(closure, at));
                return this;
            }
            n.AppendChildAt(context, at);
            return this;
        }

        public INodeCreationPhase<TLabel> InputOfAs(TLabel label, string inputName)
        {
            var n = FindNode(label);
            if (n == null)
            {
                var closure = context;
                AddRelationPromise(label, node => node.AppendChildAs(closure, inputName));
                return this;
            }
            n.AppendChildAs(context, inputName);
            return this;
        }

        public INodeSetupPhase<TLabel> InputFrom(TLabel label)
        {
            HandleInputFromInsertion(label, (inserted, node) => inserted.AppendChild(node));

            return this;
        }

        void HandleInputFromInsertion(INoiseGraphNode<TLabel> inserted, INoiseGraphNode<TLabel> newChild, Action<INoiseGraphNode<TLabel>, INoiseGraphNode<TLabel>> appendAction)
        {
            if (newChild == root)
                root = inserted;

            var priorParent = allCreatedNodes.FirstOrDefault(p => p.HasChild(newChild.Label));

            if (priorParent is not null)
            {
                var idx = priorParent.IdxOfChild(newChild.Label);
                priorParent.AppendChildAt(inserted, idx);
                appendAction(inserted, newChild);

            }
            else
            {
                appendAction(inserted, newChild);
            }
        }

        void HandleInputFromInsertion(TLabel label, Action<INoiseGraphNode<TLabel>, INoiseGraphNode<TLabel>> appendAction)

        {
            var n = FindNode(label);

            if (n is null)
            {
                var closure = context;//we want to reference what context is referencing, not reference context itself
                AddRelationPromise(label, node => HandleInputFromInsertion(closure, node, appendAction));
            }
            HandleInputFromInsertion(context, n, appendAction);
        }

        public INodeSetupPhase<TLabel> InputFromAt(TLabel label, int newChildIdx)
        {
            HandleInputFromInsertion(label, (inserted, node) => inserted.AppendChildAt(node, newChildIdx));

            return this;
        }

        public INodeSetupPhase<TLabel> InputFromAs(TLabel label, string inputName)
        {
            HandleInputFromInsertion(label, (inserted, node) => inserted.AppendChildAs(node, inputName));

            return this;
        }

        public INodeSetupPhase<TLabel> Select(TLabel label)
        {
            var n = FindNode(label);
            if (n == null)
                throw new UnusedLableException<TLabel>(label);
            context = n;
            return this;
        }
    }

}


