
using Microsoft.AspNetCore.Mvc;
using SelectPdf;

namespace ConvertDoc.Commons;

public class ConverterHtmlToPdf
{
    public static FileStreamResult Convert(string pStrHtml)
    {

        FileStream lFileStream = null;
        const string LCONS_PATH_FILE = ".\\Temp\\pdfTemp.pdf";

        try
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 15;
            converter.Options.MarginRight = 15;
            converter.Options.MarginTop = 15;
            converter.Options.MarginBottom = 15;

            PdfDocument doc = converter.ConvertHtmlString(pStrHtml);

            doc.Save(LCONS_PATH_FILE);
            doc.Close();

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