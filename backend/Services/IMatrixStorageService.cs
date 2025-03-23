using Models;

public interface IMatrixStorageService
{
    List<Matrix> GetAllMatrices();
    Matrix? GetMatrixById(string id);
    void AddMatrix(Matrix matrix);
    void UpdateMatrix(string id, Matrix updatedMatrix);
    void DeleteMatrix(string id);

}