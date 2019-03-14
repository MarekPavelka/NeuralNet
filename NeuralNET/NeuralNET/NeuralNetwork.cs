using System;
using System.Linq;

namespace NeuralNET
{
    class NeuralNetwork
    {
        private Neuron[] _inputNeurons; //fieldy, helper premmenne pre funkcie classy a constructor
        private Neuron[] _hiddenNeurons;
        private Neuron[] _outputNeurons;

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
            _inputNeurons = Enumerable.Range(0, inputLayerCount).Select(_ => new Neuron(1)).ToArray();
            _hiddenNeurons = new Neuron[hiddenLayerCount].Select(neuron => new Neuron(inputLayerCount)).ToArray();
            _outputNeurons = new Neuron[outputLayerCount].Select(neuron => new Neuron(hiddenLayerCount)).ToArray();
        }

        //public double[] EvaluteNetwork(double[] inputs) // to co tam seednem zvonka ked volam EvaluateNetwork
        //{
        //    var inputNeuronsResults = new double[_inputNeurons.Length]; // NeuralNetwork(3,4,2)
        //    for (int i = 0; i < _inputNeurons.Length; i++)
        //    {
        //        var input = inputs[i]; //3
        //        inputNeuronsResults[i] = _inputNeurons[i].Evaluate(new double[1] { input });
        //    }

        //    var hiddenNeuronsResults = new double[_hiddenNeurons.Length]; // (3,4,2)
        //    for (int i = 0; i < _hiddenNeurons.Length; i++)
        //    {
        //        hiddenNeuronsResults[i] = _hiddenNeurons[i].Evaluate(inputNeuronsResults);
        //    }

        //    var outputNeuronsResults = new double[_outputNeurons.Length]; // (3,4,2)
        //    for (int i = 0; i < _outputNeurons.Length; i++)
        //    {
        //        outputNeuronsResults[i] = _outputNeurons[i].Evaluate(hiddenNeuronsResults);
        //    }

        //    return outputNeuronsResults;
        //}
        public double[] EvaluteNetwork(double[] inputs) // to co tam seednem zvonka ked volam EvaluateNetwork
        {
            //var inputNeuronsResults = new double[_inputNeurons.Length]
            //    .Select(neuronResult =>_inputNeurons.Select(neuronValue => neuronValue.Evaluate(inputs[0])))
            //    .ToArray();
            //var inputNeuronsResults = new double[_inputNeurons.Length]; // NeuralNetwork(3,4,2)
            //    for (int i = 0; i < _inputNeurons.Length; i++)
            //    {
            //    var input = inputs[i]; //3
            //    inputNeuronsResults[i] = _inputNeurons[i].EvaluateSingleInput(inputs[i]);
            //    }

            //    var hiddenNeuronsResults = new double[_hiddenNeurons.Length]
            //        .Select(neuronResult =>
            //            _hiddenNeurons
            //                .Select(neuronValue => neuronValue.Evaluate(inputNeuronsResults))).ToArray();


            //    var outputNeuronsResults = new double[_outputNeurons.Length]
            //        .Select(neuronResult =>
            //            _outputNeurons.Select(neuronValue => neuronValue.Evaluate(hiddenNeuronsResults))).ToArray();
            //    return outputNeuronsResults;
            var inputNeuronsResults = inputs.Zip(_inputNeurons, (i, n) => n.Evaluate(new[] {i})).ToArray();
            var hiddenNeuronsResults = _hiddenNeurons.Select(neuron => neuron.Evaluate(inputNeuronsResults)).ToArray();
            var outputNeuronresults = _outputNeurons.Select(neuron => neuron.Evaluate(hiddenNeuronsResults)).ToArray();
            return outputNeuronresults;
        }
    }
}