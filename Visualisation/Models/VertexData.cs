using GraphX.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visualisation.Models
{
    public class VertexData : VertexBase
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public VertexData() : this(string.Empty) { }

        public VertexData(string text = "")
        {
            Text = text;
        }
    }
}
