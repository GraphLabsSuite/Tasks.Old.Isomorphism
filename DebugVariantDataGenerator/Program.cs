using System;
using System.IO;
using GraphLabs.Graphs;
using GraphLabs.Graphs.DataTransferObjects.Converters;

namespace DebugVariantDataGenerator
{
    /// <summary>
    /// Это генератор данных тестового варианта для отладки на сайте.
    /// Если не получается запустить его из-за ошибки вида
    /// "Could not copy the file "...\GraphLabs.Tasks.Template\Properties\DebugVariantData.bin" because it was not found.",
    /// то нужно временно выгрузить проект модуля-задания (правой кнопкой по проекту -> UnloadProject)
    /// </summary>
    class Program
    {
        public static byte[] GetSerializedVariant()
        {
            var graph1 = DirectedGraph.CreateEmpty(7);
            graph1.AddEdge(new DirectedEdge(graph1.Vertices[0], graph1.Vertices[5]));
            graph1.AddEdge(new DirectedEdge(graph1.Vertices[1], graph1.Vertices[0]));
            graph1.AddEdge(new DirectedEdge(graph1.Vertices[1], graph1.Vertices[5]));
            graph1.AddEdge(new DirectedEdge(graph1.Vertices[2], graph1.Vertices[1]));
            graph1.AddEdge(new DirectedEdge(graph1.Vertices[2], graph1.Vertices[5]));
            graph1.AddEdge(new DirectedEdge(graph1.Vertices[3], graph1.Vertices[4]));
            graph1.AddEdge(new DirectedEdge(graph1.Vertices[4], graph1.Vertices[2]));
            graph1.AddEdge(new DirectedEdge(graph1.Vertices[4], graph1.Vertices[3]));
            graph1.AddEdge(new DirectedEdge(graph1.Vertices[5], graph1.Vertices[6]));
            graph1.AddEdge(new DirectedEdge(graph1.Vertices[6], graph1.Vertices[4]));

            var graph2 = DirectedGraph.CreateEmpty(7);
            graph2.AddEdge(new DirectedEdge(graph2.Vertices[0], graph2.Vertices[5]));
            graph2.AddEdge(new DirectedEdge(graph2.Vertices[1], graph2.Vertices[0]));
            graph2.AddEdge(new DirectedEdge(graph2.Vertices[1], graph2.Vertices[5]));
            graph2.AddEdge(new DirectedEdge(graph2.Vertices[2], graph2.Vertices[1]));
            graph2.AddEdge(new DirectedEdge(graph2.Vertices[2], graph2.Vertices[5]));
            graph2.AddEdge(new DirectedEdge(graph2.Vertices[3], graph2.Vertices[4]));
            graph2.AddEdge(new DirectedEdge(graph2.Vertices[4], graph2.Vertices[2]));
            graph2.AddEdge(new DirectedEdge(graph2.Vertices[4], graph2.Vertices[3]));
            graph2.AddEdge(new DirectedEdge(graph2.Vertices[5], graph2.Vertices[6]));
            graph2.AddEdge(new DirectedEdge(graph2.Vertices[6], graph2.Vertices[4]));

            return VariantSerializer.Serialize(new IGraph[]
            {
                graph1,
                graph2
            });
        }

        static void Main(string[] args)
        {
            File.WriteAllBytes(@"..\..\..\GraphLabs.Tasks.Template\Debug\DebugVariantData.bin", GetSerializedVariant());
        }
    }
}