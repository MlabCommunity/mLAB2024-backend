using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using QuizBackend.Infrastructure.Interfaces;
using System.Text;

namespace QuizBackend.Infrastructure.Services.Processors.Attachments;
public class ExcelProcessingStrategy : IAttachmentProcessingStrategy
{
    public async Task<string> ProcessFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var text = new StringBuilder();

        using (var spreadsheetDocument = SpreadsheetDocument.Open(memoryStream, false))
        {
            var workbookPart = spreadsheetDocument.WorkbookPart;
            var sharedStringTable = workbookPart!.SharedStringTablePart?.SharedStringTable;

            foreach (var worksheetPart in workbookPart.WorksheetParts)
            {
                var worksheet = worksheetPart.Worksheet;
                var sheetData = worksheet.Elements<SheetData>().First();

                foreach (var row in sheetData.Elements<Row>())
                {
                    foreach (var cell in row.Elements<Cell>())
                    {
                        var cellValue = GetCellValue(cell, sharedStringTable!);
                        text.AppendLine(cellValue);
                    }
                }
            }
        }

        return text.ToString();
    }

    private string GetCellValue(Cell cell, SharedStringTable sharedStringTable)
    {
        if (cell.DataType == null || cell.DataType == CellValues.Number)
        {
            return cell.CellValue?.InnerText ?? "";
        }

        if (cell.DataType == CellValues.SharedString)
        {
            var index = int.Parse(cell.CellValue!.InnerText);
            return sharedStringTable.ChildElements[index].InnerText;
        }

        return "";
    }
}
