using Pharos.Organization.Domain.Abstraction;

namespace Pharos.Organization.Domain.Aggregates.AcademicYear;

public class AcademicYear : AggregateBase<AcademicYearId>
{
    public int StartYear { get; private set; }
    public int EndYear { get; private set; }
    
    public 
}