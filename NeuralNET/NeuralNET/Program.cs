using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNET
{
    class Program
    {
        static void Main()
        {
            //var newNeuron = new Neuron(8); dummy into
            //var result = newNeuron.Evaluate(new double[8] { 1, 2, 3, 4, 5, 6, 7, 8 });

            var net = new NeuralNetwork(3,4,2);
            var netValue = net.EvaluteNetwork(new double[]{2,3,4});
            var counter = 1;
            foreach (var number in netValue)
            {
                Console.WriteLine($"vysledok evaluateu {counter} output neuronu neuronky je {number}");
                counter++;
            }

            Console.ReadLine();
        }
    }
}