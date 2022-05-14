using System.ComponentModel.DataAnnotations;

namespace Entities.HolooEntity;

public class HolooArticle
{
    [Key] public string A_Code { get; set; }

    public string? A_Code_C { get; set; }
    public string A_Name { get; set; }
    public string? Model { get; set; }
    public string? PicturePath { get; set; }
    public double? Sel_Price { get; set; }
    public double? Sel_Price2 { get; set; }
    public double? Sel_Price3 { get; set; }
    public double? Sel_Price4 { get; set; }
    public double? Sel_Price5 { get; set; }
    public double? Sel_Price6 { get; set; }
    public double? Sel_Price7 { get; set; }
    public double? Sel_Price8 { get; set; }
    public double? Sel_Price9 { get; set; }
    public double? Sel_Price10 { get; set; }
    public double? Exist { get; set; }
    public double? A_Min { get; set; }
    public double? A_Max { get; set; }
    public double? Sel_Dollar { get; set; }
    public bool? Delete { get; set; }
    public string? Other1 { get; set; }
    public int WebId { get; set; }
    public short SendToSite { get; set; }
    public bool IsActive { get; set; }
    public int? VahedCode { get; set; }
}