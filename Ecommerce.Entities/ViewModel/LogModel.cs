using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Entities.ViewModel;

public class LogModel
{
    public string Method { get; set; }
    public int Status { get; set; }
    public LogLevel Level { get; set; }
    public DateTime Date { get; set; }
    public string ApplicantId { get; set; }
    public PathString Route { get; set; }
    public string Messages { get; set; }
}