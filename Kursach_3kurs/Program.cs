using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Kursach_3kurs.Enums;

namespace Kursach_3kurs
{
    public class Vertex
    {
        public ReagentType reagentV;
        public ProductType productV;
        public EnvironmentType environmentV;
        public SolventType solventV;
        public PhaseType phaseV;
        public int TV;
        public bool isResearchedV;
        public CriticalPointType criticalPointV = CriticalPointType.none;
        public Vertex(ReagentType reagentV, EnvironmentType environmentV, SolventType solventV, PhaseType phaseV, int TV, bool isResearchedV = false, ProductType productV = ProductType.none)
        {
            this.reagentV = reagentV;
            this.productV = productV;
            this.environmentV = environmentV;
            this.solventV = solventV;
            this.phaseV = phaseV;
            this.TV = TV;
            this.isResearchedV = isResearchedV;
            if (reagentV == ReagentType.HCl)
            {
                if (TV == 672)
                {
                    criticalPointV = CriticalPointType.boiling;
                }
                else if (TV == 1062)
                    criticalPointV = CriticalPointType.melting;
            }

            if (reagentV == ReagentType.HBR)
            {
                if (TV == 684)
                {
                    criticalPointV = CriticalPointType.boiling;
                }
                else if (TV == 927)
                    criticalPointV = CriticalPointType.melting;
            }

            if (reagentV == ReagentType.HF)
            {
                if (TV == 1102)
                {
                    criticalPointV = CriticalPointType.boiling;
                }
            }
        }

        public bool constrain()
        {
            bool isReal = true;

            if ((reagentV == ReagentType.HBR) && ((productV == ProductType.FeF2) || (productV == ProductType.FeCl2)))
            {
                return false;
            }
            if ((reagentV == ReagentType.HCl) && ((productV == ProductType.FeF2) || (productV == ProductType.FeBr2)))
            {
                return false;
            }
            if ((reagentV == ReagentType.HF) && ((productV == ProductType.FeCl2) || (productV == ProductType.FeBr2)))
            {
                return false;
            }

            if (phaseV == PhaseType.gas && (solventV == SolventType.H2O || environmentV == EnvironmentType.inertAndWet))
            {
                return false;
            }

            if (phaseV == PhaseType.liquid && environmentV == EnvironmentType.oxidizingAndDry)
            {
                return false;
            }

            if ((reagentV == ReagentType.HBR) && (phaseV == PhaseType.gas) && ((TV < 684) || (TV > 927)))
            {
                return false;
            }
            if ((reagentV == ReagentType.HCl) && (phaseV == PhaseType.gas) && ((TV < 672) || (TV > 1062)))
            {
                return false;
            }

            if ((solventV == SolventType.H2O) && ((TV < 0) || (TV > 100)))
            {
                return false;
            }

            return isReal;
        }


        public void printVertex()
        {
            System.Console.WriteLine(this.ToString());
        }

        public void writeInFile(string path, int i)
        {
            FileStream file = new FileStream(path, FileMode.Append);
            StreamWriter writer = new StreamWriter(file);

            writer.WriteLine(i + this.ToString());

            writer.Close();
        }

        public override string ToString()
        {
            if (isResearchedV)
            {
                return $"Vertex: Fe + {reagentV} = {productV} + H2 \t environment = {environmentV} \t solvent = {solventV} \t phase = {phaseV} \t Temperature = {TV} \t Researched? {isResearchedV} \t CriticalPoint = {criticalPointV}";
            }
            else
            {
                return $"Vertex: Fe + {reagentV} = {productV} \t environment = {environmentV} \t solvent = {solventV} \t phase = {phaseV} \t Temperature = {TV} \t Researched? {isResearchedV} \t CriticalPoint = {criticalPointV}";
            }
        }



    }

    public class Graph
    {
        public const string PATH = @"D:\kursach\file.txt";

        public List<Vertex> Vertecies { get; set; }
        public EdgeType[,] MatrixSmezh { get; set; }

        public Graph()
        {
            FillVertices();
            FillMatrix();
        }

        private void FillVertices()

        {
            Vertecies = new List<Vertex>(); 
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
                                    Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, true, ProductType.FeCl2));
                                    do
                                    {
                                        T -= 10;
                                        k /= 3f;
                                        Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    while (k >= 0.001f && T > 672);
                                    for (T = 860; T < 1062; T += 10)
                                    {
                                        Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 672, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1062, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1102, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 684, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 927, false, ProductType.none));
                                }
                                else if (reagent == ReagentType.HBR)
                                {
                                    int T = 850;
                                    float k = 1.25f;
                                    Vertecies.Add(new Vertex(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, true, ProductType.FeBr2));
                                    do
                                    {
                                        T -= 10;
                                        k /= 3f;
                                        Vertecies.Add(new Vertex(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    while (k >= 0.001f && T > 684);
                                    for (T = 860; T < 1102; T += 10)
                                    {
                                        Vertecies.Add(new Vertex(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    Vertecies.Add(new Vertex(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 672, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1062, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1102, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 684, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HBR, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 927, false, ProductType.none));
                                }
                                else if (reagent == ReagentType.HF)
                                {
                                    int T = 850;
                                    float k = 0.62f;
                                    Vertecies.Add(new Vertex(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, true, ProductType.FeF2));
                                    do
                                    {
                                        T -= 10;
                                        k /= 3f;
                                        Vertecies.Add(new Vertex(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    while (k >= 0.001f && T > 600);
                                    for (T = 860; T < 1200; T += 10)
                                    {
                                        Vertecies.Add(new Vertex(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                    }
                                    Vertecies.Add(new Vertex(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 672, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1062, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 1102, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 684, false, ProductType.none));
                                    Vertecies.Add(new Vertex(ReagentType.HF, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, 927, false, ProductType.none));
                                }
                                else if (phase == PhaseType.liquid)
                                {
                                    if (reagent == ReagentType.HCl)
                                    {
                                        int T = 20;
                                        float k = 1.04f;
                                        Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, true, ProductType.FeCl2));
                                        do
                                        {
                                            T -= 10;
                                            k /= 3f;
                                            Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                        }
                                        while ((k >= 0.001f) || (T >= 0));
                                        for (T = 30; T <= 100; T += 10)
                                        {
                                            Vertecies.Add(new Vertex(ReagentType.HCl, EnvironmentType.oxidizingAndDry, SolventType.none, PhaseType.gas, T, false, ProductType.none));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Vertecies = Vertecies.Where(x => x.constrain()).ToList();
        }

        private EdgeType TypeOfEdge(Vertex v1, Vertex v2)
        {
            EdgeType edge = EdgeType.none;
            if ((v1.reagentV != v2.reagentV) && (v1.environmentV == v2.environmentV) && (v1.solventV == v2.solventV) && (v1.phaseV == v2.phaseV) && (v1.TV == v2.TV) && (v1.criticalPointV == CriticalPointType.none) && (v2.criticalPointV == CriticalPointType.none))
            {
                edge = EdgeType.A;
            }
            else if ((v1.reagentV != v2.reagentV) && (v1.environmentV == v2.environmentV) && (v1.solventV == v2.solventV) && (v1.phaseV == v2.phaseV) && (v1.TV == v2.TV) && (v1.criticalPointV != CriticalPointType.none || v2.criticalPointV != CriticalPointType.none))
            {
                edge = EdgeType.C;
            }
            else if ((v1.reagentV != v2.reagentV) && (v1.environmentV == v2.environmentV) && (v1.solventV == v2.solventV) && (v1.phaseV == v2.phaseV) && (v1.criticalPointV != CriticalPointType.none) && (v2.criticalPointV != CriticalPointType.none) && (v1.criticalPointV == v2.criticalPointV))
            {
                edge = EdgeType.D;
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
                        return EdgeType.none;
                    }
                }

                if (count == 1)
                {
                    edge = EdgeType.B;
                }
            }
            return edge;
        }

        private void FillMatrix()
        {
            int length = Vertecies.Count;
            MatrixSmezh = new EdgeType[length, length];   
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    MatrixSmezh[i, j] = TypeOfEdge(Vertecies[i], Vertecies[j]);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\kursach\file.txt";

            var graph = new Graph();

            foreach (Vertex el in graph.Vertecies)
            {
                el.printVertex();
                el.writeInFile(path, graph.Vertecies.IndexOf(el));
            }

        }
    }
}