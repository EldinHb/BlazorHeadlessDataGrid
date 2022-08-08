using Microsoft.AspNetCore.Components;

namespace BlazorHeadlessDataGrid.Core;

public record ColumnDef<T>
{
    public string Title { get; set; } = string.Empty;
    
    public string? Id { get; set; }

    public Func<T, object>? AccessorFunc { get; set; }
    
    public bool IsSortable { get; set; }
    
    public Func<T, CellContext<T>, RenderFragment> OnRenderCell { get; set; }

    public Action<OnToggleSortedByEventArgs>? OnToggleSortedBy { get; set; }
};