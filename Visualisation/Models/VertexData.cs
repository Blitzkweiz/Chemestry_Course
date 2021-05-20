using GraphX.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Visualisation.Enums;

namespace Visualisation.Models
{
    public class VertexData : VertexBase
    {
        public ReagentType reagentV;
        public ProductType productV;
        public EnvironmentType environmentV;
        public SolventType solventV;
        public PhaseType phaseV;
        public int TV;
        public bool isResearchedV;
        public CriticalPointType criticalPointV = CriticalPointType.none;
        public VertexData(ReagentType reagentV, EnvironmentType environmentV, SolventType solventV, PhaseType phaseV, int TV, bool isResearchedV = false, ProductType productV = ProductType.none)
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

        public bool Constrain()
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


        public void PrintVertex()
        {
            System.Console.WriteLine(this.ToString());
        }

        public void WriteInFile(string path, int i)
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
                return $"Vertex: Fe + {reagentV} = {productV} + H2 \n environment = {environmentV} \n solvent = {solventV} \n phase = {phaseV} \n Temperature = {TV} \n Researched? {isResearchedV} \n CriticalPoint = {criticalPointV}";
            }
            else
            {
                return $"Vertex: Fe + {reagentV} = {productV} \n environment = {environmentV} \n solvent = {solventV} \n phase = {phaseV} \n Temperature = {TV} \n Researched? {isResearchedV} \n CriticalPoint = {criticalPointV}";
            }
        }
    }
}
