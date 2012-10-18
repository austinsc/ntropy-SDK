using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwnt
{
    class SpecificationLanguge
    {
        public SpecificationLanguge()
        {
            Comments = new List<string>();
            Quoted = new List<string>();
            Numeric = new List<string>();
            Keywords = new List<string>();
            Operators = new List<string>();
            Builtin = new List<string>();
            SymbolDefinitions = new List<string>();
            SymbolReferences = new List<string>();

            initialize();
        }

        public List<string> Comments { get; private set; }
        public List<string> Quoted { get; private set; }
        public List<string> Numeric { get; private set; }
        public List<string> Keywords { get; private set; }
        public List<string> Operators { get; private set; }
        public List<string> Identifiers { get; private set; }
        public List<string> Builtin { get; private set; }
        public List<string> SymbolDefinitions { get; private set; }
        public List<string> SymbolReferences { get; private set; }

        private void initialize()
        {
            Comments.Add(@"\b(//)+.*");
            Numeric.Add(@"\b\d+(\.\d*)?\b");
            Quoted.Add(@"([""'])(?:\\\1|.)*?\1");

            Operators.Add(@"\b=\b");

            Keywords.Add(@"\brequired\b");
            Keywords.Add(@"\boptional\b");
            Keywords.Add(@"\brepeated\b");
            Keywords.Add(@"\bmessage\b");
            Keywords.Add(@"\bservice\b");
            Keywords.Add(@"\benum\b");
            Keywords.Add(@"\bimport\b");
            Keywords.Add(@"\bextend\b");
            Keywords.Add(@"\bextensions\b");
            Keywords.Add(@"\bpackage\b");
            Keywords.Add(@"\brpc\b");
            Keywords.Add(@"\breturns\b");

            Builtin.Add(@"\bdouble\b");
            Builtin.Add(@"\bfloat\b");
            Builtin.Add(@"\bint32\b");
            Builtin.Add(@"\bint64\b");
            Builtin.Add(@"\buint32\b");
            Builtin.Add(@"\buint64\b");
            Builtin.Add(@"\bsint32\b");
            Builtin.Add(@"\bsint64\b");
            Builtin.Add(@"\bfixed32\b");
            Builtin.Add(@"\bfixed64\b");
            Builtin.Add(@"\bsfixed32\b");
            Builtin.Add(@"\bsfixed64\b");
            Builtin.Add(@"\bbool\b");
            Builtin.Add(@"\bstring\b");
            Builtin.Add(@"\bbytes\b");
            Builtin.Add(@"\b\b");
        }
    }
}
