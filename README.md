# GenerateKey

![NET](https://img.shields.io/badge/NET-8.0-green.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS](https://img.shields.io/badge/Visual%20Studio-2022-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2025.0-yellow.svg)]

Erstellen aus einem **String Template** einen Schlüssel mit beliebigen Werten und Formaten

Folgende Zeichen werden fest interpretiert:
- `x` : Zufälliger alphanumerischer Buchstabe (Groß- und Kleinbuchstaben + Ziffern)
- `a` : Zufälliger Kleinbuchstabe
- `A` : Zufälliger Großbuchstabe
- `d` : Zufällige Ziffer 

Mit dem Format `[x:n]`, `[a:n]`, `[A:n]` und `[d:n]` kann auf einfache Weise die Anzahl der zufälligen Zeichen mit dem angegebenen Format definiert werden.

Beispiel:
```csharp
const string pattern = "xxxx-xxxx-xx-xx-xx-xxxx";

StringTemplate tmp = new StringTemplate(pattern);
string result = tmp.GetResult();
```

Ergebnis:
```bat
FQYc-S4vE-Ph-A4-RD-wMkp
```

```csharp
const string pattern = "xxxx-DD-WW-xxxx";

StringTemplate tmp = new StringTemplate(pattern);
tmp.AddToken('D', "42");
tmp.AddToken('W', "A1");
string result = tmp.GetResult();
```

Ergebnis:
```bat
qDzG-42-A1-2BLV
```

Komplexes String Template
```csharp
const string pattern = "AAAA-[x:4]-DD-WW-xxxx-dddd";

StringTemplate tmp = new StringTemplate(pattern);
tmp.AddToken('D', "42");
tmp.AddToken('W', "A1");
string result = tmp.GetResult();
```

Ergebnis:
```bat
FRUR-Kpk2-42-A1-hXzb-6543
```


