namespace BlazorHeadlessDataGrid.Core;

public record DataGridRow<T>
{
    public string Id { get; set; }
    public List<DataGridCell<T>> Cells { get; } = new List<DataGridCell<T>>();
    public bool IsExpanded { get; set; } = false;
    public T Value { get; init; }
};