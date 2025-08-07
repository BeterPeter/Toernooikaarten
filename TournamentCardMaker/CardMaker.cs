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

            // Horizontal line (border)
            ws.Range(row, 1, row, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range(row, 1, row + 4, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            // merge score cells (columns 2-4)
            for (int column = 2; column <= 4; column++)
            {
                ws.Range(row + 1, column, row + 2, column).Merge();
                ws.Range(row + 3, column, row + 4, column).Merge();
            }

            ws.Range(row + 1, 2, row + 4, 4).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            row++;

            match.Players = match.Players.Length == 0
                ? [.. Enumerable.Repeat(string.Empty, 4)]
                : match.Players.Length == 2
                ? [match.Players[0], string.Empty, match.Players[1], string.Empty]
                : match.Players;
            
            for (int j = 0; j < match.Players.Length; j++)
            {
                ws.Cell(row, 1).Value = match.Players[j];
                if (j % 2 == 0) // Every two players, add a border
                {
                    ws.Range(row, 1, row + 1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                row++;
            }

            if (i % 4 != 3)
                row++;
        }

        SetStyle(ws);

        workbook.SaveAs($"{fileName}.xlsx");
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