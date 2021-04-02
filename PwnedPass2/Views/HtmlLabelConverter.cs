using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PwnedPass2.Views
{
    public class HtmlLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var formatted = new FormattedString();

            foreach (var item in ProcessStringForEm(ProcessString((string)value)))
            {
                if (item.Italic)
                {
                    formatted.Spans.Add(CreateSpanItalic(item));
                }
                else formatted.Spans.Add(CreateSpan(item));
            }
            formatted.Spans[0].Text = formatted.Spans[0].Text.Substring(1);
            return formatted;
        }

        private IList<StringSection> ProcessStringForEm(IList<StringSection> processedString)
        {
            const string spanPattern = @"(<em.*?>.*?</em>)";

            var sections = new List<StringSection>();
            foreach (var item2 in processedString)
            {
                if (item2.Link != null)
                {
                    sections.Add(item2);
                }
                MatchCollection collection = Regex.Matches(item2.Text, spanPattern, RegexOptions.Singleline);

                var lastIndex = 0;
                foreach (Match item in collection)
                {
                    sections.Add(new StringSection() { Text = (item2.Text.Substring(lastIndex, item.Index - lastIndex).Replace("&amp;", "&")).Replace("&mdash;", "-") });
                    lastIndex += item.Index - lastIndex + item.Length;

                    // Get italic
                    var html = new StringSection()
                    {
                        Link = null,
                        Text = Regex.Replace(item.Value, "<.*?>", string.Empty),
                        Italic = true
                    };

                    sections.Add(html);
                }
                sections.Add(new StringSection() { Text = (item2.Text.Substring(lastIndex).Replace("&amp;", "&")).Replace("&mdash;", "-") });
            }

            return sections;
        }

        private Span CreateSpan(StringSection section)
        {
            var span = new Span()
            {
                Text = section.Text
            };

            if (!string.IsNullOrEmpty(section.Link))
            {
                span.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = _navigationCommand,
                    CommandParameter = section.Link
                });
                span.TextColor = Color.Blue;
                span.TextDecorations = TextDecorations.Underline;
            }
            else
            {
                span.Text = " " + span.Text;
            }

            return span;
        }

        private Span CreateSpanItalic(StringSection section)
        {
            var span = new Span()
            {
                Text = section.Text
            };

            span.FontAttributes = FontAttributes.Italic;

            return span;
        }

        protected static IList<StringSection> ProcessString(string rawText)
        {
            const string spanPattern = @"(<a.*?>.*?</a>)";

            MatchCollection collection = Regex.Matches(rawText, spanPattern, RegexOptions.Singleline);

            var sections = new List<StringSection>();

            var lastIndex = 0;

            foreach (Match item in collection)
            {
                sections.Add(new StringSection() { Text = rawText.Substring(lastIndex, item.Index - lastIndex) });
                lastIndex += item.Index - lastIndex + item.Length;

                // Get HTML href
                var html = new StringSection()
                {
                    Link = Regex.Match(item.Value, "(?<=href=\\\")[\\S]+(?=\\\")").Value,
                    Text = Regex.Replace(item.Value, "<.*?>", string.Empty),
                    Italic = false
                };

                sections.Add(html);
            }
            if (lastIndex > rawText.Length)
            {
                lastIndex = rawText.Length;
            }
            sections.Add(new StringSection() { Text = rawText.Substring(lastIndex) });

            return sections;
        }

        protected class StringSection
        {
            public string Text { get; set; }
            public string Link { get; set; }
            public bool Italic { get; set; }
        }

        private readonly ICommand _navigationCommand = new Command<string>((url) =>
        {
            Launcher.OpenAsync(new Uri(url));
        });

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
