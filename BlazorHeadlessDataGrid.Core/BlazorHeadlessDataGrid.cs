namespace BlazorHeadlessDataGrid.Core;

public class BlazorHeadlessDataGrid<T>
{
    private readonly IEnumerable<ColumnDef<T>> ColumnDefs;
    private readonly DataGridOptions Options;
    
    public List<DataGridHeader> Headers { get; }
    public List<DataGridRow<T>> Rows { get; }

    public int CurrentPage { get; private set; } = 0;
    public int Pages { get; private set; }

    private List<T> Data;

    public BlazorHeadlessDataGrid(
        IEnumerable<ColumnDef<T>> columnDefs, List<T> data, DataGridOptions? options = null)
    {
        this.Data = data;
        this.ColumnDefs = columnDefs;
        this.Options = options ?? new DataGridOptions()
        {
            PageSize = 10,
            ControlledSort = true
        };
        this.CalculateMaxPages();
        this.Headers = new List<DataGridHeader>();
        this.Rows = new List<DataGridRow<T>>();
        this.InitializeHeaders();
        this.InitializeData(data);
    }

    public void UpdateData(List<T> data)
    {
        this.Data = data;
        this.CalculateMaxPages();
        this.CurrentPage = 0;
        this.InitializeData(data);
    }

    public void NextPage()
    {
        this.SetPage(CurrentPage + 1);
    }

    public void PreviousPage()
    {
        this.SetPage(CurrentPage - 1);
    }

    public void SetPage(int page)
    {
        if (page < 0) return;
        if (page > this.Pages - 1) return;
        
        this.CurrentPage = page;
        this.InitializeData(this.Data);
    }

    private void CalculateMaxPages()
    {
        this.Pages = (int)Math.Ceiling((double)this.Data.Count / this.Options.PageSize);
    }

    private void InitializeData(List<T> data)
    {
        var rows = new List<DataGridRow<T>>();
        var rowIndex = 0;

        for (var i = CurrentPage * this.Options.PageSize; i < data.Count; i++)
        {
            var value = data[i];
            var row = new DataGridRow<T>()
            {
                Id = i.ToString(),
                Value = value
            };
            foreach (var column in this.ColumnDefs)
            {
                var header = this.Headers.Find(x => x.Id.Equals(column.Id ?? column.Title));
                
                if (header == null) continue;

                var cellContext = new CellContext<T>
                {
                    Header = header,
                    Row = row
                };
                
                var cell = new DataGridCell<T>
                {
                    Header = header,
                    Value = column.AccessorFunc?.Invoke(value) ?? "",
                    Row = row,
                    Render = column.OnRenderCell?.Invoke(value, cellContext),
                    Context = cellContext
                };
                
                
                row.Cells.Add(cell);
            }

            rows.Add(row);
            rowIndex++;

            if (rowIndex == this.Options.PageSize)
            {
                break;
            }
        }

        this.Rows.Clear();
        this.Rows.AddRange(rows);
    }

    private void InitializeHeaders()
    {
        var headers = new List<DataGridHeader>();
        foreach (var column in this.ColumnDefs)
        {
            var header = new DataGridHeader
            {
                Id = column.Id ?? column.Title,
                Title = column.Title,
                IsSortable = column.IsSortable
            };

            header.OnToggleSortedBy += (obj, args) =>
            {
                column.OnToggleSortedBy?.Invoke(args);
            };

            headers.Add(header);
        }

        this.Headers.AddRange(headers);
    }
}