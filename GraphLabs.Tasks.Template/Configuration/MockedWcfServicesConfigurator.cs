using GraphLabs.Common.VariantProviderService;
using GraphLabs.CommonUI.Configuration;
using GraphLabs.Graphs;
using GraphLabs.Graphs.DataTransferObjects.Converters;

namespace GraphLabs.Tasks.Template.Configuration
{
    /// <summary> Конфигуратор заглушек wcf-сервисов </summary>
    public class MockedWcfServicesConfigurator : MockedWcfServicesConfiguratorBase
    {
        /// <summary> Сгенерировать отладочный вариант </summary>
        protected override TaskVariantDto GetDebugVariant()
        {
            var debugGraph1 = UndirectedGraph.CreateEmpty(7);
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[0], debugGraph1.Vertices[1]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[0], debugGraph1.Vertices[6]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[0], debugGraph1.Vertices[2]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[0], debugGraph1.Vertices[3]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[1], debugGraph1.Vertices[2]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[1], debugGraph1.Vertices[4]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[1], debugGraph1.Vertices[6]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[2], debugGraph1.Vertices[3]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[2], debugGraph1.Vertices[5]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[3], debugGraph1.Vertices[5]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[3], debugGraph1.Vertices[6]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[4], debugGraph1.Vertices[5]));
            debugGraph1.AddEdge(new UndirectedEdge(debugGraph1.Vertices[4], debugGraph1.Vertices[6]));
            var debugGraph2 = UndirectedGraph.CreateEmpty(7);
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[0], debugGraph2.Vertices[1]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[0], debugGraph2.Vertices[6]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[0], debugGraph2.Vertices[2]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[0], debugGraph2.Vertices[3]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[1], debugGraph2.Vertices[2]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[1], debugGraph2.Vertices[4]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[1], debugGraph2.Vertices[6]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[2], debugGraph2.Vertices[3]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[2], debugGraph2.Vertices[5]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[3], debugGraph2.Vertices[5]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[3], debugGraph2.Vertices[6]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[4], debugGraph2.Vertices[5]));
            debugGraph2.AddEdge(new UndirectedEdge(debugGraph2.Vertices[4], debugGraph2.Vertices[6]));
            var serializedVariant = VariantSerializer.Serialize(new IGraph[]
            {
                debugGraph1,
                debugGraph2
            } );

            return new TaskVariantDto
            {
                Data = serializedVariant,
                GeneratorVersion = "1.0",
                Number = "Debug",
                Version = 1
            };
        }
    }
}