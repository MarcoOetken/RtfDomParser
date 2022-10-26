/*
 * 
 *   Testcode RtfDomParser
 *   Author : Marco Oetken.
 *   Email  : marco.oetken@omspire.de
 * 
 */

namespace RtfDomParser.Tests
{
    using System;
    using NUnit.Framework;
    using RtfDomParser;

    [TestFixture]
    public class StringAttributeCollectionTests
    {
        [Test]
        public void NewObjectIsEmpty()
        {
            var sut = new StringAttributeCollection();
            Assert.That(sut.Count, Is.EqualTo(0));
        }

        [Test]
        public void EntriesAreCaseSensitive()
        {
            var sut = new StringAttributeCollection();
            sut["Name"] = "Value";
            Assert.That(sut["Name"], Is.EqualTo("Value"));
            Assert.That(sut["name"], Is.EqualTo(null));
        }

        [Test]
        public void SetValueTwiceCountStays1()
        {
            var sut = new StringAttributeCollection();
            sut["Name"] = "Value";
            sut["Name"] = "Value";
            Assert.That(sut["Name"], Is.EqualTo("Value"));
            Assert.That(sut.Count, Is.EqualTo(1));
        }
    }
}

