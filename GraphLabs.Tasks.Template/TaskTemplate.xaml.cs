using System;
using GraphLabs.Graphs.UIComponents.Visualization;

namespace GraphLabs.Tasks.Template
{
    public partial class TaskTemplate
    {
        /// <summary> Ctor. </summary>
        public TaskTemplate()
        {
            InitializeComponent();
        }

        /// <summary> Клик по вершине </summary>
        public event EventHandler<VertexClickEventArgs> VertexClicked;

        private void OnVertexClicked(VertexClickEventArgs e)
        {
            var handler = VertexClicked;
            handler?.Invoke(this, e);
        }

        private void OnVertexClick(object sender, VertexClickEventArgs e)
        {
            OnVertexClicked(e);
        }
    }
}
