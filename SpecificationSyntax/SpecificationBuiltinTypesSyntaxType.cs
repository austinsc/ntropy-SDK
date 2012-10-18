using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Pwnt
{
    internal static class SpecificationSyntaxClassificationDefinition
    {
        /// <summary>
        /// Defines the "Pwnt.Ntropy" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("SpecificationBuiltinTypes")]
        internal static ClassificationTypeDefinition SpecificationBuiltinTypesSyntaxType = null;

        [Export]
        [Name("spec")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition SpecificationContentDefinition = null;

        [Export]
        [FileExtension(".spec")]
        [ContentType("spec")]
        internal static FileExtensionToContentTypeDefinition SpecificationFileExtensionDefinition = null;

    }
}
