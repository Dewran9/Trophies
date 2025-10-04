namespace Trophies;

public class Trophy
{
    private string _competition = "";
    private int _year;

    public int Id { get; set; }

    public string Competition
    {
        get => _competition;
        set
        {
            if (value is null)
                throw new ArgumentNullException(nameof(Competition), "Competition må ikke være null.");
            var trimmed = value.Trim();
            if (trimmed.Length < 3)
                throw new ArgumentException("Competition skal være mindst 3 tegn langt.");
            _competition = trimmed;
        }
    }

    public int Year
    {
        get => _year;
        set
        {
            if (value < 1970 || value > 2025)
                throw new ArgumentOutOfRangeException(nameof(Year), "Year skal være mellem 1970 og 2025.");
            _year = value;
        }
    }

    public Trophy() { }

    public Trophy(int id, string competition, int year)
    {
        Id = id;
        Competition = competition; 
        Year = year;               
    }

  
    public Trophy(Trophy other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));
        Id = other.Id;
        Competition = other.Competition;
        Year = other.Year;
    }

    public override string ToString() => $"{Id}: {Competition} ({Year})";
}
