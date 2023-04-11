using Ecommerce.Entities;
using ECommerce.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Front.BolouriGroup.Pages;

public class AboutUsModel : PageModel
{
    
    private readonly IBrandService _brandService;

    public AboutUsModel(IBrandService brandService)
    {
        _brandService = brandService;
    }
    public List<Brand> Brands { get; set; }

    public async Task OnGetAsync()
    {
        Brands = (await _brandService.Load()).ReturnData;
        Brands.RemoveAt(0);
    }

    public async Task<IActionResult> OnGetPrint()
    {
        PurchaseOrder purchaseOrder = new()
        {
            OrderGuid = Guid.NewGuid(),
            CreationDate = DateTime.Now,
            Amount = 10000M,
            PurchaseOrderDetails = new List<PurchaseOrderDetail>
            {
                new()
                {
                    Name = "kala",
                    Price = new Price()
                    {
                        Amount = 100M
                    },
                    Quantity = 1,
                    SumPrice = 1000M
                }
            },
            SendInformation = new SendInformation
            {
                Address = "Lakanshahr"
            },
            Description = "sad"
        };
    
        return RedirectToPage("InvoiceReportPrint", new
        {
            purchaseOrder = purchaseOrder,
            systemTraceNo = "systemTraceNo",
            refid = "refid"
        });
        //  RazorEngine engine = new RazorEngine();
        //  var tempalte = engine.Compile(System.IO.File.ReadAllText("wwwroot/Reports/Report.html"));
        //var result = tempalte.Run();

        //  MemoryStream memoryStream = new();

        //  using var pdfTool = new PdfTools();
        //  using var converter = new BasicConverter(pdfTool);
        //  var doc = new HtmlToPdfDocument()
        //  {
        //      GlobalSettings =
        //      {
        //          ColorMode = ColorMode.Color,
        //          Orientation = Orientation.Portrait,
        //          PaperSize = PaperKind.A4
        //      },
        //      Objects =
        //      {
        //          new ObjectSettings()
        //          {
        //              PagesCount = true,
        //              HtmlContent = result,
        //              WebSettings = { DefaultEncoding = "utf-8" }
        //          }
        //      }
        //  };

        //  var bytes = converter.Convert(doc);
        //  memoryStream.Write(bytes,0,bytes.Length);
        //  memoryStream.Seek(0,SeekOrigin.Begin);
        //  return new FileStreamResult(memoryStream, "application/pdf");
    }

}