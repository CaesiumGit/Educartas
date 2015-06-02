using UnityEngine;
using System.Collections;
using NUnit;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class GameControlTest
    {
        private IShuffleable _cardShuffle;

        [SetUp]
        public void Initialize()
        {
            _cardShuffle = new ShuffleCards();
        }

    }
}
