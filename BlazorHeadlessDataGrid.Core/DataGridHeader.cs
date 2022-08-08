using Microsoft.AspNetCore.Components;

namespace BlazorHeadlessDataGrid.Core;

public enum SortedBy
{
    Asc,
    Desc,
    None
}

public class OnToggleSortedByEventArgs : EventArgs
{
    public readonly DataGridHeader Header;
    public readonly SortedBy SortedBy;

    public OnToggleSortedByEventArgs(DataGridHeader header, SortedBy sortedBy)
    {
        Header = header;
        SortedBy = sortedBy;
    }
} 

public record DataGridHeader
{
    public string Title { get; set; }
    public string Id { get; set; }
    public bool IsSortable { get; set; }
    public SortedBy SortedBy { get; set; } = SortedBy.None;
    public event EventHandler<OnToggleSortedByEventArgs>? OnToggleSortedBy;

    public void ToggleSortedBy()
    {
        switch (this.SortedBy)
        {
            case SortedBy.None:
                this.SortedBy = SortedBy.Asc;
                break;
            case SortedBy.Asc:
                this.SortedBy = SortedBy.Desc;
                break;
            case SortedBy.Desc:
                this.SortedBy = SortedBy.None;
                break;
            default:
                this.SortedBy = SortedBy.None;
                break;
        }
        
        this.OnToggleSortedBy?.Invoke(this, new OnToggleSortedByEventArgs(this, this.SortedBy));
    }
};