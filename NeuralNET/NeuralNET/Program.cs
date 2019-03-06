using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNET
{
    class Program
    {
        static void Main()
        {
            var newNeuron = new Neuron(8);
            var result = newNeuron.Evaluate(new double[8] { 1, 2, 3, 4, 5, 6, 7, 8 });

        }
    }

    class NeuralNetwork
    {
        private Neuron[] _inputNeurons;
        private Neuron[] _hiddeNeurons;
        private Neuron[] _outputNeurons;

        public NeuralNetwork(int inputNumber, int hiddenNumber, int outputNumber)
        {
            _inputNeurons = new Neuron[inputNumber];
            for (int i = 0; i < _inputNeurons.Length; i++)
            {
                _inputNeurons[i] = new Neuron(1);
            }
            _hiddeNeurons = new Neuron[hiddenNumber];
            for (int i = 0; i < _hiddeNeurons.Length; i++)
            {
                _hiddeNeurons[i] = new Neuron(inputNumber);
            }
            _outputNeurons = new Neuron[outputNumber];
            for (int i = 0; i < _outputNeurons.Length; i++)
            {
                _outputNeurons[i] = new Neuron(hiddenNumber);
            }
        }

        public double[] EvaluteNetwork(double[] inputs)
        {
            var inputNeuronsResults = new double[_inputNeurons.Length];
            for (int i = 0; i < _inputNeurons.Length; i++)
            {
                var neuron = _inputNeurons[i];
                var input = inputs[i];
                inputNeuronsResults[i] = neuron.Evaluate(input);
            }
        }


    }
}