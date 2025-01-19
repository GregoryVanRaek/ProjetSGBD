using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories.Interface;

namespace LocationVoiture.bll.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        this._categoryRepository = categoryRepository;
    }
    
    public List<Category> GetAll()
    {
        try
        {
            return this._categoryRepository.GetAll();
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Category", e);
        }
    }
    public Category? GetById(int id)
    {
        try
        {
            return this._categoryRepository.GetOneById(id);
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Category", e);
        }
    }
    public Category? Update(Category value)
    {
        try
        {
            return this._categoryRepository.Update(value);
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Category", e);
        }
    }
    public bool Delete(Category value)
    {
        try
        {
            return this._categoryRepository.Delete(value);
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Category", e);
        }
    }
    public Category? Create(Category value)
    {
        try
        {
            return this._categoryRepository.Create(value);
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Category", e);
        }
    }
}