/*
 * 
 *   Testcode RtfDomParser
 *   Author : Marco Oetken.
 *   Email  : marco.oetken@omspire.de
 * 
 */

namespace RtfDomParser.Tests
{
    using System.IO;
    using NUnit.Framework;

    public class LexerTests
    {
        [Test]
        public void RtfIsKeyword()
        {
            var sr = new StringReader("\\rtf1");
            var sut = new RTFLex(sr);
            RTFToken token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
        }

        [Test]
        public void AnsiIsKeyword()
        {
            var sr = new StringReader("\\ansi");
            var sut = new RTFLex(sr);
            RTFToken token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
        }

        [Test]
        public void ParIsKeyword()
        {
            var sr = new StringReader("\\par");
            var sut = new RTFLex(sr);
            RTFToken token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
        }

        [Test]
        public void PardIsKeyword()
        {
            var sr = new StringReader("\\pard");
            var sut = new RTFLex(sr);
            RTFToken token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
        }

        [Test]
        public void FIsKeyword()
        {
            var sr = new StringReader("\\f0");
            var sut = new RTFLex(sr);
            RTFToken token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.HasParam, Is.True);
        }

        [Test]
        public void BIsKeyword()
        {
            var sr = new StringReader("\\b");
            var sut = new RTFLex(sr);
            RTFToken token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.HasParam, Is.False);
        }

        [Test]
        public void IIsKeyword()
        {
            var sr = new StringReader("\\i0");
            var sut = new RTFLex(sr);
            RTFToken token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.HasParam, Is.True);
        }

        [Test]
        public void UmlautIsControl()
        {
            var sr = new StringReader("\\'e4"); // e4 is ä
            var sut = new RTFLex(sr);
            RTFToken token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Control));
            Assert.That(token.HasParam, Is.True);
            Assert.That(token.Param, Is.EqualTo(14 * 16 + 4));
        }

        [Test]
        public void MinimalRtf()
        {
            const string MinimalRtfString = @"{\rtf1\ansi{\fonttbl\f0\fswiss Helvetica;}\f0\pard
This is an umlaut (ae): \'e4!\par
}";
            var sr = new StringReader(MinimalRtfString);
            var sut = new RTFLex(sr);
            RTFToken token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.GroupStart));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.Key, Is.EqualTo("rtf"));
            Assert.That(token.HasParam, Is.True);
            Assert.That(token.Param, Is.EqualTo(1));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.Key, Is.EqualTo("ansi"));
            Assert.That(token.HasParam, Is.False);
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.GroupStart));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.Key, Is.EqualTo("fonttbl"));
            Assert.That(token.HasParam, Is.False);
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.Key, Is.EqualTo("f"));
            Assert.That(token.HasParam, Is.True);
            Assert.That(token.Param, Is.EqualTo(0));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.Key, Is.EqualTo("fswiss"));
            Assert.That(token.HasParam, Is.False);
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Text));
            Assert.That(token.Key, Is.EqualTo("Helvetica;"));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.GroupEnd));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.Key, Is.EqualTo("f"));
            Assert.That(token.HasParam, Is.True);
            Assert.That(token.Param, Is.EqualTo(0));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.Key, Is.EqualTo("pard"));
            Assert.That(token.HasParam, Is.False);
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Text));
            Assert.That(token.Key, Is.EqualTo("This is an umlaut (ae): "));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Control));
            Assert.That(token.HasParam, Is.True);
            Assert.That(token.Param, Is.EqualTo(14 * 16 + 4));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Text));
            Assert.That(token.Key, Is.EqualTo("!"));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Keyword));
            Assert.That(token.Key, Is.EqualTo("par"));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.GroupEnd));
            token = sut.NextToken();
            Assert.That(token.Type, Is.EqualTo(RTFTokenType.Eof));
        }
    }
}

