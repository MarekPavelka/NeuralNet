using System;
using System.IO;
using System.Linq;
using System.Threading;


namespace NeuralNET
{
    class Program
    {
        private static TrainingSample[] _trainingSamples = new[]
        {
            new TrainingSample(new double []{0,0}, new double []{0}),
            new TrainingSample(new double []{0,1}, new double []{1}),
            new TrainingSample(new double []{1,0}, new double []{1}),
            new TrainingSample(new double []{1,1}, new double []{0}),
        };

        static GeneticAlgo geneticAlgoForXOR = new GeneticAlgo(30, 100, 3);

        static void Main()
        {
            var chart = SimpleChart.SimpleChart.LaunchInNewThread();
            chart.Title = "Learning Progress";

            var populationCount = 100;
            var generation0 = Enumerable.Range(0, populationCount).Select(_ => new NeuralNetwork(2, 5, 1)).ToArray();
            var improvedGeneration = generation0;

            for (int i = 0; i < 10000000; i++)
            {
                improvedGeneration = Train(improvedGeneration);
                var bestNetScoreFrom10Generations = improvedGeneration.Select(n => Fitness(n)).Max();
                chart.AddPoint(bestNetScoreFrom10Generations);
            }

            Console.ReadLine();
        }

        public static NeuralNetwork[] Train(NeuralNetwork[] gen)
        {
            return geneticAlgoForXOR.TrainNewGeneration(gen, 10, Fitness);
        }

        public static double Fitness(NeuralNetwork n)
        {
            double totalError = 0;
            for (int i = 0; i < _trainingSamples.Length; i++)
            {
                var actual = n.EvaluteNetwork(_trainingSamples[i].Input).First();
                var expected = _trainingSamples[i].Output.First();
                var error = Math.Abs(expected - actual);
                totalError += error;
            }

            var avgError = totalError / _trainingSamples.Length;
            return 1 - avgError;
        }
    }

    class TrainingSample
    {
        public double[] Input { get; }
        public double[] Output { get; }

        public TrainingSample(double[] input, double[] output)
        {
            Input = input;
            Output = output;
        }
    }
}