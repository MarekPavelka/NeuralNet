using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNET
{
    class GeneticAlgo
    {
        private int _survivorsCount;
        private int _populationCount;
        private int _mutationChance; //0-100%

        private Random _rndGen = new Random();

        public GeneticAlgo(int survivorsCount, int populationCount, int mutationChance)
        {
            _survivorsCount = survivorsCount;
            _populationCount = populationCount;
            _mutationChance = mutationChance;
        }

        //NeuralNet[] Train(int[] networkShape, int generationsCount, Func<NeuralNet, double> fitness)
        //    {
        //        var initialPopulation = create _populationCount NeuralNetworks of the correct shape
        //        return Train(initialPopulation, generationsCount, fitness);
        //    }

        public NeuralNetwork[] TrainNewGeneration(NeuralNetwork[] gen0, int generationsCount, Func<NeuralNetwork, double> fitness)
        {
            var generationToEvaluate = gen0;

            for (var generationId = 1; generationId <= generationsCount; generationId++)
            {
                var survivors = GetSurvivors(generationToEvaluate, fitness);
                generationToEvaluate = SpawnNextGeneration(survivors);
            }
            return generationToEvaluate;
        }

        private NeuralNetwork[] GetSurvivors(NeuralNetwork[] generation, Func<NeuralNetwork, double> fitness)
        {
            var survivors = generation.OrderByDescending(fitness)
                .Take(_survivorsCount)
                .ToArray();
            return survivors;
        }

        private NeuralNetwork[] SpawnNextGeneration(NeuralNetwork[] survivors)
        {
            var newChildren = Enumerable.Range(0, _populationCount)
                .Select(_ => GetParents(survivors))
                .Select(parents => SpawnNewChild(parents))
                .ToArray();
            return newChildren.ToArray();
        }

        //private NeuralNetwork[] SpawnNextGeneration(NeuralNetwork[] survivors)
        //{
        //    var childrenToGenerateCount = _populationCount - survivors.Length;
        //    var newChildren = Enumerable.Range(0, childrenToGenerateCount)
        //        .Select(_ => GetParents(survivors))
        //        .Select(parents => SpawnNewChild(parents))
        //        .ToArray();
        //    return newChildren.Concat(survivors).ToArray();
        //}

        private NeuralNetwork[] GetParents(NeuralNetwork[] survivors)
        {
            var parents = new NeuralNetwork[2];
            parents[0] = survivors[_rndGen.Next(0, _survivorsCount)];
            parents[1] = survivors[_rndGen.Next(0, _survivorsCount)];
            return parents;
        }

        // sets weights and biases for new child from 2 random survivors
        private NeuralNetwork SpawnNewChild(NeuralNetwork[] parents) 
        {
            var parent1 = parents[0];
            var parent2 = parents[1];

            var childShape = parent1.GetNetworkShape(); // []{3,4,2}

            double[] parent1Dna = parent1.GetNetworkDna(); // WEIGHTS+BIASES=[32], intput=3x(1w+1b), hidden=4x(3w+1b), output=2x(4w+1b)

            double[] parent2Dna = parent2.GetNetworkDna();

            double[] childDna = Crossover(parent1Dna, parent2Dna);

            Mutate(childDna);

            return new NeuralNetwork(childShape, childDna);
        }

        private double[] Crossover(double[] parent1Dna, double[] parent2Dna)
        {
            var crossoverIdx = _rndGen.Next(1, parent1Dna.Length - 1);
            var childDna = parent1Dna.Take(crossoverIdx).Concat(parent2Dna.Skip(crossoverIdx)).ToArray();
            return childDna;
        }

        void Mutate(double[] dna)
        {
            for (var i = 0; i < dna.Length; i++)
            {
                if (_rndGen.Next(0, 100) <= _mutationChance)
                {
                    dna[i] += (_rndGen.NextDouble() - 0.5) / 5; //<-0,1 - 0,1> small difference to dna value
                }
            }
        }
    }
}