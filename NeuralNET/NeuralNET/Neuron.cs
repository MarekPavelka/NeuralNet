using System;

namespace NeuralNET
{
    class Neuron
    {
        private double[] _weights;
        private double _bias;

        public Neuron(int inputCount)
        {
            _weights = new double[inputCount];
            Random generator = new Random();
            for (int index = 0; index < _weights.Length; index++)
            {
                _weights[index] = generator.NextDouble();
            }

            _bias = generator.NextDouble();
        }

        public double Evaluate(double[] inputs)
        {
            var inputSum = 0.0;
            for (var i = 0; i < inputs.Length; i++)
            {
                inputSum = inputSum + inputs[i] * _weights[i];
            }
            var result = ActivationFunc(inputSum + _bias);
            return result;
        }

        private double ActivationFunc(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }
    }
}