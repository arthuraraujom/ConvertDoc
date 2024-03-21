
using DinkToPdf;
using Microsoft.AspNetCore.Mvc;

namespace ConvertDoc.Commons;

public class ConverterHtmlToPdf
{
    public static FileStreamResult Convert(string pStrHtml)
    {

        try
        {
            var lDoc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                //Out = LCONS_PATH_FILE,
                },
                
                Objects = { 
                            new ObjectSettings() {
                            PagesCount = true,
                            HtmlContent = pStrHtml,
                            WebSettings = { DefaultEncoding = "utf-8" },
                            //HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                            }
                           }
            };

            var lBasicConverter = new BasicConverter(new PdfTools());
        
            var lPdf = lBasicConverter.Convert(lDoc);
            var lMemStream = new MemoryStream(lPdf);
                
            return new FileStreamResult(lMemStream, "application/pdf");
        }
        catch(Exception ex) 
        {
            throw new Exception("Não foi possível gerar o PDF. Erro: "+ ex.Message);
        }     

    }
}