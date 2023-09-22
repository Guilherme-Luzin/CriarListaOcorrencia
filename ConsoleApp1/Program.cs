using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        var pdfFilePath = "C:\\Users\\Usuario\\Desktop\\codigo_Historico_aed5095b0f.pdf";
        var categorias = ExtrairTabelaDoPDF(pdfFilePath);

        foreach (var kvp in categorias)
        {
            Console.WriteLine($"[\"{kvp.Key}\"] = \"{kvp.Value}\",");
        }
        while (true)
        {
            Console.WriteLine("Terminou?");
            var x = Console.ReadLine();

            if (x == "Sim")
                break;
        }
        foreach (var kvp in categorias)
        {
            Console.WriteLine($"[\"{kvp.Key}\"] = ExtratoTipoDebitoCreditoEnum.NaoInformado.To<int>(),");
        }
        while (true)
        {
            Console.WriteLine("Terminou?");
            var x = Console.ReadLine();

            if (x == "Sim")
                break;
        }
    }

    static Dictionary<string, string> ExtrairTabelaDoPDF(string pdfFilePath)
    {
        var categorias = new Dictionary<string, string>();

        using (var reader = new PdfReader(pdfFilePath))
        {
            var table = new PdfPTable(2); // Supondo que a tabela tenha duas colunas

            // Configure as configurações da tabela, como largura das colunas, alinhamento, etc., conforme necessário

            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                var text = PdfTextExtractor.GetTextFromPage(reader, page);
                var lines = text.Split('\n');

                foreach (var line in lines)
                {
                    var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length >= 2 && !parts[0].Contains("Có"))
                    {
                        var chave = parts[0]; // A primeira parte é a chave
                        var valor = string.Join(" ", parts, 1, parts.Length - 1); // As partes restantes são o valor

                        // Adicione ao dicionário
                        categorias[chave] = valor;
                    }
                }
            }
        }

        return categorias;
    }
}
