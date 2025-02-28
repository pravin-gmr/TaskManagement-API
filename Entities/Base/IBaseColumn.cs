namespace API.Entities.Base
{
    public interface IBaseColumn
    {
        bool IsActive { get; set; }
        DateTime SavedOn { get; set; }
        long? SavedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
        long? ModifiedBy { get; set; }
    }
}
