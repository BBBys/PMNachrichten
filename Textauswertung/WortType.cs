using System;
using System.Collections.Generic;

public class nachHäufigkeit : Comparer<WortType>
{
  public override int Compare(WortType x, WortType y)
  {
    int hx, hy;
    /*if (x == null)
    {
      throw new ArgumentNullException(nameof(x));
    }

    if (y == null)
    {
      throw new ArgumentNullException(nameof(y));
    }*/
    hx = x.Anzahl;
    hy = y.Anzahl;
    return hx < hy ? 1 : hx > hy ? -1 : x.Wort.CompareTo(y.Wort);
  }
}

public class WortType : IComparable<WortType>//,IDisposable
, IEquatable<WortType>
{
  //public IComparer<WortType> Häufigkeit;
  public enum eKlassen
  {
    unklassifiziert, neutral,
    stopp, negativ, positiv
  };
  public static readonly double RLN2 = 1.0 / Math.Log(2);
  private readonly double _Entropie = -1;
  private static eKlassen _Klasse;
  /// <summary>
  /// Gesamtzahl aller Token, zu denen dieser Type gehört, im Text
  /// </summary>
  public int nTokenInText = -1;
  public int Gesamt = 0;

  ~WortType()
  {
    // Do not re-create Dispose clean-up code here.
    // Calling Dispose(false) is optimal in terms of
    // readability and maintainability.
    //Dispose(false);
  }
  /// <summary>
  /// Länge der Folge in Wörtern, NICHT Wortlänge in Buchstaben
  /// </summary>
  public int Länge { get; internal set; }
  public int Anzahl { get; set; }
  public string Wort { get; }

  public eKlassen Klasse { get => _Klasse; set => _Klasse = value; }

  /// <summary>
  /// Worttyp-Konstruktor, Anzahl = 1
  /// </summary>
  /// <param name="neu">der String</param>
  /// <param name="länge">Länge = verbundene Wörter</param>
  public WortType(string neu, int länge)
  {
    Wort = neu;
    Anzahl = 1;
    _Klasse = eKlassen.unklassifiziert;
    Länge = länge;
  }
  /// <summary>
  /// Worttyp-Konstruktor
  /// </summary>
  /// <param name="neu">der String</param>
  /// <param name="länge">Länge = verbundene Wörter</param>
  /// <param name="anzahl">Anzahl im Text</param>
  public WortType(string neu, int länge, int anzahl) : this(neu, länge) => Anzahl = anzahl;

  public WortType(string neu, int länge, int nToken, string pklasse) : this(neu, länge, nToken) => KlasseSetzen(pklasse);

  private static void KlasseSetzen(string pklasse)
  {
    if (string.IsNullOrWhiteSpace(pklasse))
    {
      _Klasse = eKlassen.unklassifiziert;
    }
    else
    {
      if (pklasse.CompareTo("unklassifiziert") == 0)
      {
        _Klasse = eKlassen.unklassifiziert;
      }
      if (pklasse.CompareTo("negativ") == 0)
      {
        _Klasse = eKlassen.negativ;
      }
      if (pklasse.CompareTo("neutral") == 0)
      {
        _Klasse = eKlassen.neutral;
      }
      if (pklasse.CompareTo("positiv") == 0)
      {
        _Klasse = eKlassen.positiv;
      }
      if (pklasse.CompareTo("stopp") == 0)
      {
        _Klasse = eKlassen.stopp;
      }
    }
  }

  /// <summary>
  /// Einlese-Konstruktor
  /// </summary>
  /// <param name="zeile">vorher mit WortType.ToString() ausgegebene Zeile</param>
  public WortType(string zeile)
  {
    char[] separators = new char[] { ';', '\t' };
    string[] vs;
    vs = zeile.Split(separators, StringSplitOptions.RemoveEmptyEntries);
    Anzahl = Convert.ToInt32(vs[0]);
    Wort = vs[1];
    KlasseSetzen(vs[2]);
    NBuchstaben = Wort.Length;
    Länge = Convert.ToInt32(vs[3]);
  }

  public static bool operator ==(WortType operand1, WortType operand2) => operand1.Wort.CompareTo(operand2.Wort) == 0;
  public static bool operator !=(WortType operand1, WortType operand2) => operand1.Wort.CompareTo(operand2.Wort) != 0;

  public int CompareTo(WortType other) =>
    // If other is not a valid object reference, this instance is greater.
    /* das geht nicht
     if (other == null)
    {
      return 1;
    }
    */
    Wort.CompareTo(other.Wort);
  public override string ToString() => $"{Anzahl,4:D};\t{Wort};\t{_Klasse}; {Länge};{RelHäufigkeit,5:F3};{Entropie,5:F3}";
  public override bool Equals(object obj) => Equals(obj as WortType);
  public bool Equals(WortType other) => !(other is null) && _Entropie == other._Entropie && nTokenInText == other.nTokenInText && Gesamt == other.Gesamt && Länge == other.Länge && Anzahl == other.Anzahl && Wort == other.Wort && Klasse == other.Klasse && Unklassifiziert == other.Unklassifiziert && Entropie == other.Entropie && RelHäufigkeit == other.RelHäufigkeit && NBuchstaben == other.NBuchstaben;

  public override int GetHashCode()
  {
    int hashCode = -1761989340;
    hashCode = (hashCode * -1521134295) + Länge.GetHashCode();
    hashCode = (hashCode * -1521134295) + NBuchstaben.GetHashCode();
    return hashCode;
  }

  public bool Unklassifiziert => _Klasse == eKlassen.unklassifiziert;

  public static bool Positiv => _Klasse == eKlassen.positiv;

  public static bool Negativ => _Klasse == eKlassen.negativ;

  public static bool Stopp => _Klasse == eKlassen.stopp;

  public double Entropie => -RelHäufigkeit * Math.Log(RelHäufigkeit) * RLN2;
  /// <summary>
  /// relative Häufigkeit dieses Types unter allen TGoken im Text
  /// </summary>
  public double RelHäufigkeit => Anzahl / (double)nTokenInText;

  public int NBuchstaben { get; }
}
