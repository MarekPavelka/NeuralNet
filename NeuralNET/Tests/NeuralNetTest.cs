using FluentAssertions;
using NeuralNET;
using Xunit;

namespace Tests
{
    public class NeuralNetTest
    {
        [Fact] //xUnit atribut ktory oznacuje funkciu ako test
        public void WhenNetworkCreatedManually_ThenNetWorkDnaAndShapeRetrievedCorrectly()
        {
            //arrange
            var expectedDna = new double[] {3, 3, 4, 4, 4, 4, 5, 5, 5};
            var expectedShape = new int[] {1, 2, 1};
            var net = new NeuralNetwork(expectedShape, expectedDna);

            //act
            var actualDna = net.GetNetworkDna();
            var actualShape = net.GetNetworkShape();

            //assert
            //FluentAssertions metody, BeEquivalent to je funkcia pre kolekcie aby checkovala po jednom
            // 4.Should().Be(4); samostatne hodnoty a premenne

            actualDna.Should().BeEquivalentTo(expectedDna); 
            actualShape.Should().BeEquivalentTo(expectedShape);
        }
        // vela malych testov, izolovane, nezavisle, ked je niekde chyba chcem aby padol single test a nie 15,
        // a zaroven ked padne test ze kde je konkretna chyba
        [Fact]
        public void When2NeuronsCreatedRandomly_ThenTheyHaveDifferentDna()
        {
            var neuron1 = new Neuron(3);
            var neuron2 = new Neuron(2);

            var dna1 = neuron1.GetDna();
            var dna2 = neuron2.GetDna();

            dna1.Should().NotBeEquivalentTo(dna2);
        }
    }
}