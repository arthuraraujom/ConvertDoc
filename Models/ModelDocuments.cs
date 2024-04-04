using ConvertDoc.Commons;
using ConvertDoc.DTOs;
using Microsoft.AspNetCore.Http.Features;
using static ConvertDoc.Commons.Consts;

namespace ConvertDoc.Models;

public class ModelDocuments
{

    public static string FactoryHtml(TypeDocument typeDocument)
    {  
        var lStrFile = System.IO.File.ReadAllLines(".//Docs//Header.html");
        var lStrHtml = Utils.StringArrayToString(lStrFile);

        var lStrPathDocs = ".//Docs//";

        switch (typeDocument)
        {
            case TypeDocument.Agreement:
                lStrFile = System.IO.File.ReadAllLines(lStrPathDocs+"Agreement.html");
                break;

            case TypeDocument.Budget:
                lStrFile = System.IO.File.ReadAllLines(lStrPathDocs+"Budget.html");  
                break; 
            default:
                throw new NotImplementedException();
                break;
        }

        var lStrDocument = Utils.StringArrayToString(lStrFile);
        lStrHtml = lStrHtml.Replace("{HEADER_DOCUMENTO}", lStrDocument);

        return lStrHtml;
    }

    public static string LoadDataHtmlAgreement(DTOAgreement pAgreement, string pStrHtml)
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
            lStrItems += "<li>" + item.name + ". Quantidade: " + item.amount.ToString() + ".</li>";
            lDblAmount = lDblAmount + (item.unit_value * item.amount);
        }

        lStrHtml = lStrHtml.Replace("{CONTRATO_ITENS}", lStrItems);

        lStrHtml = lStrHtml.Replace("{CONTRATO_VALOR}", string.Format("{0:N}", lDblAmount));
        lStrHtml = lStrHtml.Replace("{CONTRATO_VALOR_EXTENSO}", Utils.NumeroToExtenso(lDblAmount));

        lStrHtml = lStrHtml.Replace("{CONTRATO_DATA}", pAgreement.date.ToString("dd/MM/yyyy"));

        return lStrHtml;

    }

   public static string LoadDataHtmlBudget(DTOBudget pBudget, string pStrHtml)
   {
        var lStrHtml = pStrHtml;

        lStrHtml = lStrHtml.Replace("{HEADER_TITULO}", "PROPOSTA DE MANUTENÇÃO");
        lStrHtml = lStrHtml.Replace("{DATA_EMISSAO}", pBudget.date.ToString("dd/MM/yyyy"));
        lStrHtml = lStrHtml.Replace("{CLIENTE_NOME}", pBudget.name);

        var lStrItems = "";    
        var lIntQtd = 0;    
        var lDblAmount = 0.0;
        foreach(var iItem in pBudget.items) 
        {
            lIntQtd++;
            lStrItems   += getHtmlItemBudget(lIntQtd, iItem);
            lDblAmount = lDblAmount + (iItem.unit_value * iItem.amount);
        }    

        if(lIntQtd < 12)
        {
            for(var iIntCount = lIntQtd; iIntCount <= 12; iIntCount++)
            {
                  lStrItems   += getHtmlItemBudget(0, null);
  
            }
        }

        lStrHtml = lStrHtml.Replace("{ORCAMENTO_ITENS}", lStrItems);
        lStrHtml = lStrHtml.Replace("{VALOR_TOTAL}", string.Format("{0:N}", lDblAmount));

        return lStrHtml;
    }

    private static string getHtmlItemBudget(int pIntItem, DTOItem pItem)
    {
        var lStrHtml =  "<tr style=\"height: 18pt\">"+
                        "    <td style=\"text-align:center; border:.5pt solid;\">{NU_ITEM}</td>"+
                        "     <td style=\"text-align:left; border:.5pt solid; width:261pt;\">{NAME}</td>"+
                        "     <td style=\"text-align:center; border:.5pt solid;\">{QTD}</td>"+           
                        "     <td style=\"text-align:general; border:.5pt solid; text-align:right;\">{VL_UNIT}</td>"+
                        "     <td style=\"text-align:general; border:.5pt solid; text-align:right;\">{VL_AMOUNT}</td>"+
                        "</tr>";

        
         if(pIntItem > 0)
         {
            lStrHtml = lStrHtml.Replace("{NU_ITEM}", pIntItem.ToString());
            lStrHtml = lStrHtml.Replace("{NAME}", pItem.name);
            lStrHtml = lStrHtml.Replace("{QTD}", pItem.amount.ToString());
            lStrHtml = lStrHtml.Replace("{VL_UNIT}", "R$ "+string.Format("{0:N}", pItem.unit_value));
            lStrHtml = lStrHtml.Replace("{VL_AMOUNT}", "R$ "+string.Format("{0:N}", pItem.unit_value * pItem.amount));
         }
         else
         {
            lStrHtml = lStrHtml.Replace("{NU_ITEM}", "");
            lStrHtml = lStrHtml.Replace("{NAME}", "");
            lStrHtml = lStrHtml.Replace("{QTD}", "");
            lStrHtml = lStrHtml.Replace("{VL_UNIT}", "");
            lStrHtml = lStrHtml.Replace("{VL_AMOUNT}", ""); 
         }


        return lStrHtml;                   
    }


}
