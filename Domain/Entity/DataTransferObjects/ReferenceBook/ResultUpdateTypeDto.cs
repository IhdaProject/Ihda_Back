namespace Entity.DataTransferObjects.ReferenceBook;

public record ResultUpdateTypeDto(
    int UpdateRowCount,
    List<string> AddColumns,
    List<string> UpdateColumns,
    List<string> DeleteColumns,
    string Token);