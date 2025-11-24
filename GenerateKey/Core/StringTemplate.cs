namespace Core
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class StringTemplate
    {
        private const string DIGITS = "0123456789";
        private const string LOWERCASE = "abcdefghijklmnopqrstuvwxyz";
        private const string UPPERCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string ALPHANUMERIC = UPPERCASE + LOWERCASE + DIGITS;

        // Token → String-Wert
        private readonly Dictionary<char, string> _tokens = new();

        // Token → aktueller Leseindex
        private readonly Dictionary<char, int> _tokenPositions = new();

        public StringTemplate(string template)
        {
            this.Template = template;
        }

        public string Template { get; private set; }

        /// <summary>
        /// Fügt einen Token hinzu, z.B. AddToken('X', "AB")
        /// </summary>
        public void AddToken(char token, string value)
        {
            this._tokens[token] = value;
            this._tokenPositions[token] = 0;
        }

        public string GetResult()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < this.Template.Length; i++)
            {
                char c = this.Template[i];

                // Prüfen ob ein mehrstelliger Platzhalter startet: {X:4}
                if (c == '{')
                {
                    int end = this.Template.IndexOf('}', i);
                    if (end == -1)
                    {
                        throw new FormatException("Ungültiger Platzhalter: '}' fehlt.");
                    }

                    string content = this.Template.Substring(i + 1, end - i - 1);

                    // Format: X:4
                    var match = Regex.Match(content, @"^(?<token>[A-Za-z])\:(?<len>\d+)$");
                    if (!match.Success)
                    {
                        throw new FormatException($"Ungültiges Token-Format: {{{content}}}");
                    }

                    char tokenChar = match.Groups["token"].Value[0];
                    int length = int.Parse(match.Groups["len"].Value);

                    if (!_tokens.ContainsKey(tokenChar))
                    {
                        throw new InvalidOperationException($"Token '{tokenChar}' wurde nicht definiert.");
                    }

                    string tokenValue = _tokens[tokenChar];

                    // Länge erzeugen – zyklisch über Token-String
                    for (int j = 0; j < length; j++)
                    {
                        int pos = _tokenPositions[tokenChar] % tokenValue.Length;
                        sb.Append(tokenValue[pos]);
                        _tokenPositions[tokenChar]++;
                    }

                    i = end; // Index weiter hinter die geschlossene Klammer setzen
                    continue;
                }

                if (c == '[')
                {
                    int end = this.Template.IndexOf(']', i);
                    if (end == -1)
                    {
                        throw new FormatException("Ungültiger Platzhalter: ']' fehlt.");
                    }

                    string content = this.Template.Substring(i + 1, end - i - 1);

                    // Format: X:4
                    var match = Regex.Match(content, @"^(?<token>[A-Za-z])\:(?<len>\d+)$");
                    if (!match.Success)
                    {
                        throw new FormatException($"Ungültiges Token-Format: {{{content}}}");
                    }

                    char tokenChar = match.Groups["token"].Value[0];
                    int length = int.Parse(match.Groups["len"].Value);

                    for (int j = 0; j < length; j++)
                    {
                        // Standardplatzhalter
                        sb.Append(tokenChar switch
                        {
                            'd' => CryptoRandomChar(DIGITS),
                            'a' => CryptoRandomChar(LOWERCASE),
                            'A' => CryptoRandomChar(UPPERCASE),
                            'x' => CryptoRandomChar(ALPHANUMERIC),
                            _ => c // normales Zeichen
                        });
                    }

                    i = end; // Index weiter hinter die geschlossene Klammer setzen
                    continue;
                }

                // Einfache 1-Zeichen Token-Verarbeitung (X)
                if (_tokens.ContainsKey(c))
                {
                    string tokenValue = _tokens[c];
                    int pos = _tokenPositions[c] % tokenValue.Length;
                    sb.Append(tokenValue[pos]);
                    _tokenPositions[c]++;
                    continue;
                }

                // Standardplatzhalter
                sb.Append(c switch
                {
                    'd' => CryptoRandomChar(DIGITS),
                    'a' => CryptoRandomChar(LOWERCASE),
                    'A' => CryptoRandomChar(UPPERCASE),
                    'x' => CryptoRandomChar(ALPHANUMERIC),
                    _ => c // normales Zeichen
                });
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gibt ein zufälliges Zeichen aus der Menge *crypto-sicher* zurück.
        /// </summary>
        private char CryptoRandomChar(string set)
        {
            int idx = RandomNumberGenerator.GetInt32(set.Length);
            return set[idx];
        }
    }
}
