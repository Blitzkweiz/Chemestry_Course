using GraphX.Common.Enums;
using GraphX.Controls;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using System;
using System.Windows;
using Visualisation.Models;

namespace Visualisation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        public MainWindow()
        {
            InitializeComponent();

            ZoomControl.SetViewFinderVisibility(zoomctrl, Visibility.Visible);

            zoomctrl.ZoomToFill();
            GraphAreaModel_Setup();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gg_but_randomgraph_Click(null, null);
        }

        private void gg_but_relayout_Click(object sender, RoutedEventArgs e)
        {
            Area.RelayoutGraph();
            zoomctrl.ZoomToFill();
        }

        private void gg_but_randomgraph_Click(object sender, RoutedEventArgs e)
        {
            Area.GenerateGraph(true, true);
            Area.SetEdgesDashStyle(EdgeDashStyle.Solid);
            Area.ShowAllEdgesArrows(false);
            Area.ShowAllEdgesLabels(true);

            zoomctrl.ZoomToFill();
        }

        private GraphModel Graph_Setup()
        {
            var graphData = new GraphModel();

            Utils.Utils.CreateEdges(ref graphData);

            return graphData;
        }

        private void GraphAreaModel_Setup()
        {
            var logicCore = new GXLogicCoreModel() { Graph = Graph_Setup() };

            logicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.LinLog;
            logicCore.DefaultLayoutAlgorithmParams = logicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.LinLog);

            logicCore.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            logicCore.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 100;
            logicCore.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 100;

            logicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;

            logicCore.AsyncAlgorithmCompute = false;

            Area.LogicCore = logicCore;
        }

        public void Dispose()
        {
            Area.Dispose();
        }
    }
}
