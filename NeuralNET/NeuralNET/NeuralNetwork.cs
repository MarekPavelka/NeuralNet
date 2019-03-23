using System.Data;
using System.Linq;

namespace NeuralNET
{
    class NeuralNetwork
    {
        private readonly Neuron[] _inputNeurons; //fieldy, helper premmenne pre funkcie classy a constructor
        private readonly Neuron[] _hiddenNeurons;
        private readonly Neuron[] _outputNeurons;

        //public NeuralNetwork(int inputLayerCount, int hiddenLayerCount, int outputLayerCount) // kolko neuronov v layeri
        //{
        //    _inputNeurons = new Neuron[inputLayerCount]; // inicializacia inputlayeru
        //    for (int i = 0; i < _inputNeurons.Length; i++)
        //    {
        //        _inputNeurons[i] = new Neuron(1);
        //    }
        //    _hiddenNeurons = new Neuron[hiddenLayerCount];
        //    for (int i = 0; i < _hiddenNeurons.Length; i++)
        //    {
        //        _hiddenNeurons[i] = new Neuron(inputLayerCount);
        //    }
        //    _outputNeurons = new Neuron[outputLayerCount];
        //    for (int i = 0; i < _outputNeurons.Length; i++)
        //    {
        //        _outputNeurons[i] = new Neuron(hiddenLayerCount); //hiddenLayerCount pouzivam na nasetovanie random _weights pre neuron
        //    }
        //}




        public NeuralNetwork(int inputLayerCount, int hiddenLayerCount, int outputLayerCount) // kolko neuronov v layeri
        {
            //_inputNeurons = new Neuron[inputLayerCount].Select(neuron => new Neuron(1)).ToArray();
            _inputNeurons = CreateNeuronLayer(inputLayerCount, 1);
            _hiddenNeurons = CreateNeuronLayer(hiddenLayerCount, inputLayerCount);
            _outputNeurons = CreateNeuronLayer(outputLayerCount, hiddenLayerCount);
        }

        //WEIGHTS + BIASES =[32], intput = 3x(1w + 1b), hidden = 4x(3w + 1b), output = 2x(4w + 1b)
        // dorobit funkcionalitu na hocikolko layerov
        public NeuralNetwork(int[] networkShape, double[] dna)
        {
            var biasCount = 1;
            var numberOfLayers = networkShape.Length; 
            var layer1Count = networkShape[0];
            var Layer1Dna = dna.Take(layer1Count + layer1Count * biasCount).ToArray();
            var layer2Count = networkShape[1];
            var Layer2Dna = dna.Skip(Layer1Dna.Length).Take(layer1Count * layer2Count + layer2Count * biasCount).ToArray();
            var layer3Count = networkShape[2];
            var layer3Dna = dna.Skip(Layer2Dna.Length).Take(layer2Count * layer3Count + layer3Count * biasCount).ToArray();

            _inputNeurons = Enumerable.Range(0, layer1Count)
                .Select(nIndex => GetNeuronDna(Layer1Dna, 1 + biasCount, nIndex))
                .Select(nDna => new Neuron(nDna))
                .ToArray();

            _hiddenNeurons = Enumerable.Range(0, layer2Count)
                .Select(nIndex => GetNeuronDna(Layer2Dna, layer1Count + biasCount, nIndex))
                .Select(nDna => new Neuron(nDna))
                .ToArray();

            _outputNeurons = Enumerable.Range(0, layer3Count)
                .Select(nIndex => GetNeuronDna(layer3Dna, layer2Count + biasCount, nIndex))
                .Select(nDna => new Neuron(nDna))
                .ToArray();
        }

        public double[] GetNeuronDna(double[] dnaOfLayer, int dnaElementsNeeded, int dnaIndex)
        {
            return dnaOfLayer.Skip(dnaIndex*dnaElementsNeeded).Take(dnaElementsNeeded).ToArray();
        }

        private Neuron[] CreateNeuronLayer(int neuronCount, int neuronInputCount)
        {
            return Enumerable.Range(0, neuronCount).Select(_ => new Neuron(neuronInputCount)).ToArray();
        }

        public double[] GetNetworkDna()
        {
            var netDna = GetNetworkNeurons()
                .Select(neuron => neuron.GetDna())
                .SelectMany(dnaArray => dnaArray)
                .ToArray();
            return netDna;
        }

        public int[] GetNetworkInfo()
        {
            var networkInfo = new[] { _inputNeurons.Length, _hiddenNeurons.Length, _outputNeurons.Length };
            return networkInfo;
        }

        private Neuron[] GetNetworkNeurons()
        {
            var network = new[] { _inputNeurons, _hiddenNeurons, _outputNeurons };
            return network.SelectMany(n => n).ToArray();
        }

        public double[] EvaluteNetwork(double[] inputs) // to co tam feednem zvonka ked volam EvaluateNetwork
        { 
            var inputNeuronsResults = inputs.Zip(_inputNeurons, (i, n) => n.EvaluateSingleInput(i)).ToArray();
            var hiddenNeuronsResults = _hiddenNeurons.Select(neuron => neuron.Evaluate(inputNeuronsResults)).ToArray();
            var outputNeuronresults = _outputNeurons.Select(neuron => neuron.Evaluate(hiddenNeuronsResults)).ToArray();
            return outputNeuronresults;
        }
    }
}