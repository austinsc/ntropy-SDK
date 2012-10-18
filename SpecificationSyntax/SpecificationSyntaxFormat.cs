using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Pwnt
{
    #region Format definition
    /// <summary>
    /// Defines an editor format for the Pwnt.Ntropy type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "SpecificationBuiltinTypes")]
    [Name("SpecificationBuiltinTypes")]
    [UserVisible(true)] //this should be visible to the end user
    [Order(Before = Priority.Default)] //set the priority to be after the default classifiers
    internal sealed class SpecificationBuiltInTypesSyntaxFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "Pwnt.Ntropy" classification type
        /// </summary>
        public SpecificationBuiltInTypesSyntaxFormat()
        {
            this.DisplayName = "SpecificationBuiltinTypes"; //human readable version of the name
            //this.BackgroundColor = Colors.BlueViolet;
            //this.TextDecorations = System.Windows.TextDecorations.Underline;
            this.ForegroundColor = Colors.CadetBlue;
            this.IsBold = true;
        }
    }
    #endregion //Format definition
}
