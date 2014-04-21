using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncTesting.BusinessLogic;
using AsyncTesting.DataAccess;
using NUnit.Framework;

namespace AsyncTesting.Test
{
    [TestFixture]
    public class BusinessLayerTests
    {
        BusinessLayer businessLayer;
        DataLayer dataLayer;

        [SetUp]
        public void SetUp()
        {
            dataLayer = new DataLayer(); 
            businessLayer = new BusinessLayer(dataLayer);
        }

        // STEP 1: Actually execute task, using await in test code
        // STEP 2: Ghetto mock w/a delegate
        // STEP 3: Use Task.FromResult, Moq.ReturnsAsync a civilized human being
    }
}
