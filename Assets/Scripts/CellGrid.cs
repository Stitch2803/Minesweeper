using UnityEngine;

public class CellGrid
{
    private readonly Cells[,] cells;

    public int Width => cells.GetLength(0);
    public int Height => cells.GetLength(1);

    public Cells this[int x, int y] => cells[x, y];

    public CellGrid(int width, int height)
    {
        cells = new Cells[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new Cells
                {
                    position = new Vector3Int(x, y, 0),
                    type = Cells.Type.Empty
                };
            }
        }
    }

    public void GenerateMines(Cells startingCell, int amount)
    {
        int width = Width;
        int height = Height;

        for (int i = 0; i < amount; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            Cells cell = cells[x, y];

            while (cell.type == Cells.Type.Mine || IsAdjacent(startingCell, cell))
            {
                x++;

                if (x >= width)
                {
                    x = 0;
                    y++;

                    if (y >= height) {
                        y = 0;
                    }
                }

                cell = cells[x, y];
            }

            cell.type = Cells.Type.Mine;
        }
    }

    public void GenerateNumbers()
    {
        int width = Width;
        int height = Height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cells cell = cells[x, y];

                if (cell.type == Cells.Type.Mine) {
                    continue;
                }

                cell.number = CountAdjacentMines(cell);
                cell.type = cell.number > 0 ? Cells.Type.Number : Cells.Type.Empty;
            }
        }
    }

    public int CountAdjacentMines(Cells cell)
    {
        int count = 0;

        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0) {
                    continue;
                }

                int x = cell.position.x + adjacentX;
                int y = cell.position.y + adjacentY;

                if (TryGetCell(x, y, out Cells adjacent) && adjacent.type == Cells.Type.Mine) {
                    count++;
                }
            }
        }

        return count;
    }

    public int CountAdjacentFlags(Cells cell)
    {
        int count = 0;

        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0) {
                    continue;
                }

                int x = cell.position.x + adjacentX;
                int y = cell.position.y + adjacentY;

                if (TryGetCell(x, y, out Cells adjacent) && !adjacent.revealed && adjacent.flagged) {
                    count++;
                }
            }
        }

        return count;
    }

    public int CountAllFlags()
    {
        int count = 0;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (this[x, y].flagged) count++;
            }
        }
        return count;
    }

    public Cells GetCell(int x, int y)
    {
        if (InBounds(x, y)) {
            return cells[x, y];
        } else {
            return null;
        }
    }

    public bool TryGetCell(int x, int y, out Cells cell)
    {
        cell = GetCell(x, y);
        return cell != null;
    }

    public bool InBounds(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    public bool IsAdjacent(Cells a, Cells b)
    {
        return Mathf.Abs(a.position.x - b.position.x) <= 1 &&
               Mathf.Abs(a.position.y - b.position.y) <= 1;
    }

}
