using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GraphLabs.Common.Utils;
using GraphLabs.Graphs;

/// <summary> Комбинация визуализаторов для изоморфизма </summary>

namespace GraphLabs.Tasks.Template
{
    public partial class IsomorphismVisualizer : UserControl
    {
        /// <summary> Визуализатор для изоморфизма </summary>
        public IsomorphismVisualizer()
        {
            InitializeComponent();
            WorkspaceVisualizer.VertexReleased += (s, e) =>
            {
                IsomorphismResult = Calculate();
            };
        }

        #region Рабочий граф

        /// <summary> Рабочий граф </summary>
        public static DependencyProperty WorkspaceGraphProperty =
            DependencyProperty.Register(
                "WorkspaceGraph",
                typeof(IObservableGraph),
                typeof(IsomorphismVisualizer),
                new PropertyMetadata(WorkspaceGraphChanged));

        /// <summary> Рабочий граф </summary>
        public IObservableGraph WorkspaceGraph
        {
            get
            {
                return (IObservableGraph)GetValue(WorkspaceGraphProperty);
            }
            set
            {
                UpdateLayout();
                SetValue(WorkspaceGraphProperty, value);
            }
        }

        private static void WorkspaceGraphChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                ((IsomorphismVisualizer)d).WorkspaceVisualizer.Graph = (IObservableGraph)e.NewValue;
            }
        }

        #endregion

        #region Граф на бэкграунде

        /// <summary> Граф на бэкграунде </summary>
        public static DependencyProperty BackgroundGraphProperty =
            DependencyProperty.Register(
                "BackgroundGraph",
                typeof(IObservableGraph),
                typeof(IsomorphismVisualizer),
                new PropertyMetadata(BackgroundGraphChanged));

        /// <summary> Граф на бэкграунде </summary>
        public IObservableGraph BackgroundGraph
        {
            get
            {
                return (IObservableGraph)GetValue(BackgroundGraphProperty);
            }
            set
            {
                UpdateLayout();
                SetValue(BackgroundGraphProperty, value);
            }
        }

        private static void BackgroundGraphChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var i = d as IsomorphismVisualizer;
            i.BackgroundVisualizer.Graph = (IObservableGraph)e.NewValue;
            i.WorkspaceVisualizer.Glue = i.BackgroundVisualizer.GetVertexesCoordinates();
            i.UpdateLayout();
        }

        #endregion

        #region Видимость

        /// <summary> Видимость рабочего графа </summary>
        public static DependencyProperty WorkspaceGraphVisibilityProperty =
            DependencyProperty.Register(
                "WorkspaceGraphVisibility",
                typeof(Visibility),
                typeof(IsomorphismVisualizer),
                new PropertyMetadata(WorkspaceGraphVisibilityChanged));

        /// <summary> Видимость рабочего графа </summary>
        public Visibility WorkspaceGraphVisibility
        {
            get
            {
                return (Visibility)GetValue(VisibilityProperty);
            }
            set
            {
                SetValue(VisibilityProperty, value);
            }
        }

        private static void WorkspaceGraphVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var i = (IsomorphismVisualizer) d;
            i.WorkspaceVisualizer.Visibility = (Visibility) e.NewValue;
            if (((Visibility) e.NewValue) == Visibility.Collapsed) return;
            i.WorkspaceVisualizer.Glue = i.BackgroundVisualizer.GetVertexesCoordinates();
            i.WorkspaceVisualizer.UpdateColors();
        }

        #endregion

        /// <summary> Совмещены ли два графа? </summary>
        public static DependencyProperty IsomorphismResultProperty =
            DependencyProperty.Register(
                ExpressionUtility.NameForMember((IsomorphismVisualizer m) => m.IsomorphismResult),
                typeof(bool),
                typeof(IsomorphismVisualizer),
                new PropertyMetadata(default(bool)));

        public bool Calculate()
        {
            if (BackgroundVisualizer.Graph == null ||
                    WorkspaceVisualizer.Graph == null ||
                    BackgroundVisualizer.Graph.VerticesCount != WorkspaceVisualizer.Graph.VerticesCount ||
                    BackgroundVisualizer.Graph.EdgesCount != WorkspaceVisualizer.Graph.EdgesCount)
                return false;
            var result = true;
            var vertexesOrder = new ObservableCollection<Vertex>();
            var bgPoints = BackgroundVisualizer.GetVertexesCoordinates();
            var wsPoints = WorkspaceVisualizer.GetVertexesCoordinates();
            var bgGraph = BackgroundVisualizer.Graph;
            var wsGraph = WorkspaceVisualizer.Graph;
            for (var i = 0; i < bgGraph.VerticesCount; i++)
            {
                var point = wsPoints.SingleOrDefault(p => p.X == bgPoints[i].X && p.Y == bgPoints[i].Y);
                if (point == default(Point))
                    return false;
                var index = wsPoints.IndexOf(point);
                vertexesOrder.Add((Vertex)wsGraph.Vertices[index]);
            }
            for (var i = 0; i < bgGraph.VerticesCount; i++)
                for (var j = 0; j < bgGraph.VerticesCount; j++)
                    if (i != j)
                        result &= bgGraph[bgGraph.Vertices[i], bgGraph.Vertices[j]] != null ||
                                  wsGraph[vertexesOrder[i], vertexesOrder[j]] == null;
            return result;
        }

        public bool IsomorphismResult
        {
            get { return (bool) GetValue(IsomorphismResultProperty); }
            set { SetValue(IsomorphismResultProperty, value); }
        }
    }
}