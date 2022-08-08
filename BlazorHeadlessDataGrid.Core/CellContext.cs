namespace BlazorHeadlessDataGrid.Core;

public record CellContext<T>
{
    public DataGridRow<T> Row { get; set; }
    public DataGridHeader Header { get; set; }
}