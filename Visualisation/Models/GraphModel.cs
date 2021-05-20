using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using Visualisation.Enums;
using EdgeTypeEnum = Visualisation.Enums.EdgeType;

namespace Visualisation.Models
{
    public class GraphModel : BidirectionalGraph<VertexData, EdgeData>
    {
        public const string PATH = @"D:\kursach\file.txt";

        public EdgeTypeEnum[,] MatrixSmezh { get; set; }

        public GraphModel()
        {
            FillVertices();
            FillMatrix();
        }

        private void FillVertices()

        {
            var Vertecies = new List<VertexData>();
            foreach (ReagentType reagent in Enum.GetValues(typeof(ReagentType)))
            {
                foreach (EnvironmentType environment in Enum.GetValues(typeof(EnvironmentType)))
                {
                    foreach (SolventType solvent in Enum.GetValues(typeof(SolventType)))
                    {
                        foreach (PhaseType phase in Enum.GetValues(typeof(PhaseType)))
                        {
                            if (phase == PhaseType.gas)
                            {
                                if (reagent == ReagentType.HCl)
                                {
                                    int T = 850;
                                    float k = 0.78f;
                                    Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, true, ProductType.FeCl2));
                                    do
                                    {
                                        T -= 10;
                                        k /= 3f;
                                        Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    while (k >= 0.001f && T > 672);
                                    for (T = 860; T < 1062; T += 10)
                                    {
                                        Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 672, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1062, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1102, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 684, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 927, false, ProductType.none));
                                }
                                else if (reagent == ReagentType.HBR)
                                {
                                    int T = 850;
                                    float k = 1.25f;
                                    Vertecies.Add(new VertexData(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, true, ProductType.FeBr2));
                                    do
                                    {
                                        T -= 10;
                                        k /= 3f;
                                        Vertecies.Add(new VertexData(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    while (k >= 0.001f && T > 684);
                                    for (T = 860; T < 1102; T += 10)
                                    {
                                        Vertecies.Add(new VertexData(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    Vertecies.Add(new VertexData(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 672, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1062, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1102, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 684, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 927, false, ProductType.none));
                                }
                                else if (reagent == ReagentType.HF)
                                {
                                    int T = 850;
                                    float k = 0.62f;
                                    Vertecies.Add(new VertexData(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, true, ProductType.FeF2));
                                    do
                                    {
                                        T -= 10;
                                        k /= 3f;
                                        Vertecies.Add(new VertexData(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    while (k >= 0.001f && T > 600);
                                    for (T = 860; T < 1200; T += 10)
                                    {
                                        Vertecies.Add(new VertexData(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    Vertecies.Add(new VertexData(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 672, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1062, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1102, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 684, false, ProductType.none));
                                    Vertecies.Add(new VertexData(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 927, false, ProductType.none));
                                }
                                else if (phase == PhaseType.liquid)
                                {
                                    if (reagent == ReagentType.HCl)
                                    {
                                        int T = 20;
                                        float k = 1.04f;
                                        Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, true, ProductType.FeCl2));
                                        do
                                        {
                                            T -= 10;
                                            k /= 3f;
                                            Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                        }
                                        while ((k >= 0.001f) || (T >= 0));
                                        for (T = 30; T <= 100; T += 10)
                                        {
                                            Vertecies.Add(new VertexData(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Vertecies = Vertecies.Where(x => x.Constrain()).ToList();

            foreach(VertexData vertex in Vertecies)
            {
                this.AddVertex(vertex);
            }
        }

        private EdgeTypeEnum TypeOfEdge(VertexData v1, VertexData v2)
        {
            EdgeTypeEnum edge = EdgeTypeEnum.none;
            if ((v1.reagentV != v2.reagentV) && (v1.environmentV == v2.environmentV) && (v1.solventV == v2.solventV) && (v1.phaseV == v2.phaseV) && (v1.TV == v2.TV) && (v1.criticalPointV == CriticalPointType.none) && (v2.criticalPointV == CriticalPointType.none))
            {
                edge = EdgeTypeEnum.A;
            }
            else if ((v1.reagentV != v2.reagentV) && (v1.environmentV == v2.environmentV) && (v1.solventV == v2.solventV) && (v1.phaseV == v2.phaseV) && (v1.TV == v2.TV) && (v1.criticalPointV != CriticalPointType.none || v2.criticalPointV != CriticalPointType.none))
            {
                edge = EdgeTypeEnum.C;
            }
            else if ((v1.reagentV != v2.reagentV) && (v1.environmentV == v2.environmentV) && (v1.solventV == v2.solventV) && (v1.phaseV == v2.phaseV) && (v1.criticalPointV != CriticalPointType.none) && (v2.criticalPointV != CriticalPointType.none) && (v1.criticalPointV == v2.criticalPointV))
            {
                edge = EdgeTypeEnum.D;
            }
            else if (v1.reagentV == v2.reagentV)
            {
                int count = 0;
                if (v1.environmentV != v2.environmentV)
                    count++;
                if (v1.solventV != v2.solventV)
                    count++;
                if (v1.phaseV != v2.phaseV)
                    count++;
                if (v1.TV != v2.TV)
                {
                    if (Math.Abs(v1.TV - v2.TV) <= 10)
                    {
                        count++;
                    }
                    else
                    {
                        return EdgeTypeEnum.none;
                    }
                }

                if (count == 1)
                {
                    edge = EdgeTypeEnum.B;
                }
            }
            return edge;
        }

        private void FillMatrix()
        {
            int length = Vertices.Count();
            MatrixSmezh = new EdgeTypeEnum[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    MatrixSmezh[i, j] = TypeOfEdge(Vertices.ElementAt(i), Vertices.ElementAt(j));
                }
            }
        }
    }
}
