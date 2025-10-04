using System.Collections.Generic;
using System.Linq;

namespace Trophies;

public class TrophiesRepository
{
    private readonly List<Trophy> _trophies = new();
    private int _nextId = 1;

    public TrophiesRepository()
    {
    
        Add(new Trophy(0, "National Championship", 2018));
        Add(new Trophy(0, "Regional Cup", 2020));
        Add(new Trophy(0, "City League", 2015));
        Add(new Trophy(0, "Summer Games", 2025));
        Add(new Trophy(0, "Winter Classic", 2019));
    }

    public IEnumerable<Trophy> Get(int? year = null, string? sortBy = null, bool descending = false)
    {
        IEnumerable<Trophy> query = _trophies;

    
        if (year.HasValue)
            query = query.Where(t => t.Year == year.Value);

       
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            var key = sortBy.Trim().ToLowerInvariant();
            query = key switch
            {
                "competition" => descending ? query.OrderByDescending(t => t.Competition)
                                            : query.OrderBy(t => t.Competition),
                "year" => descending ? query.OrderByDescending(t => t.Year)
                                            : query.OrderBy(t => t.Year),
                _ => query
            };
        }

        return query.Select(t => new Trophy(t)).ToList();
    }

    public Trophy? GetById(int id) => _trophies.FirstOrDefault(t => t.Id == id);

    public Trophy Add(Trophy trophy)
    {
        if (trophy is null) throw new ArgumentNullException(nameof(trophy));

    
        var toInsert = new Trophy(0, trophy.Competition, trophy.Year)
        {
            Id = _nextId++
        };

        _trophies.Add(toInsert);
        return toInsert;
    }

    public Trophy? Remove(int id)
    {
        var found = GetById(id);
        if (found is null) return null;
        _trophies.Remove(found);
        return found;
    }


    public Trophy? Update(int id, Trophy values)
    {
        var existing = GetById(id);
        if (existing is null) return null;
        if (values is null) throw new ArgumentNullException(nameof(values));

        existing.Competition = values.Competition;
        existing.Year = values.Year;

        return existing;
    }
}
