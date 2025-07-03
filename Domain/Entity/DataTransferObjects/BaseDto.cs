namespace Entity.DataTransferObjects;

public record BaseDto<T>(
    T Id);