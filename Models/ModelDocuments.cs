using ConvertDoc.Commons;
using ConvertDoc.DTOs;
using static ConvertDoc.Commons.Consts;

namespace ConvertDoc.Models;

public class ModelDocuments
{

    public static string FactoryHtml(TypeDocument typeDocument)
    {
        var lStrFile = System.IO.File.ReadAllLines(".\\Docs\\Header.html");
        var lStrHtml = Utils.StringArrayToString(lStrFile);

        switch (typeDocument)
        {
            case TypeDocument.Agreement:
                lStrFile = System.IO.File.ReadAllLines(".\\Docs\\Contrato.html");
                break;
            default:
                throw new NotImplementedException();
                break;
        }

        var lStrDocument = Utils.StringArrayToString(lStrFile);

        lStrHtml = lStrHtml.Replace("{HEADER_DOCUMENTO}", lStrDocument);

        return lStrHtml;
    }

    public static string LoadDataHtml(DTOAgreement pAgreement, string pStrHtml)
    {
        var lStrHtml = pStrHtml.Replace("{CONTRATO_NUMERO}", pAgreement.number);

        lStrHtml = lStrHtml.Replace("{HEADER_TITULO}", "CONTRATO DE PRESTAÇÃO DE SERVIÇOS");
        lStrHtml = lStrHtml.Replace("{CONTRATANTE_RAZAO_SOCIAL}", pAgreement.contracting.name);
        lStrHtml = lStrHtml.Replace("{CONTRATANTE_CNPJ}", pAgreement.contracting.ein);
        lStrHtml = lStrHtml.Replace("{CONTRATANTE_ENDERECO}", pAgreement.contracting.address);
        lStrHtml = lStrHtml.Replace("{CONTRATANTE_CEP}", pAgreement.contracting.zip);
        lStrHtml = lStrHtml.Replace("{CONTRATANTE_CIDADE}", pAgreement.contracting.city);

        var lStrItems = "";
        var lDblAmount = 0.0;

        foreach (var item in pAgreement.items)
        {
            lStrItems += "<li>" + item.name + ". Quantidade " + item.amount.ToString() + ".</li>";
            lDblAmount = lDblAmount + (item.unit_value * item.amount);
        }

        lStrHtml = lStrHtml.Replace("{CONTRATO_ITENS}", lStrItems);

        lStrHtml = lStrHtml.Replace("{CONTRATO_VALOR}", string.Format("{0:N}", lDblAmount));
        lStrHtml = lStrHtml.Replace("{CONTRATO_VALOR_EXTENSO}", Utils.NumeroToExtenso(lDblAmount));

        lStrHtml = lStrHtml.Replace("{CONTRATO_DATA}", pAgreement.date.ToString("dd/MM/yyyy"));

        return lStrHtml;

    }


}
