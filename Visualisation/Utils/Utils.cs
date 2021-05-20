using System.Linq;
using Visualisation.Enums;
using Visualisation.Models;

namespace Visualisation.Utils
{
    public static class Utils
    {
        public static void CreateEdges(ref GraphModel graphModel)
        {
            for (int i = 0; i < graphModel.Vertices.Count(); i++)
            {
                for (int j = i; j < graphModel.Vertices.Count(); j++)
                {
                    if (graphModel.MatrixSmezh[i, j] != EdgeType.none)
                    {
                        graphModel.AddEdge(new EdgeData(graphModel.Vertices.ElementAt(i), graphModel.Vertices.ElementAt(j), graphModel.MatrixSmezh[i, j]));
                    }
                }
            }
        }
    }
}
