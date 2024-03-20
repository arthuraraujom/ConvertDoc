
using DinkToPdf;
using Microsoft.AspNetCore.Mvc;

namespace ConvertDoc.Commons;

public class ConverterHtmlToPdf
{
    public static FileStreamResult Convert(string pStrHtml)
    {

        FileStream lFileStream = null;
        const string LCONS_PATH_FILE = ".//Temp//pdfTemp.pdf";

        try
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Out = LCONS_PATH_FILE,
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

            var converter = new BasicConverter(new PdfTools());
            converter.Convert(doc);

            lFileStream = new FileStream(LCONS_PATH_FILE, FileMode.Open);
            return new FileStreamResult(lFileStream, "application/pdf");
        }
        catch(Exception ex) 
        {
            throw new Exception("Não foi possível gerar o PDF. Erro: " + ex.Message);
        }
        finally
        {
            //if (File.Exists(LCONS_PATH_FILE))
            //    File.Delete(LCONS_PATH_FILE);
        }       

    }
}