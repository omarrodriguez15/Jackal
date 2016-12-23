using System;
using Xunit;
using Jackal;
namespace Tests
{
    public class Tests
    {
        [Fact]
        public void Test1() 
        {
            Assert.True(true);
            BlackJackSim blackJack = new BlackJackSim(1, 2);
            blackJack.SimulateRounds(3);
            Assert.NotNull(blackJack.mainDeck);
        }
    }
}
