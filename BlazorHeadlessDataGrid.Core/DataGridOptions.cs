namespace BlazorHeadlessDataGrid.Core;

public record DataGridOptions
{
    public int PageSize { get; init; }
    public bool ControlledSort { get; init; }
}