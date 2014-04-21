using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncTesting.DataAccess;
using NUnit.Framework;

namespace AsyncTesting.Test
{
    [TestFixture]
    public class DataLayerTests
    {
        DataLayer dataLayer;

        [SetUp]
        public void SetUp()
        {
           dataLayer = new DataLayer(); 
        }
    }
}
