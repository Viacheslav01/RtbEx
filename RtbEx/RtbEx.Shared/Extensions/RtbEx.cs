using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace RtbEx.Extensions
{
    public static class RtbEx
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
            "Text", typeof(string), typeof(RtbEx), new PropertyMetadata(default(string), OnTextChanged));
        
        public static void SetText(DependencyObject element, string value)
        {
            element.SetValue(TextProperty, value);
        }

        public static string GetText(DependencyObject element)
        {
            return (string) element.GetValue(TextProperty);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var richTextBlock = d as RichTextBlock;
            if (richTextBlock == null)
            {
                return;
            }

            richTextBlock.Blocks.Clear();

            var text = e.NewValue as string;
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            richTextBlock.Blocks.Add(CreateParagraph(text));
        }

        private static Paragraph CreateParagraph(string text)
        {
            var paragraph = new Paragraph();

            var splitResult = Regex.Split(text, @"(https?://\S+)");
            foreach (var part in splitResult)
            {
                if (part.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    var hyperLink = new Hyperlink {NavigateUri = new Uri(part)};
                    hyperLink.Inlines.Add(new Run {Text = part});

                    paragraph.Inlines.Add(hyperLink);
                    continue;
                }
                    
                paragraph.Inlines.Add(new Run {Text = part});
            }

            return paragraph;
        }
    }
}
