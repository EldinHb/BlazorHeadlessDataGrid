using Microsoft.AspNetCore.Components;

namespace BlazorHeadlessDataGrid.Core;

public record DataGridCell<T>
{
    public DataGridHeader Header { get; set; }
    public object Value { get; set; }
    public DataGridRow<T> Row { get; set; }
    public RenderFragment? Render { get; set; }
    public CellContext<T> Context { get; set; }

    public bool IsExpanded { get; private set; } = false;
};