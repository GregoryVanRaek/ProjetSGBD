using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories.Interface;

namespace LocationVoiture.bll.Services;

public class ModelService :IModelService
{
    private readonly IModelRepository _modelRepository;

    public ModelService(IModelRepository modelRepository)
    {
        this._modelRepository = modelRepository;
    }
    
    public List<Model> GetAll()
    {
        try
        {
            return this._modelRepository.GetAll();
        }
        catch (Exception e)
        {
            throw new Exception("Model service error : " + e.Message);
        }
    }

    public Model? GetById(int id)
    {
        try
        {
            return this._modelRepository.GetOneById(id);
        }
        catch (Exception e)
        {
            throw new Exception("Model service error : " + e.Message);
        }
    }

    public Model? Update(Model value)
    {
        try
        {
            return this._modelRepository.Update(value);
        }
        catch (Exception e)
        {
            throw new Exception("Model service error : " + e.Message);
        }
    }

    public bool Delete(Model value)
    {
        try
        {
            return this._modelRepository.Delete(value);
        }
        catch (Exception e)
        {
            throw new Exception("Model service error : " + e.Message);
        }
    }

    public Model? Create(Model value)
    {
        try
        {
            return this._modelRepository.Create(value);
        }
        catch (Exception e)
        {
            throw new Exception("Model service error : " + e.Message);
        }
    }
}