using GraphX.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Visualisation.Enums;

namespace Visualisation.Models
{
    public class EdgeData : EdgeBase<VertexData>
    {
        public string Text { get; set; }

        public EdgeData(VertexData source, VertexData target, EdgeType edgeType = EdgeType.none, double weight = 1) : base(source, target, weight) 
        {
            Text = edgeType.ToString();
        }

        public EdgeData() : base(null, null, 1) { }

        public override string ToString()
        {
            return Text;
        }
    }
}
