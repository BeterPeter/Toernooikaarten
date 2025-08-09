namespace TournamentCardMaker;

using ClosedXML.Excel;

public class CardMaker
{
    public static void CreateCards(MatchInfo[] matchInfo, string fileName)
    {
        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Matches");

        int row = 1;
        for (int i = 0; i < matchInfo.Length; i++)
        {
            var match = matchInfo[i];

            // Title & Time (merged across columns 1-2)
            ws.Cell(row, 1).Value = $"{match.Title} ({match.Time})";
            ws.Range(row, 1, row, 4).Merge().Style.Font.Bold = true;

            // merge score cells (columns 2-4)
            for (int column = 2; column <= 4; column++)
            {
                ws.Range(row + 1, column, row + 2, column).Merge();
                ws.Range(row + 3, column, row + 4, column).Merge();
            }

            // borders
            ws.Range(row, 1, row, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range(row, 1, row + 4, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range(row + 1, 2, row + 4, 4).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            ws.Range(row + 1, 2, row + 4, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Cell(row + 2, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            row++;

            row = FillPlayerCells(ws, match.TopPlayers, row);
            row = FillPlayerCells(ws, match.BottomPlayers, row);

            if (i % 4 != 3)
                row++;
        }

        SetStyle(ws);

        workbook.SaveAs($"{fileName}.xlsx");
    }

    private static int FillPlayerCells(IXLWorksheet ws, string[] players, int row)
    {
        ws.Cell(row++, 1).Value = players.Length > 0 ? players[0] : string.Empty;
        ws.Cell(row++, 1).Value = players.Length > 1 ? players[1] : string.Empty;
        return row;
    }

    private static void SetStyle(IXLWorksheet ws)
    {
        ws.Style.Font.FontName = "Aptos Narrow";
        ws.Style.Font.FontSize = 14;
        ws.Rows().Height = 30;
        ws.Column(1).Width = 35;
        ws.Columns(2, 4).Width = 5;
        ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    }
}