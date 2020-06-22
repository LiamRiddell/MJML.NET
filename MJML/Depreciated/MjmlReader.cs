using System;
using System.IO;
using System.Xml;

namespace MJML
{
    public class MjmlReader : XmlReader
    {
        private XmlReader _reader;

        private MjmlReader(StringReader input)
        {
            _reader = XmlReader.Create(input);
        }

        // LR: This may need to return XmlReader
        public static MjmlReader Create(StringReader input)
        {
            return new MjmlReader(input);
        }

        public override bool Read()
        {
            return _reader.Read();
        }

        public override XmlNodeType NodeType => _reader.NodeType;

        public override string LocalName => _reader.LocalName;

        public override string NamespaceURI => _reader.NamespaceURI;

        public override string Prefix => _reader.Prefix;

        public override string Value => _reader.Value;

        public override int Depth => _reader.Depth;

        public override string BaseURI => _reader.BaseURI;

        public override bool IsEmptyElement => _reader.IsEmptyElement;

        public override int AttributeCount => _reader.AttributeCount;

        public override bool EOF => _reader.EOF;

        public override ReadState ReadState => _reader.ReadState;

        public override XmlNameTable NameTable => _reader.NameTable;

        public override string GetAttribute(string name) => _reader.GetAttribute(name);

        public override string GetAttribute(string name, string namespaceURI) => _reader.GetAttribute(name, namespaceURI);

        public override string GetAttribute(int i) => _reader.GetAttribute(i);

        public override string LookupNamespace(string prefix) => _reader.LookupNamespace(prefix);

        public override bool MoveToAttribute(string name) => _reader.MoveToAttribute(name);

        public override bool MoveToAttribute(string name, string ns) => _reader.MoveToAttribute(name, ns);

        public override bool MoveToElement() => _reader.MoveToElement();

        public override bool MoveToFirstAttribute() => _reader.MoveToFirstAttribute();

        public override bool MoveToNextAttribute() => _reader.MoveToNextAttribute();

        public override bool ReadAttributeValue() => _reader.ReadAttributeValue();

        public override void ResolveEntity() => _reader.ResolveEntity();
    }
}