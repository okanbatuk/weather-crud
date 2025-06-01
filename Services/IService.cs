namespace WeatherApiProject.Services;

public interface IService<TEntity, TUpdateDto>
{
  Task<List<TEntity>> GetAll();
  Task<TEntity?> GetById(int id);
  Task<bool> Update(int id, TUpdateDto updateDto);
  Task<bool> Delete(int id);
}