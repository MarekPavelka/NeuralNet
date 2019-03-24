using System;
using System.Linq;

namespace NeuralNET
{
    public class Neuron
    {
        private double[] _weights; // vaha je taka ako input count
        private double _bias; //tuhost neuronu, iba na konci sa narata
        private static Random _generator = new Random(); //field lebo nema get set

        public Neuron(int inputCount)
        {
            _weights = InitializeRandomWeights(inputCount);
            _bias = _generator.NextDouble(); //jedna hodnota
        }

        public Neuron(double[] dna)
        {
            _weights = dna.Take(dna.Length - 1).ToArray();
            _bias = dna.Last();
        }

        //public void InitializeRandomWeights(int inputCount)
        //{
        //    _weights = new double[inputCount];
        //    for (int index = 0; index < _weights.Length; index++)
        //    {
        //        _weights[index] = _generator.NextDouble(); //inicializacia vah na random cisla
        //    }
        //}

        public double[] InitializeRandomWeights(int inputCount)
        {
            var weights = Enumerable.Range(0, inputCount).Select(_ => _generator.NextDouble()).ToArray();
            return weights;
        }

        public double EvaluateSingleInput(double input)
        {
            return Evaluate(new[] { input });
        }

        //public double Evaluate(double[] inputs) // zere vstupne hodnoty napr hidden layer
        //{
        //    var inputSum = 0.0;
        //    for (var i = 0; i < inputs.Length; i++)
        //    {
        //        inputSum = inputSum + inputs[i] * _weights[i];
        //    }
        //    var result = ActivationFunc(inputSum + _bias);
        //    return result;
        //}

        public double Evaluate(double[] inputs)
        {
            var inputSum = inputs.Zip(_weights, (i, w) => i * w).Sum() + _bias;
            return ActivationFunc(inputSum);
        }


        private double ActivationFunc(double input) // helper pre result funkcie Evaluate
        {
            return 1 / (1 + Math.Exp(-input));
        }

        public double[] GetDna()
        {
            return _weights.Concat(new[] { _bias }).ToArray();
        }
    }
}