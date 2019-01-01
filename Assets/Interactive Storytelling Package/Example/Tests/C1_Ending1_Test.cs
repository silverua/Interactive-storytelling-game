using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class C1_Ending1_Test
    {
        // A Test behaves as an ordinary method
        [Test]
        public void C1_Ending1_TestSimplePasses()
        {
            // Use the Assert class to test conditions
            ChapterManager.SetChapter("Chapter 1");
            // kill the king
            ChapterManager.InteractWith("King", "Sword");
            // add any number of other interactions that are planned here:
            
            // check for results of all of these interactions:
            // get the queen
            var queen = ChapterManager.CurrentChapter.GetElementByName("Queen");
            // check that her current interaction index changed from 0 to 1
            Assert.AreEqual(queen.CurrentInteractionIndex, 1);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator C1_Ending1_TestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
