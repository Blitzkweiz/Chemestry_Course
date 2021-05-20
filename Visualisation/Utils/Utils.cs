using Kursach_3kurs;
using Kursach_3kurs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visualisation.Models;

namespace Visualisation.Utils
{
    public static class Utils
    {
        public static void CreateEdges(ref GraphModel graphModel, EdgeType[,] matrixSmezh, int length)
        {
            var vertecies = graphModel.Vertices.ToList();

            for (int i = 0; i < length; i++)
            {
                for (int j = i; j < length; j++)
                {
                    if(matrixSmezh[i, j] != EdgeType.none)
                    {
                        graphModel.AddEdge(new EdgeData(vertecies[i], vertecies[j]));
                    }
                }
            }
        }

        public static void CreateVertecies(ref GraphModel graphModel, List<Vertex> vertices)
        {
            int i = 0;
            foreach (Vertex vertex in vertices)
            {
                graphModel.AddVertex(new VertexData(i.ToString()));
                i++;
            }
        }
    }
}
