using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Media;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Language.StandardClassification;


namespace Pwnt
{
    #region Provider definition
    /// <summary>
    /// This class causes a classifier to be added to the set of classifiers. Since 
    /// the content type is set to "text", this classifier applies to all text files
    /// </summary>
    [Export(typeof(IClassifierProvider))]
    [ContentType("spec")]
    internal class SpecificationSyntaxProvider : IClassifierProvider
    {
        /// <summary>
        /// Import the classification registry to be used for getting a reference
        /// to the custom classification type later.
        /// </summary>
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null; // Set via MEF

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty<SpecificationSyntax>(delegate { return new SpecificationSyntax(ClassificationRegistry); });
        }
    }
    #endregion //provider def

    #region Classifier
    /// <summary>
    /// Classifier that classifies all text as an instance of the OrinaryClassifierType
    /// </summary>
    class SpecificationSyntax : IClassifier
    {
        SpecificationLanguge m_specificationLanguage;
        IClassificationType m_builtinType;

        IClassificationType m_whiteSpaceType;
        IClassificationType m_commentType;
        IClassificationType m_stringType;
        IClassificationType m_numericType;
        IClassificationType m_operatorType;

        IClassificationType m_keywordType;
        IClassificationType m_identifierType;
        
        IClassificationType m_symbolDefinitionType;
        IClassificationType m_symbolReferenceType;


        internal SpecificationSyntax(IClassificationTypeRegistryService registry)
        {
            m_specificationLanguage = new SpecificationLanguge();
            m_builtinType = registry.GetClassificationType("SpecificationBuiltinTypes");

            m_whiteSpaceType = registry.GetClassificationType(PredefinedClassificationTypeNames.WhiteSpace);
            m_commentType = registry.GetClassificationType(PredefinedClassificationTypeNames.Comment);
            m_stringType = registry.GetClassificationType(PredefinedClassificationTypeNames.String);
            m_numericType = registry.GetClassificationType(PredefinedClassificationTypeNames.Number);
            m_operatorType = registry.GetClassificationType(PredefinedClassificationTypeNames.Operator);

            m_keywordType = registry.GetClassificationType(PredefinedClassificationTypeNames.Keyword);
            m_identifierType = registry.GetClassificationType(PredefinedClassificationTypeNames.Identifier);


            m_symbolDefinitionType = registry.GetClassificationType(PredefinedClassificationTypeNames.SymbolDefinition);
            m_symbolReferenceType = registry.GetClassificationType(PredefinedClassificationTypeNames.SymbolReference);
        }

        /// <summary>
        /// This method scans the given SnapshotSpan for potential matches for this classification.
        /// In this instance, it classifies everything and returns each span as a new ClassificationSpan.
        /// </summary>
        /// <param name="trackingSpan">The span currently being classified</param>
        /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            //create a list to hold the results
            List<ClassificationSpan> classifications = new List<ClassificationSpan>();
            string current = span.GetText();

            //classifications.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), m_builtinType));

            Classify(classifications, current, span, m_specificationLanguage.Comments, m_commentType);

            Classify(classifications, current, span, m_specificationLanguage.Builtin, m_builtinType);
            Classify(classifications, current, span, m_specificationLanguage.Quoted, m_stringType);
            Classify(classifications, current, span, m_specificationLanguage.Keywords, m_keywordType);
            Classify(classifications, current, span, m_specificationLanguage.Operators, m_operatorType);
            Classify(classifications, current, span, m_specificationLanguage.Numeric, m_numericType);
            Classify(classifications, current, span, m_specificationLanguage.SymbolDefinitions, m_symbolDefinitionType);
            Classify(classifications, current, span, m_specificationLanguage.SymbolReferences, m_symbolReferenceType);

            return classifications;
        }

        private void Classify(List<ClassificationSpan> classifications, string current, SnapshotSpan span, List<string> matchList, IClassificationType classificationType)
        {
            foreach (var item in matchList)
            {
                Regex reg = new Regex(item, RegexOptions.None);
                var matches = reg.Matches(current);
                for (int i = 0; i < matches.Count; i++)
                {
                    Match m = matches[i];
                    Span new_span = new Span(span.Start.Position + m.Index, m.Length);
                    SnapshotSpan new_snapshot = new SnapshotSpan(span.Snapshot, new_span);
                    var newText = new_snapshot.GetText();
                    classifications.Add(new ClassificationSpan(new_snapshot, classificationType));
                }
            }
        }


#pragma warning disable 67
        // This event gets raised if a non-text change would affect the classification in some way,
        // for example typing /* would cause the classification to change in C# without directly
        // affecting the span.
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67
    }
    #endregion //Classifier
}
